using UnityEngine;

[CreateAssetMenu(fileName = "SkillDataSO", menuName = "GameData/Skill/SkillDataSO")]
public class SkillDataSO : ScriptableObject
{
    public string skillName;
    public Sprite skillIcon;
    public EDataType dataType;
    public EItemRarity skillRarity;
    public int skillLvl;
}
