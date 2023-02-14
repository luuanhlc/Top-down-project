using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject door;
    public GameObject controlBox;

    public float maxHight;
    public float minHight;

    private bool _isOpen;
    private float _turnStateProses;
    public bool _isTurning;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);
        if (!other.gameObject.CompareTag("Player")) return;
        UIManager.Ins.mainGameUI.OpenDorUi.rectTransform.position = Camera.main.WorldToScreenPoint(controlBox.transform.position);
        UIManager.Ins.mainGameUI.OpenDorUi.gameObject.SetActive(true);
        PlayerStateManager.Ins.SetDoor = this;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        UIManager.Ins.mainGameUI.OpenDorUi.gameObject.SetActive(false);
        PlayerStateManager.Ins.SetDoor = null;
    }

    public IEnumerator OpenDoor()
    {
        _isTurning = true;
        _turnStateProses = 0;
        if (_isOpen)
        {
            for(int i = 0; i < 10; i++)
            {
                yield return new WaitForSeconds(.1f);
                _turnStateProses += .1f;
                door.transform.localPosition = Vector3.Lerp(door.transform.localPosition, new Vector3(door.transform.localPosition.x, minHight, door.transform.localPosition.z), _turnStateProses);
            }
        }
        else
        {
            for (int i = 0; i < 10; i++)
            {
                yield return new WaitForSeconds(.1f);
                _turnStateProses += .1f;
                door.transform.localPosition = Vector3.Lerp(door.transform.localPosition, new Vector3(door.transform.localPosition.x, maxHight, door.transform.localPosition.z), _turnStateProses);
            }
        }

        _isTurning = false;
        _isOpen = !_isOpen;
    }
}
