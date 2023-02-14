using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainGameUI : MonoBehaviour
{
    [Header("Health bar")]
    [SerializeField] private Image fristHealthBar;
    [SerializeField] private Image secondHealthBar;

    [Header("Bullet")]
    [SerializeField] private TextMeshProUGUI currentBullet;
    [SerializeField] private TextMeshProUGUI leftBullet;

    [SerializeField] public Image OpenDorUi;

    private int visibleHealth;
    private int currentHealth;
    public Texture2D _cursor;

    private void OnEnable()
    {
        visibleHealth = (int)PlayerStateManager.Ins.Health;
        Init();
    }

    void Init()
    {
        hasGun(PlayerStateManager.Ins.IsHasWeapon);

        fristHealthBar.fillAmount = PlayerStateManager.Ins.Health / 100f;
        secondHealthBar.fillAmount = fristHealthBar.fillAmount;
    }

    private void hasGun(bool isHasGun)
    {
        currentBullet.gameObject.SetActive(isHasGun);
        leftBullet.gameObject.SetActive(isHasGun);

        if(isHasGun )
        {
            currentBullet.text = GunStateManager.Ins.MaxBullet.ToString();
            leftBullet.text = GunStateManager.Ins.BulletLeft.ToString();
        }
    }
    public void HealthChange(float health)
    {
        Debug.Log(health);
        int step = (int)health;
        StartCoroutine(HandleHealthChangeUI(step));
    }

    IEnumerator HandleHealthChangeUI(int step)
    {
        for(int i = 0; i < Mathf.Abs(step); i++)
        {
            Debug.Log(i);
            yield return new WaitForSeconds(.01f);
            fristHealthBar.fillAmount += .01f * Mathf.Abs(step)/step;
        }
        StartCoroutine(SecondHealthBarChangeUI(step));
    }

    IEnumerator SecondHealthBarChangeUI(int step)
    {
        for (int i = 0; i < step; i++)
        {
            yield return new WaitForSeconds(.01f);
            secondHealthBar.fillAmount -= .01f;
        }
    }

    public void CurrentBulletChange(int bulletChange)
    {

        GunStateManager.Ins.CurrentBullet = Mathf.Max(0, GunStateManager.Ins.CurrentBullet + bulletChange);
        currentBullet.text = GunStateManager.Ins.CurrentBullet.ToString();
    }

    public void LeftBulletChange(int addBullet)
    {
        StartCoroutine(HandleBullet(addBullet));
    }

    IEnumerator HandleBullet(int bullet)
    {
        for(int i = 0; i < Mathf.Abs(bullet); i++)
        {
            yield return new WaitForSeconds(.01f);
            GunStateManager.Ins.BulletLeft += Mathf.Abs(bullet)/bullet;
            leftBullet.text = GunStateManager.Ins.BulletLeft.ToString();
        }
    }
}
