using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    public PlayerJumpState(PlayerStateManager _currentState, StateFactory stateFactory) 
        : base (_currentState, stateFactory)
        { IsRootState = true; InitializeSubState(); }

    public override void EnterState()
    {
        Ctx.IsNeedToResetJump = true;
        Ctx._ani.SetBool(Ctx.Idle, false);
        Ctx._ani.SetBool(Ctx.Crouching, false);
        Ctx._ani.SetBool(Ctx.Crawling, false);

        if (Ctx.GetWeaponIK != null)
            Ctx.GetWeaponIK.AttackHand = false;
        HandleJump();

    }

    public override void UpdateState()
    {
        CheckSwitchState();
    }

    public override void ExitState()
    {
        //GameManager.Ins._Debug("Exit Jump State", DebugTag.PlayerState);
        Ctx.JumpLeft -= 1;
    }

    public override void CheckSwitchState()
    {
        if (Ctx.IsGrounded)
            SwitchState(Factory.Grounded());
    }
    public override void InitializeSubState()
    {
        if (!Ctx.IsMovementPress)
        {
            SetSubState(Factory.Idle());
        }
        else
            SetSubState(Factory.Run());
    }

    private void HandleJump()
    {
        Ctx.IsJumpPress = false;
        Ctx._rb.velocity = new Vector3(Ctx._rb.velocity.x, 0, Ctx._rb.velocity.z);
        Ctx._rb.AddForce(Vector3.up * Ctx.baseJumpPower);
    }
}
