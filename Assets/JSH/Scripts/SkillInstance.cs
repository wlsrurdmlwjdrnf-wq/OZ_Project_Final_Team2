using UnityEngine;

public class SkillInstance : IUpgradable
{
    public SkillDataSO baseData;
    public int currentLevel;

    public SkillInstance(SkillDataSO data)
    {
        baseData = data;
        currentLevel = data.Level;
    }

    public int Level => currentLevel;

    public void Upgrade()
    {
        currentLevel++;
    }
}
