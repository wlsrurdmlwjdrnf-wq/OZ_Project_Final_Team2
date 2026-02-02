using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 모디파이어(스탯에 영향을 주는 요소)
[System.Serializable]
public class StatModifier
{
    public StatType statType;
    public Operation operation;
    public float value;

    public StatModifier(StatType type, Operation op, float val)
    {
        statType = type;
        operation = op;
        value = val;
    }
}
[System.Serializable]
public class PlayerSaveData
{
    public int level;
    public Tier tier;

    // StatModifier를 직렬화 가능하게 만들기 위해
    // 간단한 직렬화용 클래스 사용 (enum은 string으로 저장)
    public List<SerializableModifier> modifiers = new List<SerializableModifier>();
}

[System.Serializable]
public class SerializableModifier
{
    public string statType;     // StatType을 string으로
    public string operation;    // Operation을 string으로
    public float value;

    // 생성자 (StatModifier -> SerializableModifier 변환용)
    public SerializableModifier(StatModifier mod)
    {
        statType = mod.statType.ToString();
        operation = mod.operation.ToString();
        value = mod.value;
    }

    // 역변환 메서드
    public StatModifier ToStatModifier()
    {
        if (!Enum.TryParse<StatType>(statType, out var type) ||
            !Enum.TryParse<Operation>(operation, out var op))
        {
            Debug.LogWarning($"잘못된 modifier 데이터: {statType} / {operation}");
            return null;
        }

        return new StatModifier(type, op, value);
    }
}

public class PlayerStatManager : Singleton<PlayerStatManager>
{
    [Header("기본 데이터")]
    [SerializeField] private PlayerBaseStatsSO _baseStats;

    // 현재 상태 (세이브 로드 필요)
    private int _currentLevel = 1;
    private Tier _currentTier = Tier.Stone;

    // 캐싱 + Dirty Flag
    private Dictionary<StatType, float> _cachedValues = new Dictionary<StatType, float>();
    private bool _isDirty = true;

    // 모든 모디파이어(강화, 성장, 장비, 기타 등등)
    private readonly List<StatModifier> _modifiers = new List<StatModifier>();

    private void Awake()
    {
        if (_baseStats == null)
        {
            Debug.LogError("PlayerBaseStatsSO가 할당되지 않았습니다!");
            enabled = false;
            return;
        }

        // 초기 계산 강제
        MarkDirty();
    }

    public void MarkDirty()
    {
        _isDirty = true;
    }

    public void AddModifier(StatModifier modifier)
    {
        _modifiers.Add(modifier);
        MarkDirty();
    }

    public void RemoveModifier(StatModifier modifier)
    {
        _modifiers.Remove(modifier);
        MarkDirty();
    }

    public void ClearModifiers()
    {
        _modifiers.Clear();
        MarkDirty();
    }

