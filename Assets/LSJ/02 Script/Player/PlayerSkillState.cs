using UnityEngine;

public class PlayerSkillState : IEntityState
{
    private readonly Player _player;
    public PlayerSkillState(Player player) => _player = player;
    public void OnEnter() 
    {
        _player.Animator.speed = 1f;
        _player.Animator.SetInteger("AttackIndex", Random.Range(1, 4));
    }
    public void OnUpdate() { }
    public void OnFixedUpdate() { }
    public void OnExit() { }
}
