using UnityEngine;

[CreateAssetMenu(fileName = "ItemDataSO", menuName = "GameData/Item/ItemDataSO")]
public class ItemDataSO : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
    public EDataType dataType;
    public EItemRarity itemRarity;
    public int itemLvl;

    public int itemGrade;
    public float effectValue;
    public EEffectType activeType;
    public EEffectType passiveType;
}
