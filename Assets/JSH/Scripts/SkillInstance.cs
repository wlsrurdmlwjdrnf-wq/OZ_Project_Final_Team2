using UnityEngine;

public class SkillInstance : IUpgradable
{
    public SkillDataSO baseData;
    public int currentLevel;

    public SkillInstance(SkillDataSO data)
    {
        baseData = data;
        currentLevel = data.skillLvl;
    }

    public int Level => currentLevel;

    public void Upgrade()
    {
        currentLevel++;
    }
}
