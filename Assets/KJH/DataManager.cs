using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    public Dictionary<int, WeaponData> weaponData = new Dictionary<int, WeaponData>();
    public Dictionary<int, AccessoryData> accessoryData = new Dictionary<int, AccessoryData>();
    public Dictionary<int, ArtifactData> artifactData = new Dictionary<int, ArtifactData>();
    public Dictionary<int, SkillData> skillData = new Dictionary<int, SkillData>();
}
