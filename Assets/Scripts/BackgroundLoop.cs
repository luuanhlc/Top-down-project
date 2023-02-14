using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
    public GameObject one;
    public GameObject two;
    public GameObject spwamOne;
    public GameObject spwamTwo;

    public float speed;

    private void Update()
    {
        one.transform.position += Vector3.right * speed * Time.deltaTime;
        two.transform.position += Vector3.right * speed * Time.deltaTime;
        if(one.transform.position.x < -42f)
            one.transform.position = spwamOne.transform.position + new Vector3(56.25f, 0f, 0f);
        if(two.transform.position.x < -42f)
            two.transform.position= spwamTwo.transform.position + new Vector3(56.25f, 0f, 0f);
    }
}
