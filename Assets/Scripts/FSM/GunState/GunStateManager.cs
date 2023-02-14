using UnityEngine;
using System.Collections;

public class GunStateManager : MonoBehaviour
{
    public static GunStateManager Ins;
    
    PlayerStateManager playerState;

    WeaponIK weaponIK;
    GunBaseState currentGunState;
    GunStateFactory _gunState;

    private int bulletLeft = 100;
    private int maxBullet = 45;
    private int currentBullet;
    private bool _isFirePress = false;
    private bool _isShooting = false;

    [SerializeField]
    //private Light light;

    public GunBaseState CurrentGunState { get { return currentGunState; } set { currentGunState = value; } }
    public int MaxBullet { get { return maxBullet; }}
    public int CurrentBullet { get { return currentBullet; } set { currentBullet = value; } }
    public bool IsFirePress { get { return _isFirePress; } set { _isFirePress = value; } }
    public PlayerStateManager PlayerState { get { return playerState; } }
    public Light Light { get { return GetComponent<Light>(); } }
    public bool IsShooting { get { return _isShooting; } set { _isShooting = value; } }
    public int BulletLeft { get { return bulletLeft; } set { bulletLeft = value; } }
    public WeaponIK GetWeaponIK { get { return weaponIK; } }


    private void Awake()
    {
        Ins = this;
    }

    private void Init()
    {
        _gunState = new GunStateFactory(this);
        currentGunState = _gunState.Ready();
        currentGunState.EnterState();

        currentBullet = maxBullet;

        weaponIK = GetComponentInChildren<WeaponIK>();
    }

    private void Start()
    {
        playerState = PlayerStateManager.Ins;
        Init();

    }

    private void Update()
    {
        GetInput();

        currentGunState.UpdateState();
    }

    private void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && currentBullet > 0 && !playerState.CantShoot)
        {
            _isFirePress = true;
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            _isFirePress = false;
        }
    }

    [Range(0f, 1f)]
    public float lerpTime;
    public Color baseColor;
    public Color endColor;

    private void ShottingLight()
    {
        lerpTime += Mathf.Sin(Time.time * 3f);
        GetComponent<Light>().color = Color.Lerp(baseColor, endColor, lerpTime );
        GetComponent<Light>().intensity = Mathf.Lerp(1f, 10f, lerpTime);
        GetComponent<Light>().range = Mathf.Lerp(10f, 20f, lerpTime);

        //bloom 

        //Vignette

        //add smal than .1 DOF

        //Chromatic < .45
    }
}
