using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterBase : EntityStateMachine, IDamageable
{
    [SerializeField] protected MonsterBaseStatsSO _baseStats;

    public string Name { get; private set; }
    public BigNumber CurrentHP {  get; private set; }
    public BigNumber CurrentAtk { get; private set; }
    public BigNumber CurrentDef { get; private set; } 
    public MonsterIdleState IdleState { get; private set; }
    public MonsterDeadState DeadState { get; private set; }
    protected void Awake()
    {
        Name = _baseStats.monsterName;
        CurrentHP = MonsterStatCorrection(_baseStats.baseMaxHP);
        CurrentAtk = MonsterStatCorrection(_baseStats.baseAttackPower);
        CurrentDef = MonsterStatCorrection(_baseStats.baseDefensivePower);

    }
    public void TakeDamage(BigNumber amount)
    {
        if (amount <= new BigNumber(0)) return;

        CurrentHP -= amount;

        if (CurrentHP <= new BigNumber(0))
        {
            CurrentHP = new BigNumber(0);
            Die();
        }
    }
    protected void Die()
    {
        if (gameObject.TryGetComponent<MonsterBase>(out var monster))
        {
            monster.ChangeState(monster.DeadState);
        }
    }
    protected BigNumber MonsterStatCorrection(float stats)
    {
        BigNumber bn = new BigNumber(stats) *
            new BigNumber(Mathf.Pow(StageManager.Instance.CurrentMainNumber - 1, 10)) *
            new BigNumber((StageManager.Instance.CurrentSubNumber - 1) * 2);
        return bn;
    }
}
