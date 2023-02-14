using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiSensor : MonoBehaviour
{
    [SerializeField] private float radius;
    [Range(0, 360)]
    [SerializeField] private float angle;

    public GameObject playerRef;

    public GameObject lookPos;
    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeePlayer;

    public float Radius { get { return radius; } }
    public float Angle { get { return angle; } }

    private void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());
    }

    IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(lookPos.transform.position, radius, targetMask);

        if(rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionTOTarget = (target.position - lookPos.transform.position).normalized;

            if(Vector3.Angle(lookPos.transform.forward, directionTOTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(lookPos.transform.position, directionTOTarget, distanceToTarget, obstructionMask))
                    canSeePlayer = true;
                else 
                    canSeePlayer = false;
            }
            else
            {
                canSeePlayer = false;
            }
        }
        else if(canSeePlayer)
            canSeePlayer = false;
    }

}
