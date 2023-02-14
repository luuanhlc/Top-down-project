using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressAnim : MonoBehaviour
{
    public float freg;
    public float amp;
    Vector3 intiPos;

    private void OnEnable()
    {
        intiPos = transform.position;
    }

    private void Update()
    {
        transform.position = new Vector3(intiPos.x, Mathf.Sin(Time.time * freg) * amp + intiPos.y, intiPos.z);
    }
}
