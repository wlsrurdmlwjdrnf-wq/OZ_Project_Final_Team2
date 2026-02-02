public class PlayerDeadState : IEntityState
{
    private readonly Player _player;
    public PlayerDeadState(Player player) => _player = player;
    public void OnEnter() { }
    public void OnUpdate() { }
    public void OnFixedUpdate() { }
    public void OnExit() { }
}
