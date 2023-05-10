/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(EnemySensor))]
public class SensorEditor : Editor
{
    private void OnDrawGizmos()
    {
        EnemySensor sensor = (EnemySensor)target;
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(sensor.transform.position, sensor.viewRadius);

        Vector3 viewAngle01 = DirectionFromAngle(sensor.TF_viewPos.transform.eulerAngles.y, - sensor.viewAngle / 2);
        Vector3 viewAngle02 = DirectionFromAngle(sensor.TF_viewPos.transform.eulerAngles.y, sensor.viewAngle / 2);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(sensor.TF_viewPos.transform.position, sensor.TF_viewPos.transform.position + viewAngle01 * sensor.viewRadius);
        Gizmos.DrawLine(sensor.TF_viewPos.transform.position, sensor.TF_viewPos.transform.position + viewAngle02 * sensor.viewRadius);

        if (sensor._isSee)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(sensor.transform.position, sensor.target.transform.position);
        }
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
*/