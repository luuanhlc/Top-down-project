using UnityEngine;

public class PlayerCrawlingState : PlayerBaseState
{
    public PlayerCrawlingState(PlayerStateManager _currentContex, StateFactory playerStateFactory)
        : base(_currentContex, playerStateFactory) { }

    public override void EnterState()
    {
        Ctx._ani.SetBool(Ctx.Crawling, true);
    }
    public override void UpdateState()
    {
        CheckSwitchState();
        move();
    }
    public override void ExitState()
    {
        Ctx._ani.SetBool(Ctx.Crawling, false);
    }

    public override void CheckSwitchState()
    {
        if (Ctx.IsCrouching && !Ctx.IsMovementPress)
            SwitchState(Factory.Crouch());
        else if (!Ctx.IsCrouching && Ctx.IsMovementPress)
            SwitchState(Factory.Walk());
        else if(!Ctx.IsCrouching && Ctx.IsMovementPress)
            SwitchState(Factory.Idle());
    }
    public override void InitializeSubState()
    {
    }

    public void move()
    {
        Ctx.TimeRun += Time.deltaTime;
        Ctx.Speed = Ctx.speedCurve.Evaluate(Ctx.TimeRun);

        Ctx.SetRemapSpeedAniParemetter();
        if (!Ctx.IsHasWeapon)
        {
            Vector3 move = new Vector3(Ctx.DirNormalized.x, 0, Ctx.DirNormalized.y);
            Ctx._rb.velocity = move * Ctx.Speed * Ctx.crawlingSpeed;
        }
        if (Ctx._rb.velocity != Vector3.zero && !Ctx.IsHasWeapon)
            Ctx.transform.rotation = Quaternion.LookRotation(Ctx._rb.velocity);
        else if (Ctx.IsHasWeapon)
        {
            Ctx.SetRemapSpeedAniParemetter();
            Ctx._rb.velocity = Ctx.MoveVectorDir * Ctx.Speed * Ctx.crawlingSpeed;
        }
    }
}
