using SQLite4Unity3d;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheetData
{
    public string version { get; set; }
    public Dictionary<string, List<Dictionary<string, object>>> data { get; set; }
}

// 무기 데이터
public class WeaponData
{
    [PrimaryKey]
    public int ID { get; set; }
    public string Name { get; set; }
    public ElementType Element { get; set; }
    public GradeType Grade { get; set; }
    public int Level { get; set; }
    public double EquipATK { get; set; }
    public double PassiveATK { get; set; }
    public int CriticalDMG { get; set; }
    public float CriticalRate { get; set; }
    public float GoldPer { get; set; }
    public DataSOType DataSO { get; set; }
}

// 액세서리 데이터 
public class AccessoryData
{
    [PrimaryKey]
    public int ID { get; set; }
    public string Name { get; set; }
    public ElementType Element { get; set; }
    public GradeType Grade { get; set; }
    public int Level { get; set; }
    public double Hp { get; set; }
    public float HpPer { get; set; }
    public float MpPer { get; set; }
    public float GoldPer { get; set; }
    public DataSOType DataSO { get; set; }
}

// 유물 데이터
public class ArtifactData
{
    [PrimaryKey] public int Id { get; set; }
    public string Name { get; set; }
    public ElementType Element{ get; set; }
    public GradeType Grade { get; set; }
    public int Level { get; set; }
    public DataSOType DataSO { get; set; }
}

// 스킬 데이터
public class SkillData
{
    [PrimaryKey]
    public int ID { get; set; }
    public string Name { get; set; }
    public ElementType Element { get; set; }
    public GradeType Grade { get; set; }
    public int Level { get; set; }
    public DataSOType DataSO { get; set; }
}

// 유저 초기화 데이터
public class PlayerInitData
{
    [PrimaryKey]
    public string Name { get; set; }
    public int Level { get; set; }
    public Tier Tier { get; set; }
    public float ATKPower { get; set; }
    public float MaxHP { get; set; }
    public float HPRegenPerSec { get; set; }
    public float MAxMP { get; set; }
    public float CriticalRate { get; set; }
    public float CriticalDamage { get; set; }
    public float MPRegenPerSec { get; set; }
    public float GoldMultiplier { get; set; }
    public float EXPMultiplier { get; set; }
    public float ATKSpeed { get; set; }
    public float MoveSpeed { get; set; }
}

// 스테이지 데이터
public class StageData
{
    [PrimaryKey]
    public int ID { get; set; }
    public string Name { get; set; }
    public ElementType Element { get; set; }
    public GradeType Grade { get; set; }
    public int Level { get; set; }
    public DataSOType DataSO { get; set; }
}
