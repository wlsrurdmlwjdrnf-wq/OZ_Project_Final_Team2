using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKnockBackState : IEntityState
{
    private readonly Player _player;
    public PlayerKnockBackState(Player player) => _player = player;
    public void OnEnter()
    {
        _player.Animator.speed = 1f;
        _player.Animator.SetBool("IsKnockBack", true);
    }
    public void OnUpdate() { }
    public void OnFixedUpdate() { }
    public void OnExit()
    {

    }
}
