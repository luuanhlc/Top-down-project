using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : PlayerBaseState
{
    public PlayerWalkState(PlayerStateManager _currentState, StateFactory stateFactory) : base(_currentState, stateFactory) { }

    public override void EnterState()
    {
        Ctx._ani.SetBool(Ctx.walking, true);
    }

    public override void UpdateState()
    {
        CheckSwitchState();
        move();
    }

    public override void ExitState()
    {
        Ctx._ani.SetBool(Ctx.walking, false);
    }
    public override void CheckSwitchState()
    {
        if (!Ctx.IsMovementPress)
            SwitchState(Factory.Idle());
        else if (Ctx.IsRunPress)
            SwitchState(Factory.Run());
        else if (Ctx.IsMovementPress && Ctx.IsCrouching)
            SwitchState(Factory.Crawl());
        else if (Ctx.IsAttacking)
            SwitchState(Factory.Attack());
    }
    public override void InitializeSubState()
    {

    }
    public void move()
    {
        Ctx.TimeRun += Time.deltaTime;
        Ctx.Speed = Ctx.speedCurve.Evaluate(Ctx.TimeRun);


        Vector3 move = new Vector3(Ctx.DirNormalized.x, 0, Ctx.DirNormalized.y);
        Ctx._rb.velocity = move * Ctx.Speed * Ctx.walkBaseSpeed;

        if (Ctx._rb.velocity != Vector3.zero && !Ctx.IsHasWeapon)
            Ctx.transform.rotation = Quaternion.LookRotation(Ctx._rb.velocity);
        else if(Ctx.IsHasWeapon)
            Ctx.SetRemapSpeedAniParemetter();
    }
}