    // 핵심 : 필요할 때만 전체 재계산
    private void RecalculateIfNeeded()
    {
        if (!_isDirty) return;

        _cachedValues.Clear();

        // 티어 보너스
        float tierBonus = GetTierBonus(_currentTier);

        // ================================
        // 각 스탯별 최종값 계산
        // ===============================

        // 공격력
        float finalAttack = _baseStats.baseAttackPower * tierBonus;
        finalAttack += GetAdditive(StatType.AttackPower);
        finalAttack *= GetMultiplicative(StatType.AttackPower);
        _cachedValues[StatType.AttackPower] = Mathf.Max(1f, finalAttack);

        // 최대 HP
        float finalHP = _baseStats.baseMaxHP * tierBonus;
        finalHP += GetAdditive(StatType.MaxHP);
        finalHP *= GetMultiplicative(StatType.MaxHP);
        _cachedValues[StatType.MaxHP] = Mathf.Max(10f, finalHP);

        // 초당 HP 회복량
        float finalHPRegen = _baseStats.baseHPRegenPerSec;
        finalHPRegen += GetAdditive(StatType.HPRegenPerSec);
        finalHPRegen *= GetMultiplicative(StatType.HPRegenPerSec);
        _cachedValues[StatType.HPRegenPerSec] = Mathf.Max(0f, finalHPRegen);

        // 치명타 확률 (0~1)
        float finalCritRate = _baseStats.baseCritRate;
        finalCritRate += GetAdditive(StatType.CritRate);
        finalCritRate *= GetMultiplicative(StatType.CritRate);
        _cachedValues[StatType.CritRate] = Mathf.Clamp01(finalCritRate);

        // 치명타 데미지 (1.0 이상)
        float finalCritDmg = _baseStats.baseCritDamage;
        finalCritDmg += GetAdditive(StatType.CritDamage);
        finalCritDmg *= GetMultiplicative(StatType.CritDamage);
        _cachedValues[StatType.CritDamage] = Mathf.Max(1f, finalCritDmg);

        // 최대 마나
        float finalMaxMana = _baseStats.baseMaxMana;
        finalMaxMana += GetAdditive(StatType.MaxMana);
        finalMaxMana *= GetMultiplicative(StatType.MaxMana);
        _cachedValues[StatType.MaxMana] = Mathf.Max(1f, finalMaxMana);

        // 초당 마나 회복량
        float finalManaRegen = _baseStats.baseManaRegenPerSec;
        finalManaRegen += GetAdditive(StatType.ManaRegenPerSec);
        finalManaRegen *= GetMultiplicative(StatType.ManaRegenPerSec);
        _cachedValues[StatType.ManaRegenPerSec] = Mathf.Max(0f, finalManaRegen);

        // 골드, 경험치 추가 획득량
        float finalGoldMul = _baseStats.baseGoldMultiplier;
        finalGoldMul += GetAdditive(StatType.GoldMultiplier);
        finalGoldMul *= GetMultiplicative(StatType.GoldMultiplier);
        _cachedValues[StatType.GoldMultiplier] = Mathf.Max(1f, finalGoldMul);

        float finalExpMul = _baseStats.baseExpMultiplier;
        finalExpMul += GetAdditive(StatType.ExpMultiplier);
        finalExpMul *= GetMultiplicative(StatType.ExpMultiplier);
        _cachedValues[StatType.ExpMultiplier] = Mathf.Max(1f, finalExpMul);

        // AttackSpeed -> 공격 쿨타임에 반비례
        float finalAtkSpeed = _baseStats.baseAttackSpeed;
        _cachedValues[StatType.AttackSpeed] = Mathf.Max(0.1f, finalAtkSpeed);

        // MoveSpeed -> 배경 스크롤과 몬스터 속도에 직접 사용
        float finalMoveSpeed = _baseStats.baseMoveSpeed;
        _cachedValues[StatType.MoveSpeed] = Mathf.Max(0.1f, finalMoveSpeed);

        _isDirty = false;
    }

    // 헬퍼 메서드
    private float GetAdditive(StatType type)
    {
        float sum = 0f;
        foreach (var m in _modifiers)
            if (m.statType == type && m.operation == Operation.Add)
                sum += m.value;
        return sum;
    }

    private float GetMultiplicative(StatType type)
    {
        float mul = 1f;
        foreach (var m in _modifiers)
            if (m.statType == type && m.operation == Operation.Multiply)
                mul *= m.value;
        return mul;
    }

    // 외부에서 사용할 프로퍼티들
    public int PlayerLevel => _currentLevel;
    public Tier PlayerTier => _currentTier;
    public float AttackPower => Get(StatType.AttackPower);
    public float MaxHP => Get(StatType.MaxHP);
    public float HPRegenPerSec => Get(StatType.HPRegenPerSec);
    public float CritRate => Get(StatType.CritRate);
    public float CritDamage => Get(StatType.CritDamage);
    public float MaxMana => Get(StatType.MaxMana);
    public float ManaRegenPerSec => Get(StatType.ManaRegenPerSec);
    public float GoldMultiplier => Get(StatType.GoldMultiplier);
    public float ExpMultiplier => Get(StatType.ExpMultiplier);
    public float AttackSpeed => Get(StatType.AttackSpeed);
    public float MoveSpeed => Get(StatType.MoveSpeed);

