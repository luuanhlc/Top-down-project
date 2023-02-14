public class EnemyAttackState : EnemyBaseState
{
    public EnemyAttackState(EnemyStateManager _currentContex, EnemyStateFactory _factory)
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
        if (Ctx.IsMovement && !Ctx.Runing)
            SwitchState(Factory.Walk());
        else if(Ctx.IsMovement && Ctx.Runing)
            SwitchState(Factory.Run());
        else if(!Ctx.IsMovement)
            SwitchState(Factory.Idle());
            
    }
}
