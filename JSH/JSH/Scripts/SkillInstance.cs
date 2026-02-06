using UnityEngine;

public class SkillInstance : IUpgradable
{
    public SkillDataSO baseData;
    public int currentLevel;
    public ISkillEffect effect;
    private float _lastCastTime;
    private float _currAttackCount;
    public SkillInstance(SkillDataSO data, ISkillEffect skillEffect)
    {
        baseData = data;
<<<<<<< HEAD
        currentLevel = data.Level;
        effect = skillEffect;
=======
        currentLevel = data.skillLvl;
>>>>>>> parent of 4c43670 (02/02 ìž‘ì—…ì „ DEV ì •ë¦¬)
    }
    public int Level => currentLevel;
    public void Upgrade()
    {
        currentLevel++;
    }
    public void OnNormalAttack() 
    {
        _currAttackCount++;
    }
    //³ªÁß¿¡ ¸Å°³º¯¼ö ¼öÁ¤ ÇÊ¿äÇÒ ¼ö ÀÖÀ½
    public bool CanCast(float mana) 
    {
        if (Time.time < _lastCastTime + baseData.CoolTime) return false;  //ÄðÅ¸ÀÓÃ¼Å© 
        if (mana < baseData.ManaCost) return false;  //¸¶³ªÃ¼Å©
        if (_currAttackCount < baseData.TriggerCount) return false;  //ÆòÅ¸È½¼öÃ¼Å©
        return true;
    }
    public void Cast(PlayerStatManager player) 
    {
        //ÇöÀç¸¶³ª·Î ¼öÁ¤ÇÊ¿ä
        if (!CanCast(player.MaxMana)) return;
        //¸¶³ª°¨¼Ò
        _lastCastTime = Time.time;
        _currAttackCount = 0;
        effect.Apply();
    }
}
