using System.Collections.Generic;
using UnityEngine;

//°¡Ã­°á°ú¹°
public struct ItemCard 
{
    public string ID;
    public EDataType type;
    public EItemRarity rarity;
    public int grade;

    public ItemCard(EDataType type, EItemRarity rarity, int grade = 0) 
    {
        this.type = type;
        this.rarity = rarity;
        this.grade = grade;
        ID = $"{type}{rarity}{grade}";
    }
}

public class GachaSystem : MonoBehaviour
{
    private int _weaaponGachaLvl = 0;
    private int _accessoriesGachaLvl = 0;
    private float _min = 0;
    private float _max = 100;

    //°¡Ã­ °á°ú ¸®½ºÆ®
    public List<ItemCard> gachaResults = new List<ItemCard>();

    //¹«±â & ¾Ç¼¼ Èñ±Íµµ È®·ü
    private Dictionary<EItemRarity, float[]> _itemRarityChanceTable = new Dictionary<EItemRarity, float[]>
    {
        { EItemRarity.Normal,    new float[]{ 68.58f, 54.2f, 33.1f, 10.56f, 7.2f, 5.08f, 4.41f, 2.68f, 0.11f, 0.01f } },
        { EItemRarity.Advanced,  new float[]{ 25.5f, 32.8f, 43.5f, 55.84f, 45f, 31.2f, 21f, 18f, 4.2f, 0.5f } },
        { EItemRarity.Rare,      new float[]{ 5.4f, 11.2f, 18.4f, 23.5f, 28.5f, 40.05f, 36.5f, 29f, 18f, 9.49f } },
        { EItemRarity.Heroic,    new float[]{ 0.5149f, 1.7197f, 4.7982f, 9.59f, 18.575f, 22.618f, 36.5f, 48.2f, 73.57f, 82.85f } },
        { EItemRarity.Legendary, new float[]{ 0.005f, 0.08f, 0.2f, 0.5f, 0.7f, 1.02f, 1.54f, 2.05f, 4.02f, 7f } },
        { EItemRarity.Mythical,  new float[]{ 0.0001f, 0.0003f, 0.0018f, 0.01f, 0.025f, 0.032f, 0.05f, 0.07f, 0.1f, 0.15f } }
    };
    //½ºÅ³ È®·ü
    private Dictionary<EItemRarity, float[]> _skillRarityChanceTable = new Dictionary<EItemRarity, float[]>
    {
        { EItemRarity.Normal,    new float[]{ 40f } },
        { EItemRarity.Advanced,  new float[]{ 30f } },
        { EItemRarity.Rare,      new float[]{ 20f } },
        { EItemRarity.Heroic,    new float[]{ 8f } },
        { EItemRarity.Legendary, new float[]{ 1f } },
        { EItemRarity.Mythical,  new float[]{ 1f } }
    };
    //¹«±â & ¾Ç¼¼ µî±Þ È®·ü
    private int[] _ItemGradeChanceTable = { 40, 30, 20, 10 };

    private void Start()
    {
        //Å×½ºÆ®
        DrawGacha(EDataType.Weapon,11);
        foreach (var gachaResult in gachaResults) 
        {
            Debug.Log($"{gachaResult.type}{gachaResult.rarity}{gachaResult.grade}"); 
        }
        DrawGacha(EDataType.Accessories, 11);
        foreach (var gachaResult in gachaResults)
        {
            Debug.Log($"{gachaResult.type}{gachaResult.rarity}{gachaResult.grade}");
        }
        DrawGacha(EDataType.Skill, 11);
        foreach (var gachaResult in gachaResults)
        {
            Debug.Log($"{gachaResult.type}{gachaResult.rarity}{gachaResult.grade}");
        }
    }
    //°¡Ã­½ÇÇà
    public void DrawGacha(EDataType gachaType, int count = 1)
    {
        gachaResults.Clear();
        for (int i = 0; i < count; i++)
        {
            gachaResults.Add(DrawOnce(gachaType));
        }

        foreach (var card in gachaResults)
        {
            switch (gachaType)
            {
                case EDataType.Weapon:
                    InventorySystem.Instance.AddItem(ItemSkillDataManager.Instance.GetItemData(card));
                    break;
                case EDataType.Accessories:
                    InventorySystem.Instance.AddItem(ItemSkillDataManager.Instance.GetItemData(card));
                    break;
                case EDataType.Skill:
                    InventorySystem.Instance.AddSkill(ItemSkillDataManager.Instance.GetSkillData(card));
                    break;
            }
        }
    }
    private ItemCard DrawOnce(EDataType gachaType) 
    {
        switch (gachaType)
        {
            case EDataType.Weapon:
                return new ItemCard(
                    gachaType,
                    DrawRarity(_itemRarityChanceTable, _weaaponGachaLvl),
                    DrawGrade()
                    );
            case EDataType.Accessories:
                return new ItemCard(
                  gachaType,
                  DrawRarity(_itemRarityChanceTable, _accessoriesGachaLvl),
                  DrawGrade()
                  );
            case EDataType.Skill:
                return new ItemCard(
                  gachaType,
                  DrawRarity(_skillRarityChanceTable)
                  );
            default:
                return new ItemCard(EDataType.Weapon, EItemRarity.Normal);
        }
    }
    //Èñ±Íµµ ÃßÃ· > ÀÏ¹Ý, ·¹¾î, ½ÅÈ­ µîµî...
    private EItemRarity DrawRarity( Dictionary<EItemRarity, float[]> gachaTable, int gachaLvl = 0) 
    {
        float randomValue = Random.Range(_min, _max);
        float cumulative = 0;

        var order = new List<EItemRarity> {
            EItemRarity.Normal, EItemRarity.Advanced, EItemRarity.Rare,
            EItemRarity.Heroic, EItemRarity.Legendary, EItemRarity.Mythical };

        foreach (var rarity in order) 
        {
            cumulative += gachaTable[rarity][gachaLvl];
            if (randomValue <= cumulative) 
            {
                return rarity;
            }
        }
        return EItemRarity.Normal;
    }
    //µî±Þ ÃßÃ· > 4, 3, 2, 1
    private int DrawGrade() 
    {
        float randomValue = Random.Range(_min, _max);
        float cumulative = 0;

        for (int i = 0; i < _ItemGradeChanceTable.Length; i++) 
        {
            cumulative += _ItemGradeChanceTable[i];
            if (randomValue <= cumulative)
            {
                return _ItemGradeChanceTable.Length - i;
            }
        }
        return _ItemGradeChanceTable.Length;
    }
}
