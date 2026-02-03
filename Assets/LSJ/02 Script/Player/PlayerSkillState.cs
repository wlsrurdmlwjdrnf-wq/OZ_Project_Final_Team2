public class PlayerSkillState : IEntityState
{
    private readonly Player _player;
    public PlayerSkillState(Player player) => _player = player;
    public void OnEnter() 
    {
        // 스킬 시전 애니메이션
    }
    public void OnUpdate() { }
    public void OnFixedUpdate() { }
    public void OnExit() { }
}
