using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponIK : MonoBehaviour
{
    public Animator _ani;

    public Transform rightHandTarget;
    public Transform leftHandTarget;

    public bool AttackHand;


    private void OnAnimatorIK(int layerIndex)
    {
        if (!_ani)
            return;
        if (AttackHand)
        {
            if (leftHandTarget != null)
            {
                AttachHandToHandle(AvatarIKGoal.LeftHand, leftHandTarget);

                _ani.SetLookAtWeight(1f);
                _ani.SetLookAtPosition(leftHandTarget.position);
            }
        }
        else
            DetachHandFromHandle(AvatarIKGoal.LeftHand);
    }

    protected void AttachHandToHandle(AvatarIKGoal hand, Transform handle)
    {
        _ani.SetIKPositionWeight(hand, 1);
        _ani.SetIKRotationWeight(hand, 1);

        _ani.SetIKPosition(hand, handle.position);
        _ani.SetIKRotation(hand, handle.rotation);
    }

    protected virtual void DetachHandFromHandle(AvatarIKGoal hand)
    {
        _ani.SetIKPositionWeight(hand, 0);
        _ani.SetIKRotationWeight(hand, 0);
        _ani.SetLookAtWeight(0);
    }
}