    private float Get(StatType type)
    {
        RecalculateIfNeeded();
        _cachedValues.TryGetValue(type, out float value);
        return value;
    }

    // 레벨업
    public void LevelUp()
    {
        _currentLevel++;
        MarkDirty();
    }

    // 티어 승급
    public void PromoteTier(Tier newTier)
    {
        _currentTier = newTier;
        MarkDirty();
    }
    private float GetTierBonus(Tier tier)
    {
        switch (tier)
        {
            case Tier.Bronze: return 2f;
            case Tier.Silver: return 5f;
            case Tier.Gold: return 10f;
            case Tier.Platinum: return 20f;
            case Tier.Diamond: return 50f;
            case Tier.Amethyst: return 100f;
            case Tier.Ruby: return 300f;
            case Tier.Brilliance: return 1000f;
            default: return 1f;
        }
    }

    // 새 게임 시작 시 또는 리셋이 필요할 때
    public void ResetToBase()
    {
        _currentLevel = _baseStats.baseLevel;
        _currentTier = _baseStats.baseTier;
        ClearModifiers();
        MarkDirty();
    }

    /// <summary>
    /// 현재 플레이어 상태(레벨, 티어, 모든 모디파이어)를 JSON 문자열로 변환해서 리턴합니다.
    /// 팀원(또는 다른 시스템)이 이 문자열을 파일에 쓰거나 네트워크로 전송하면 됩니다.
    /// </summary>
    public string GetSaveJson()
    {
        var saveData = new PlayerSaveData
        {
            level = _currentLevel,
            tier = _currentTier
        };

        foreach (var mod in _modifiers)
        {
            saveData.modifiers.Add(new SerializableModifier(mod));
        }

        string json = JsonUtility.ToJson(saveData, true);

        return json;
    }

    /// <summary>
    /// JSON 문자열을 받아서 현재 상태(레벨, 티어, 모디파이어)를 복원합니다.
    /// 성공 여부를 bool로 리턴합니다.
    /// </summary>
    /// <param name="json">GetSaveJson()에서 받은 문자열 또는 세이브 파일에서 읽은 문자열</param>
    /// <returns>로드 성공 여부</returns>
    public bool LoadFromJson(string json)
    {
        if (string.IsNullOrWhiteSpace(json))
        {
            Debug.LogWarning("빈 JSON 문자열 → 로드 실패");
            return false;
        }

        try
        {
            var saveData = JsonUtility.FromJson<PlayerSaveData>(json);

            if (saveData == null)
            {
                Debug.LogWarning("JSON 파싱 실패: saveData가 null");
                return false;
            }

            _currentLevel = Mathf.Max(1, saveData.level);
            _currentTier = saveData.tier;

            _modifiers.Clear();

            foreach (var sMod in saveData.modifiers)
            {
                var mod = sMod.ToStatModifier();
                if (mod != null)
                {
                    _modifiers.Add(mod);
                }
                else
                {
                    Debug.LogWarning($"잘못된 모디파이어 무시됨: {sMod.statType} / {sMod.operation}");
                }
            }

            MarkDirty();

            Debug.Log($"JSON 로드 성공 → Lv.{_currentLevel} {_currentTier}, 모디파이어 {_modifiers.Count}개");
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError($"JSON 로드 중 예외 발생: {e.Message}\nJSON 내용:\n{json}");
            return false;
        }
    }

    // 디버그용
    [ContextMenu("Log All Stats")]
    private void LogStats()
    {
        RecalculateIfNeeded();
        foreach (var kvp in _cachedValues)
        {
            Debug.Log($"{kvp.Key}: {kvp.Value:F2}");
        }
    }
}
