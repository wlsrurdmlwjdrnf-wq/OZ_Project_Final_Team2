using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDatabaseSO", menuName = "Database/ItemDatabaseSO")]
public class ItemDatabaseSO : ScriptableObject
{
    public string version;
    public List<WeaponData> weapons;
    public List<AccessoryData> accessories;
    public List<ArtifactData> artifacts;
    public List<SkillData> skills;
    public List<PlayerInitData> playerInits;
    public List<StageData> stages;
}
