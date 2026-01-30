using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;

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
