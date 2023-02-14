public class EnemyRunState : EnemyBaseState
{
    public EnemyRunState(EnemyStateManager _currentContex, EnemyStateFactory _factory)
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
        if (Ctx.IsMovement && !Ctx.FoundPlayer)
            SwitchState(Factory.Walk());
        else if (!Ctx.IsMovement)
            SwitchState(Factory.Idle());
        else if( Ctx.CanAttack)
            SwitchState(Factory.Attack());
    }
}
