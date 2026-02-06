using UnityEngine;

[CreateAssetMenu(fileName = "SkillDataSO", menuName = "GameData/Skill/SkillDataSO")]
public class SkillDataSO : ScriptableObject
{
<<<<<<< HEAD
    public string Name;
    public EDataType Type;
    public ElementType Element;
    public GradeType Grade;
    public int Level;
    public DataSOType DataSO;

    public ESkillEffectType SkillType;
    public float CoolTime;
    public float ManaCost;
    public int TriggerCount;
    public float Range;
    public float Damage;
    public float CriticalRate;
    public float CriticalDMG;

    public StatType Stat;
    public float ModifyAmount;
=======
    public string skillName;
    public Sprite skillIcon;
    public EDataType dataType;
    public EItemRarity skillRarity;
    public int skillLvl;
>>>>>>> parent of 4c43670 (02/02 작업전 DEV 정리)
}
