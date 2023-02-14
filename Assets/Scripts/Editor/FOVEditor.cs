using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AiSensor))]
public class FOVEditor : Editor
{
    private void OnSceneGUI()
    {
        AiSensor sensor = (AiSensor)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(sensor.lookPos.transform.position, Vector3.up, Vector3.forward, 360, sensor.Radius);

        Vector3 viewAngle01 = DirectionFromAngle(sensor.lookPos.transform.eulerAngles.y, -sensor.Angle / 2);
        Vector3 viewAngle02 = DirectionFromAngle(sensor.lookPos.transform.eulerAngles.y, sensor.Angle / 2);

        Handles.color = Color.yellow;
        Handles.DrawLine(sensor.lookPos.transform.position, sensor.lookPos.transform.position + viewAngle01 * sensor.Radius);
        Handles.DrawLine(sensor.lookPos.transform.position, sensor.lookPos.transform.position + viewAngle02 * sensor.Radius);

        if (sensor.canSeePlayer)
        {
            Handles.color = Color.green;
            Handles.DrawLine(sensor.lookPos.transform.position, sensor.playerRef.transform.position);
        }
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDeree)
    {
        angleInDeree += eulerY;

        return new Vector3(Mathf.Sin(angleInDeree * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDeree * Mathf.Deg2Rad));
    }
}
