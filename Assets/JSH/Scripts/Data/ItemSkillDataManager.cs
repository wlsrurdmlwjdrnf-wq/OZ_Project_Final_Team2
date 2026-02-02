using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSkillDataManager : MonoBehaviour
{
    public static ItemSkillDataManager Instance { get; private set; }

    public ItemDatabaseSO WeaponDatabase;
    public ItemDatabaseSO AccessoriesDatabase;
    public SkillDatabaseSO SkillDatabase;

    private void Awake()
    {
        Instance = this;
    }

    public ItemDataSO GetItemData(ItemCard card) 
    {
        ItemDatabaseSO targetDB = null;

        switch (card.type) 
        {
            case EDataType.Weapon:
                targetDB = WeaponDatabase;
                break;
            case EDataType.Accessories:
                targetDB = AccessoriesDatabase;
                break;
        }

        if ( targetDB == null ) return null;

        foreach (var item in targetDB.items) 
        {
            if (item.dataType == card.type &&
                item.itemRarity == card.rarity &&
                item.itemGrade == card.grade) 
            {
                return item;
            }
        }
        return null;
    }

    public SkillDataSO GetSkillData(ItemCard card) 
    {
        foreach (var skill in SkillDatabase.skills) 
        {
            if (skill.dataType == card.type &&
                skill.skillRarity == card.rarity) 
            {
                return skill;
            }
        }
        return null;
    }
}
