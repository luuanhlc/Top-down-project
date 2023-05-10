using Spine.Unity.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySensor : MonoBehaviour
{
    public float viewRadius;
    public float viewAngle;
    public float catchRangle;

    public float waitToFind = .2f;

    public LayerMask targetMask;
    public LayerMask ObstrackMask;
    public Transform TF_viewPos;

    private void Start()
    {
        StartCoroutine(FOVRoutine());
        target = PlayerStateManager.Ins.gameObject;
    }

    private IEnumerator FOVRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(waitToFind);
            FieldOfViewCheck();
        }
    }
    public bool _isSee;
    public bool _isCanCatch;
    public GameObject target;
    private void FieldOfViewCheck()
    {
        Collider[] hit = Physics.OverlapSphere(TF_viewPos.position, viewRadius, targetMask);

        if (hit.Length != 0)
        {
            Transform target = hit[0].transform;
            if(PlayerStateManager.Ins.IsShield || PlayerStateManager.Ins.IsHide || PlayerStateManager.Ins.IsInBox)
            {
                _isSee = false;
                _isCanCatch = false;
                return;
            }
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(TF_viewPos.forward, directionToTarget) < viewAngle / 2)
            {
                float distance = Vector3.Distance(transform.position, target.position);
                if (distance < viewRadius)
                {
                    if (!Physics.Raycast(this.TF_viewPos.position, directionToTarget, distance, ObstrackMask))
                    {
                        if(Vector3.Distance(transform.position, target.position) <= catchRangle)
                        {
                            _isCanCatch = true;
                        }
                        _isSee = true;
                    }
                    else
                        _isSee = false;
                }
            }
            else
                _isSee = false;

        }
        else if (_isSee)
            _isSee = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, viewRadius);

        Vector3 viewAngle01 = DirectionFromAngle(TF_viewPos.transform.eulerAngles.y, -viewAngle / 2);
        Vector3 viewAngle02 = DirectionFromAngle(TF_viewPos.transform.eulerAngles.y, viewAngle / 2);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(TF_viewPos.transform.position, TF_viewPos.transform.position + viewAngle01 * viewRadius);
        Gizmos.DrawLine(TF_viewPos.transform.position, TF_viewPos.transform.position + viewAngle02 * viewRadius);

        if (_isSee)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(transform.position, target.transform.position);
        }
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
