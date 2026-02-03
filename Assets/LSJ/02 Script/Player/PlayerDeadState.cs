public class PlayerDeadState : IEntityState
{
    private readonly Player _player;
    public PlayerDeadState(Player player) => _player = player;
    public void OnEnter() 
    {
        // 죽는 애니메이션
    }
    public void OnUpdate() { }
    public void OnFixedUpdate() { }
    public void OnExit() 
    {
        // 죽은 스테이지에서 리셋
    }
}
