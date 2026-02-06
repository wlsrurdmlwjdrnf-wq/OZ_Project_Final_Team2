using UnityEngine;

[CreateAssetMenu(fileName = "ItemDataSO", menuName = "GameData/Item/ItemDataSO")]
public class ItemDataSO : ScriptableObject
{
<<<<<<< HEAD
    public string Name;
    public EDataType Type;
    public ElementType Element;
    public GradeType Grade;
    public int Tier;
    public int Level;
    public float EquipATK;
    public float PassiveATK;
    public float CriticalDMG;
    public float CriticalRate;
    public float GoldPer;
    public DataSOType DataSO;
=======
    public string itemName;
    public Sprite itemIcon;
    public EDataType dataType;
    public EItemRarity itemRarity;
    public int itemLvl;

    public int itemGrade;
    public float effectValue;
    public EEffectType activeType;
    public EEffectType passiveType;
>>>>>>> parent of 4c43670 (02/02 작업전 DEV 정리)
}
