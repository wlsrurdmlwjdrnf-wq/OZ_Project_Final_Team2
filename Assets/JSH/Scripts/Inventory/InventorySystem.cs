using System.Collections.Generic;
using UnityEngine;

public struct InventorySlot 
{
    public int stack;
    public IUpgradable instance;  //스킬, 아이템 인스턴스 이 안에 데이터 있음
    public bool unlocked;

    public InventorySlot(IUpgradable instance, int stack) 
    {
        this.instance = instance;
        this.stack = stack;
        this.unlocked = true; //최초 생성 시 해금
    }

    public bool TryCombine(int requireStack, out IUpgradable newInstance) 
    {
        newInstance = null;
        if (stack < requireStack) return false;
        if (instance is ItemInstance itemInstance)
        {
            var baseData = itemInstance.baseData;
            int grade = baseData.itemGrade;
            EItemRarity rarity = baseData.itemRarity;

            if (grade > 1)
            {
                grade--;
            }
            else
            {
                rarity = GetNextRarity(rarity);
                grade = 4;
            }

            ItemCard card = new ItemCard
            {
                type = baseData.dataType,
                rarity = rarity,
                grade = grade
            };

            ItemDataSO newData = ItemSkillDataManager.Instance.GetItemData(card);
            if (newData != null) 
            {
                newInstance = new ItemInstance(newData);
            }
        }
        else if (instance is SkillInstance skillInstance) 
        {
            var baseData = skillInstance.baseData;
            EItemRarity rarity = GetNextRarity(baseData.skillRarity);

            ItemCard card = new ItemCard
            {
                type = baseData.dataType,
                rarity = rarity,
                grade = 4
            };

            SkillDataSO newSkill = ItemSkillDataManager.Instance.GetSkillData(card);
            if (newSkill != null) 
            {
                newInstance = new SkillInstance(newSkill);
            }
        }

        stack -= requireStack;
        return true;
    }

    private EItemRarity GetNextRarity(EItemRarity rarity) 
    {
        switch (rarity) 
        {
            case EItemRarity.Normal: return EItemRarity.Advanced;
            case EItemRarity.Advanced: return EItemRarity.Rare;
            case EItemRarity.Rare: return EItemRarity.Heroic;
            case EItemRarity.Heroic: return EItemRarity.Legendary;
            case EItemRarity.Legendary: return EItemRarity.Mythical;
            default: return EItemRarity.Mythical;
        }
    }
}

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem Instance { get; private set; }

    private List<InventorySlot> _weaponInventory = new List<InventorySlot>();
    private List<InventorySlot> _accessoriesInventory = new List<InventorySlot>();
    private List<InventorySlot> _skillInventory = new List<InventorySlot>();

    private int testGold = 1000; //임시골드

    private void Awake()
    {
        Instance = this;
    }

    public void AddItem(ItemDataSO itemDataSO) 
    {
        var newInstance = new ItemInstance(itemDataSO);
        AddToInventory(newInstance, itemDataSO.dataType);
    }

    public void AddSkill(SkillDataSO skillDataSO)
    {
        var newInstance = new SkillInstance(skillDataSO);
        AddToInventory(newInstance, skillDataSO.dataType);
    }
    private void AddToInventory(IUpgradable instance, EDataType type)
    {
        List<InventorySlot> targetInventory = GetInventory(type);

        if (targetInventory == null) return;

        int index = targetInventory.FindIndex(slot => slot.instance == instance);

        if (index >= 0)
        {
            var slot = targetInventory[index];
            slot.stack++;
            targetInventory[index] = slot;
        }
        else
        {
            targetInventory.Add(new InventorySlot(instance, 1));
        }
    }

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
            //합성실패
            Debug.Log("NotEnoughStack");
        }
        //스택감소 반영
        targetInventory[slotIndex] = slot;
    }

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
            //강화실패
            Debug.Log("NotEnoughCost");
        }
    }

    public void Equip() 
    {

    }

    private int CalculateUpgradeCost(int currentLevel) 
    {
        return currentLevel * 100;  //임시
    }

    private List<InventorySlot> GetInventory(EDataType type) 
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
}
