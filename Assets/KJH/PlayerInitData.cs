using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;

public class PlayerInitData
{
    [PrimaryKey]
    public int StartGold { get; set; }
    public int StartLevel { get; set; }
    public float DefaultGoldPer { get; set; }
    public bool TutorialClear { get; set; }
    public DataSOType DataSO { get; set; }
}
