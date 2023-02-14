using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakerItems : MonoBehaviour
{
    public void TakeItem(ItemsType type, Item item)
    {
        StartCoroutine(Equip(type, item));
    }

    IEnumerator Equip(ItemsType type, Item item)
    {
        yield return new WaitForSeconds(.7f);
        switch (type)
        {
            case ItemsType.Medic:
                AudioSource.PlayClipAtPoint(GameManager.Ins.GetSoundManager.Heal, this.transform.position);
                PlayerStateManager.Ins.Health = Mathf.Min(100f, PlayerStateManager.Ins.Health + item.Health);
                UIManager.Ins.mainGameUI.HealthChange(item.Health);
                break;
            case ItemsType.Bullet:
                GunStateManager.Ins.BulletLeft += item.Bullet;
                UIManager.Ins.mainGameUI.LeftBulletChange(item.Bullet);
                break;
        }
    }
}
