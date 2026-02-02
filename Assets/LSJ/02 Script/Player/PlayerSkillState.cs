public class PlayerSkillState : IEntityState
{
    private readonly Player _player;
    public PlayerSkillState(Player player) => _player = player;
    public void OnEnter() { }
    public void OnUpdate() { }
    public void OnFixedUpdate() { }
    public void OnExit() { }
}
