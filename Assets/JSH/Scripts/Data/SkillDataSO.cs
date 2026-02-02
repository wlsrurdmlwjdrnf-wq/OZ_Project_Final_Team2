using UnityEngine;

[CreateAssetMenu(fileName = "SkillDataSO", menuName = "GameData/Skill/SkillDataSO")]
public class SkillDataSO : ScriptableObject
{
    public string Name;
    public EDataType Type;
    public ElementType Element;
    public GradeType Grade;
    public int Level;
    public DataSOType DataSO;
}
