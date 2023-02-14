using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    //Infor
    [SerializeField]
    private ItemsType thisItemType;
    [SerializeField]
    private float health;
    [SerializeField]
    private int bullet;
    public float Health { get { return health; } }
    public int Bullet { get { return bullet; } }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {

            AudioSource.PlayClipAtPoint(GameManager.Ins.GetSoundManager.ItemEquit, this.transform.position);
            collision.gameObject.GetComponent<TakerItems>().TakeItem(this.thisItemType, this);

            Destroy(this.gameObject);
        }
    }
}

public enum ItemsType
{
    Medic,
    Bullet
}
