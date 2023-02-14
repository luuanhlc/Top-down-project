using UnityEngine;
using System.Collections;

public class AnimationEvent : MonoBehaviour
{
    PlayerStateManager playerState;
    GunStateManager gunState;

    WeaponModel weaponModel;
    private void Start() 
    {
        gunState = GunStateManager.Ins;
        playerState = PlayerStateManager.Ins;
    }



    public void Shoot()
    {
        playerState.GetWeaponModel.shootParticle.Play();
        StartCoroutine(SpawmBullet());
        //gunState.Light.gameObject.SetActive(true);
    }

    IEnumerator SpawmBullet()
    {
        for(int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(.085f);
            AudioSource.PlayClipAtPoint(playerState.GetWeaponModel.shootingAudioClip, playerState.GetWeaponModel.transform.position);
            if(UIManager.Ins)
                UIManager.Ins.mainGameUI.CurrentBulletChange(-1);
            playerState.GetWeaponModel.CheckTarget();
            

            if (GunStateManager.Ins.CurrentBullet == 0)
                AudioSource.PlayClipAtPoint(playerState.GetWeaponModel.reloadNeedAudioClip, playerState.GetWeaponModel.transform.position);
            
            if(i == 2)
                GunStateManager.Ins.IsShooting = false;
        }
    }
    public void EndShoot()
    {
        playerState._ani.SetBool(playerState.shoot, false);
        gunState.IsFirePress = false;
    }

    public void Reloading()
    {
        AudioSource.PlayClipAtPoint(playerState.GetWeaponModel.reloadAudioClip, playerState.GetWeaponModel.transform.position);
    }

    public void ReloadDone()
    {
        int LeftAfterReload = gunState.BulletLeft - gunState.MaxBullet;

        if(LeftAfterReload < 0)
        {
            UIManager.Ins.mainGameUI.LeftBulletChange(-gunState.BulletLeft);
            UIManager.Ins.mainGameUI.CurrentBulletChange(gunState.BulletLeft);
        }
        else
        {
            UIManager.Ins.mainGameUI.LeftBulletChange(-gunState.MaxBullet);
            UIManager.Ins.mainGameUI.CurrentBulletChange(gunState.MaxBullet);
        }

        playerState.GetWeaponIK.AttackHand = true;
    }

    public void EndAttack()
    {

        playerState.IsAttacking = false;
    }
}
