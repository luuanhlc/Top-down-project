public class PlayerAttackState : PlayerBaseState
{
    public PlayerAttackState(PlayerStateManager _currentContex, StateFactory playerStateFactory)
        : base(_currentContex, playerStateFactory) { }

    public override void EnterState()
    {
        Ctx._ani.SetTrigger(Ctx.Attack);
    }
    public override void UpdateState()
    {
        CheckSwitchState();
    }
    public override void ExitState()
    {
        Ctx._ani.SetBool(Ctx.Crouching, false);
        Ctx.GetWeaponIK.AttackHand = true;
    }

    public override void CheckSwitchState()
    {
        if (Ctx.IsAttacking) return;

        if (!Ctx.IsCrouching && !Ctx.IsMovementPress)
            SwitchState(Factory.Idle());
        else if (!Ctx.IsCrouching && Ctx.IsMovementPress)
            SwitchState(Factory.Walk());
    }
    public override void InitializeSubState()
    {
    }
}
