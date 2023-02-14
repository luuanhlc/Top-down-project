using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanLoopRun : MonoBehaviour
{
    public Transform beginPos;
    public Transform endPos;

    public float speed;

    private void Update()
    {
        this.transform.position += Vector3.right * speed * Time.deltaTime;
        if (this.transform.position.x <= endPos.position.x)
            this.transform.position = beginPos.position;
    }
}
