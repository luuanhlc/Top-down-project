using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamManager : MonoBehaviour
{
    public static CamManager Ins;
    private void Awake()
    {
        Ins = this;
    }

    public Camera SplashCam;
}
