using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerBaseState
{
    public PlayerRunState(PlayerStateManager _currentState, StateFactory stateFactory) : base(_currentState, stateFactory) { }

    public override void EnterState()
    {
        //GameManager.Ins._Debug("Enter Run State", DebugTag.PlayerState);
        Ctx._ani.SetBool(Ctx.running, true);
    }

    public override void UpdateState()
    {
        CheckSwitchState();
        move();
    }

    public override void ExitState()
    {
        Ctx._ani.SetBool(Ctx.running, false);
    }
    public override void CheckSwitchState()
    {
        if (!Ctx.IsMovementPress)
            SwitchState(Factory.Idle());
        else if (!Ctx.IsRunPress)
            SwitchState(Factory.Walk());
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
            Ctx._rb.velocity = move * Ctx.Speed * Ctx.runBaseSpeed;
        }
        if (Ctx._rb.velocity != Vector3.zero && !Ctx.IsHasWeapon)
            Ctx.transform.rotation = Quaternion.LookRotation(Ctx._rb.velocity);
        else if (Ctx.IsHasWeapon)
        {
            Ctx.SetRemapSpeedAniParemetter();
            Ctx._rb.velocity = Ctx.MoveVectorDir * Ctx.Speed * Ctx.runBaseSpeed;
        }
    }
}
