public class PlayerCrouchState : PlayerBaseState
{
    public PlayerCrouchState(PlayerStateManager _currentContex, StateFactory playerStateFactory)
        : base(_currentContex, playerStateFactory) { }

    public override void EnterState()
    {
        Ctx._ani.SetBool(Ctx.Crouching, true);
        Ctx.CantShoot = true;
        if (Ctx.GetWeaponIK != null)
            Ctx.GetWeaponIK.AttackHand = false;
    }
    public override void UpdateState()
    {
        CheckSwitchState();
    }
    public override void ExitState()
    {
        Ctx._ani.SetBool(Ctx.Crouching, false);
        Ctx.CantShoot = false;

        if (Ctx.GetWeaponIK != null)
            Ctx.GetWeaponIK.AttackHand = true;
    }

    public override void CheckSwitchState()
    {
        if (!Ctx.IsCrouching)
            SwitchState(Factory.Idle());
        else if (Ctx.IsMovementPress)
            SwitchState(Factory.Crawl());
    }
    public override void InitializeSubState()
    {
    }
}
