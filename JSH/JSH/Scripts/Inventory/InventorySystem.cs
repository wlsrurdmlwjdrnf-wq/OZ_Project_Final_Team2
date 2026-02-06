<<<<<<< HEAD
using System.Collections;
=======
>>>>>>> parent of 4c43670 (02/02 ìž‘ì—…ì „ DEV ì •ë¦¬)
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem Instance { get; private set; }

    private List<InventorySlot> _weaponInventory = new List<InventorySlot>();
    private List<InventorySlot> _accessoriesInventory = new List<InventorySlot>();
    private List<InventorySlot> _skillInventory = new List<InventorySlot>();

    private InventorySlot _equippedWeapon = null;
    private InventorySlot _equippedAccessory = null;
    private List<InventorySlot> _equippedSkills = new List<InventorySlot>();
    private int _maxSkillSlot = 8;
    private int _currSkillSlot = 4;

    private int testGold = 1000; //ÀÓ½Ã°ñµå

    private void Awake()
    {
        Instance = this;
    }
    public void Initialize()
    {
        foreach (var item in ItemSkillDataManager.Instance.WeaponDatabase.items) 
        {
            _weaponInventory.Add(new InventorySlot(new ItemInstance(item), 0, false));
        }
        foreach (var item in ItemSkillDataManager.Instance.AccessoriesDatabase.items)
        {
            _accessoriesInventory.Add(new InventorySlot(new ItemInstance(item), 0, false));
        }
        foreach (var skill in ItemSkillDataManager.Instance.SkillDatabase.skills)
        {
            _skillInventory.Add(new InventorySlot(SkillFactory.CreateInstance(skill), 0, false));
        }
        Debug.Log($"Weapon:{_weaponInventory.Count}, Accessory:{_accessoriesInventory.Count}, Skill:{_skillInventory.Count}");
        TotalStats stat = CalculateStats();
    }
    #region Á¤·Ä
    public void SortInventory(EDataType type)
    {
        List<InventorySlot> targetInventory = GetInventory(type);
        targetInventory.Sort((a, b) =>
        {
            object aData = null;
            if (a.instance is ItemInstance ai) { aData = ai.baseData; }
            else if (a.instance is SkillInstance asi) { aData = asi.baseData; }
            object bData = null;
            if (b.instance is ItemInstance bi) { bData = bi.baseData; }
            else if (b.instance is SkillInstance bsi) { bData = bsi.baseData; }
            GradeType aGrade;
            int aTier;
            if (aData is ItemDataSO aItem)
            {
                aGrade = aItem.Grade;
                aTier = aItem.Tier;
            }
            else if (aData is SkillDataSO aSkill)
            {
                aGrade = aSkill.Grade;
                aTier = 0;
            }
            else return 0;
            GradeType bGrade;
            int bTier;
            if (bData is ItemDataSO bItem)
            {
                bGrade = bItem.Grade;
                bTier = bItem.Tier;
            }
            else if (bData is SkillDataSO bSkill)
            {
                bGrade = bSkill.Grade;
                bTier = 0;
            }
            else return 0;
            //µî±Þºñ±³
            int gradeCompare = GetGradeOrder(aGrade).CompareTo(GetGradeOrder(bGrade));
            if (gradeCompare != 0) return gradeCompare;
            //Æ¼¾îºñ±³
            return bTier.CompareTo(aTier);
        });
    }
    #endregion
    #region È¹µæ
    public void AddItem(ItemDataSO itemDataSO) 
    {
        var newInstance = new ItemInstance(itemDataSO);
        AddToInventory(newInstance, itemDataSO.Type);
    }
    public void AddSkill(SkillDataSO skillDataSO)
    {
        var newInstance = SkillFactory.CreateInstance(skillDataSO);
        AddToInventory(newInstance, skillDataSO.Type);
    }
    private void AddToInventory(IUpgradable instance, EDataType type)
    {
        List<InventorySlot> targetInventory = GetInventory(type);
        if (targetInventory == null) return;
        int index = targetInventory.FindIndex(slot => 
        (slot.instance is ItemInstance ii && instance is ItemInstance ni && ii.baseData == ni.baseData) ||
        (slot.instance is SkillInstance si && instance is SkillInstance nsi && si.baseData == nsi.baseData));
        if (index >= 0)
        {
            var slot = targetInventory[index];
            if (!slot.unlocked)
            {
                slot.unlocked = true;
                slot.stack = 0;
            }
            else 
            {
                slot.stack++;
            }
        }
        else
        {
            targetInventory.Add(new InventorySlot(instance, 0, true));
        }
    }
    public void UnlockItem(EDataType type, int slotIndex) 
    {
        List<InventorySlot> targetInventory = GetInventory(type);
        var slot = targetInventory[slotIndex];
        slot.stack++;
        slot.unlocked = true;
        targetInventory[slotIndex] = slot;
    }
    #endregion
    #region ÇÕ¼º
    public void CombineSlot(EDataType type, int slotIndex, int requiredStack = 5) 
    {
        List<InventorySlot> targetInventory = GetInventory(type);
        var slot = targetInventory[slotIndex];
        if (slot.TryCombine(requiredStack, out IUpgradable newInstance))
        {
            targetInventory.Add(new InventorySlot(newInstance, 1));
        }
        else 
        {
            //ÇÕ¼º½ÇÆÐ
            Debug.Log("NotEnoughStack");
        }
        //½ºÅÃ°¨¼Ò ¹Ý¿µ
        targetInventory[slotIndex] = slot;
    }
    #endregion
    #region °­È­
    public void UpgradeSlot(EDataType type, int slotIndex) 
    {
        List<InventorySlot> targetInventory = GetInventory(type);
        var slot = targetInventory[slotIndex];
        int cost = CalculateUpgradeCost(slot.instance.Level);
        if (testGold >= cost)
        {
            testGold -= cost;
            slot.instance.Upgrade();
            targetInventory[slotIndex] = slot;
        }
        else 
        {
            //°­È­½ÇÆÐ
            Debug.Log("NotEnoughCost");
        }
    }
    #endregion
    #region ÀåÂø
    public void Equip(EDataType type, int slotIndex)
    {
        List<InventorySlot> targetInventory = GetInventory(type);
        var slot = targetInventory[slotIndex];
        Equip(type, slot);
    }
    public void Equip(EDataType type,InventorySlot slot)
    {
        //ÇØ±Ý ¾ÈµÈ °æ¿ì
        if (!slot.unlocked)
        {
            Debug.Log("NotUnlocked");
            return;
        }
        switch (type)
        {
            case EDataType.Weapon:
                if (_equippedWeapon != null) { _equippedWeapon = null; }
                _equippedWeapon = slot;
                Debug.Log($"EquippedWeapon:{((ItemInstance)slot.instance).baseData.Name}");
                break;
            case EDataType.Accessories:
                if (_equippedAccessory != null) { _equippedAccessory = null; }
                _equippedAccessory = slot;
                Debug.Log($"EquippedAccessory:{((ItemInstance)slot.instance).baseData.Name}");
                break;
            case EDataType.Skill:
                if (!_equippedSkills.Contains(slot) && _equippedSkills.Count < _currSkillSlot)
                {
                    _equippedSkills.Add(slot);
                    Debug.Log($"EquippedSkill:{((SkillInstance)slot.instance).baseData.Name}");
                }
                break;
        }
    }
    public void UnEquip(EDataType type, int slotIndex = -1)
    {
        switch (type)
        {
            case EDataType.Weapon:
                _equippedWeapon = null;
                break;
            case EDataType.Accessories:
                _equippedAccessory = null;
                break;
            case EDataType.Skill:
                if (slotIndex >= 0 && slotIndex < _equippedSkills.Count)
                { _equippedSkills.RemoveAt(slotIndex); }
                else
                { _equippedSkills.Clear(); }
                break;
        }
    }
    #endregion
    #region ½ºÅÈÇÕ»ê
    public TotalStats CalculateStats() 
    {
        TotalStats totalStats = new TotalStats();
        //ref·Î ÇØ¾ß ¿øº»µµ ¼öÁ¤µÊ
        AddPassiveStats(EDataType.Weapon, ref totalStats);
        AddPassiveStats(EDataType.Accessories, ref totalStats);
        AddPassiveStats(EDataType.Skill, ref totalStats);
        AddEquipStats(_equippedWeapon, ref totalStats);
        AddEquipStats(_equippedAccessory, ref totalStats);
        foreach (var skillSlot in _equippedSkills) { AddEquipStats(skillSlot, ref totalStats); }
        return totalStats;
    }
    private void AddPassiveStats(EDataType type, ref TotalStats totalStats) 
    {
        List<InventorySlot> targetInventory = GetInventory(type);
        foreach (var slot in targetInventory) 
        {
            if (!slot.unlocked) continue;
            if (slot.instance is ItemInstance item)
            {
                var data = item.baseData;
                switch (type) 
                {
                    case EDataType.Weapon:
                        totalStats.ATK += data.PassiveATK;
                        break;
                    case EDataType.Accessories:
                        totalStats.HP += data.PassiveATK;
                        break;
                }
                totalStats.CriticalRate += data.CriticalRate;
                totalStats.CriticalDMG += data.CriticalDMG;
                totalStats.GoldPer += data.GoldPer;
            }
            else if (slot.instance is SkillInstance skill) 
            {
                break;
            }
        }
    }
    private void AddEquipStats(InventorySlot slot, ref TotalStats totalStats) 
    {
        if ( slot == null || !slot.unlocked) return;

        if (slot.instance is ItemInstance item)
        {
            var data = item.baseData;
            if (data.Type == EDataType.Weapon)
                totalStats.ATK += data.EquipATK;
            else if (data.Type == EDataType.Accessories)
                totalStats.HP += data.EquipATK;
        }
        else if (slot.instance is SkillInstance skill) 
        {
            var data = skill.baseData;
            switch (data.Stat) 
            {
                case StatType.AttackPower: 
                    totalStats.ATK += data.ModifyAmount;
                    break;
                case StatType.AttackSpeed:
                    totalStats.AttackSpeed += data.ModifyAmount;
                    break;
                case StatType.MoveSpeed:
                    totalStats.MoveSpeed += data.ModifyAmount;
                    break;
            }
        }
    }
    #endregion
    #region ÇïÆÛ
    public void PrintInventory(EDataType type)
    {
        List<InventorySlot> targetInventory = GetInventory(type);
        Debug.Log($"{type} Inventory");
        for (int i = 0; i < targetInventory.Count; i++)
        {
            var slot = targetInventory[i];
            string name = "";
            if (slot.instance is ItemInstance item) { name = item.baseData.Name; }
            else if (slot.instance is SkillInstance skill) { name = skill.baseData.Name; }
            Debug.Log($"Index:{i}, Name:{name}, Grade:{GetGrade(slot)}, Tier:{GetTier(slot)}");
        }
    }
    private int CalculateUpgradeCost(int currentLevel) 
    {
        return currentLevel * 100;  //ÀÓ½Ã
    }
    private void AddSkillSlot() 
    {
        if (_currSkillSlot < _maxSkillSlot) _currSkillSlot++;
    }
    private int GetGradeOrder(GradeType grade) 
    {
        switch (grade) 
        {
            case GradeType.Normal: return 0;
            case GradeType.Advanced: return 1;
            case GradeType.Rare: return 2;
            case GradeType.Heroic: return 3;
            case GradeType.Legendary: return 4;
            case GradeType.Mythical: return 5;
            default: return 99999;
        }
    }
    private GradeType GetGrade(InventorySlot slot) 
    {
        if (slot.instance is ItemInstance item) return item.baseData.Grade;
        if (slot.instance is SkillInstance skill) return skill.baseData.Grade;
        return GradeType.Normal;
    }
    private int GetTier(InventorySlot slot) 
    {
        if (slot.instance is ItemInstance item) return item.baseData.Tier;
        return 0;
    }
    public List<InventorySlot> GetInventory(EDataType type) 
    {
        List<InventorySlot> targetInventory = null;

        switch (type)
        {
            case EDataType.Weapon:
                targetInventory = _weaponInventory;
                break;
            case EDataType.Accessories:
                targetInventory = _accessoriesInventory;
                break;
            case EDataType.Skill:
                targetInventory = _skillInventory;
                break;
        }
        return targetInventory;
    }
<<<<<<< HEAD
    public List<InventorySlot> GetEquippedSkills() { return _equippedSkills; }
    public InventorySlot GetEquippedWeapon() { return _equippedWeapon; }
    public InventorySlot GetEquippedAccessory() { return _equippedAccessory; }
    #endregion
}
=======
}
>>>>>>> parent of 4c43670 (02/02 ìž‘ì—…ì „ DEV ì •ë¦¬)
