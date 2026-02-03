public class PlayerDeadState : IEntityState
{
    private readonly Player _player;
    public PlayerDeadState(Player player) => _player = player;
    public void OnEnter() 
    {
        _player.Animator.speed = 1f;
        _player.Animator.SetBool("IsDead", true);
    }
    public void OnUpdate() { }
    public void OnFixedUpdate() { }
    public void OnExit() 
    {

    }
}
