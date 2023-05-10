public class EnemyIdleState : EnemyBaseState
{
    public EnemyIdleState(EnemyStateManager _currentContex, EnemyStateFactory _factory)
            : base(_currentContex, _factory) { }

    public override void EnterState()
    {

    }

    public override void UpdateState()
    {
        CheckSwitchState();
    }

    public override void ExitState()
    {

    }

    public override void CheckSwitchState()
    {
    }
}
