using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class EventTrigger : MonoBehaviour
{
    private bool[] _isActived;
    public CinemachineVirtualCamera Camera;
    public List<Trigger> triggers = new List<Trigger>();
    float time = 0f;
    bool _isRuning;

    private void Awake()
    {
        _isActived = new bool[triggers.Count];
    }

    private void OnTriggerEnter(Collider other)
    {
        if(_isRuning) return;
        if(other.gameObject.CompareTag("Player"))
            RunEvent();
    }

    private void RunEvent()
    {
        if (triggers.Count == 0) return;
        Debug.Log("Event");
        _isRuning = true;
        PlayerStateManager.Ins.IsStoryTeling = true;
        Camera.gameObject.SetActive(true);
        //Camera.gameObject.transform.position = triggers[0].onPos;
        StartCoroutine(moveCam(triggers[0]));
    }
    IEnumerator moveCam(Trigger trigger)
    {
        yield return new WaitForSeconds(2f);
        /*for(int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(.01f);
            Debug.Log(i);
            Debug.Log(time);
            time += .1f;
            Camera.gameObject.transform.position = Vector3.Lerp(trigger.onPos, trigger.offPos, time);
            Camera.gameObject.transform.rotation = Quaternion.Lerp(Quaternion.identity, Quaternion.Euler(trigger.aimPos.x, trigger.aimPos.y, trigger.aimPos.z), time);
        }*/
        Camera.gameObject.SetActive(false);
        PlayerStateManager.Ins.IsStoryTeling = false;
        _isRuning = false;

        triggers.Remove(trigger);
    }
}

[System.Serializable]
public class Trigger
{
    public GameObject triggerBox;

    public Vector3 onPos;
    public Vector3 offPos;
    public Vector3 aimPos;

    public float targetFOV;
}
