using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{

    //State variable
    PlayerBaseState currentPlayerState;
    StateFactory _states;
    WeaponIK weaponIK;
    WeaponModel weaponModel;
    RaycastHit attackHit;

    //getter setter
    public PlayerBaseState CurrentPlayerState
    {
        get { return currentPlayerState; } set { currentPlayerState = value; } 
    }

    #region Singleton
    public static PlayerStateManager Ins;
    private void Awake()
    {
        Init();

    }

    private void Init()
    {
        Ins = this;
        _states = new StateFactory(this);
        currentPlayerState = _states.Grounded();
        currentPlayerState.EnterState();
        weaponIK = GetComponentInChildren<WeaponIK>();
        weaponIK.AttackHand = true;

        GetWeapon();
    }

    public void GetWeapon()
    {
        weaponModel = GetComponentInChildren<WeaponModel>();
        if (weaponModel != null)
            _isHasWeapon = true;
    }

    #endregion;
    
    //Physic
    public Rigidbody _rb;
    public Animator _ani;
    [Header("LayerMask")]
    public LayerMask GroundLayer;

    [Header("AnimationCurve")]
    public AnimationCurve speedCurve;
    
    [Header("Run")]
    private float timeRun;
    private float speed;
    public float walkBaseSpeed;
    public float runBaseSpeed;
    public float rotationSpeed;
    public float crawlingSpeed;
    //Run Getter and Setter
    public float TimeRun { get { return timeRun; } set { timeRun = value; } }
    public float Speed { get { return speed; } set { speed = value; } }

    [Header("Jump")]
    private float timeJump;
    private float jumpPower;
    private int jumpLeft = 0;

    private bool _isGrounded;
    private bool _isJumpPress;
    private bool _isMovementPress;
    private bool _isNeedToResetJump = false;
    private bool _isRunPress = false;
    private bool _isHasWeapon = false;
    private bool _isAlive = true;
    private bool _isCrouching = false;
    private bool _isAttacking = false;
    private bool _cantShoot = false;
    private bool _isStoryteling = false;
    public float baseJumpPower;

    [Header("Input")]
    private float xInput;
    private float yInput;

    [Header("Health")]
    public float health = 100f;

    [Header("Particle")]
    [SerializeField] private GameObject bloodImpactEffectPerfab;

    // Jump Getter and Setter
    public float TimeJump { get { return timeJump; } set { timeJump = value; } }
    public float JumpPower { get { return jumpPower; } set { jumpPower = value; } }
    public int JumpLeft { get { return jumpLeft; } set { jumpLeft = value; } }
    public bool IsJumpPress { get { return _isJumpPress; } set { _isJumpPress = value;}}
    public bool IsMovementPress { get { return _isMovementPress; } set { _isMovementPress = value; } }
    public bool IsGrounded { get { return _isGrounded; } set { _isGrounded = value; } }
    public bool IsNeedToResetJump { get { return _isNeedToResetJump; } set { _isNeedToResetJump = value; } }
    public bool IsRunPress { get { return _isRunPress; } set { _isRunPress = value; } }
    public bool IsHasWeapon { get { return _isHasWeapon; } set { _isHasWeapon = value; } }
    public bool IsCrouching { get { return _isCrouching; } set { _isCrouching = value; } }
    public bool IsAttacking { get { return _isAttacking; } set { _isAttacking = value; } }
    public bool CantShoot { get {return _cantShoot; } set { _cantShoot = value; } }
    public bool IsStoryTeling { get { return _isStoryteling; } set { _isStoryteling = value; } }
    public float Health { get { return health; } set { health = value; } }
    public GameObject BloodImpactEffectPerfab { get { return bloodImpactEffectPerfab; } }
    public bool IsAlive { get { return _isAlive; }}
    public float XInput { get { return xInput; } }
    public float YInput { get { return yInput; } }
    public WeaponIK GetWeaponIK { get { return weaponIK; } }
    public WeaponModel GetWeaponModel { get { return weaponModel; } }

    [HideInInspector]
    private Vector3 moveVectorDir;
    private Vector2 dirNormalized;

    public Vector2 DirNormalized { get { return dirNormalized; } }
    public Vector3 MoveVectorDir { get { return moveVectorDir; } }
    [Header("Cam")]
    public Camera _cam;

    public float mouseSensitivity = 1f;

    //Animator parametter
    public string Idle;
    public string walking;
    public string running;
    public string jumping;
    public string speedParameter;
    public string HasRifle;
    public string xSpeed;
    public string ySpeed;
    public string remapXSpeed;
    public string remapYSpeed;
    public string shoot;
    public string Reload;
    public string Hit;
    public string Die;
    public string Crouching;
    public string Crawling;
    public string Attack;

    private void Start()
    {
        _ani.SetBool(HasRifle, _isHasWeapon);
    }

    private void Update()
    {
        _ani.SetFloat(speedParameter, _rb.velocity.magnitude);


        if (!_isAlive || _isStoryteling)
            return;

        currentPlayerState.UpdateStates();



        GroundCheck();
        //Input
        GetInput();
        SetSpeedAniParametter();
    }

    void GroundCheck()
    {
        if (Physics.Raycast(transform.position, -transform.up, 1.05f, GroundLayer))
        {
            _isGrounded = true;
            _isNeedToResetJump = false;
        }
        else
            _isGrounded = false;
    }

    void GetInput()
    {
        //Move Direction Input
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");
        moveVectorDir = Vector3.forward * yInput + Vector3.right * xInput;
        dirNormalized = new Vector2(moveVectorDir.x, moveVectorDir.z).normalized;
        //Movement Press
        if (_isGrounded)
            if (xInput != 0 || yInput != 0)
                _isMovementPress = true;
            else
                _isMovementPress = false;
        else
            _isMovementPress = false;

        //Run Press
        if (Input.GetKeyDown(KeyCode.LeftShift))
            _isRunPress = true;
        if (Input.GetKeyUp(KeyCode.LeftShift))
            _isRunPress = false;

        //Jump Press
        if (Input.GetKey(KeyCode.Space) && _isGrounded && !_isNeedToResetJump && !_isAttacking)
        {
            _isJumpPress = true;
            _isCrouching = false;
            Debug.Log(_isCrouching);

            _ani.SetTrigger(jumping);
        }

        //Crouching Press

        if(Input.GetKeyDown(KeyCode.Q))
        {
            _isCrouching = !_isCrouching;
            Debug.Log(_isCrouching);
        }
        //Debug.DrawRay(this.transform.position, this.transform.forward, Color.blue, 4f);
        //Attacking 
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            _isAttacking = true;

            if (Physics.Raycast(this.transform.position, this.transform.forward, out attackHit, 4f))
            {
                if (attackHit.transform.CompareTag("Enemy"))
                {
                    Debug.Log("Hit");
                    StartCoroutine(delayGiveDamage());
                }
            }
        }

    }

    IEnumerator delayGiveDamage()
    {
        yield return new WaitForSeconds(.35f);
    }

    void SetSpeedAniParametter()
    {
        _ani.SetFloat(xSpeed, Mathf.Abs(_rb.velocity.x));
        _ani.SetFloat(ySpeed, Mathf.Abs(_rb.velocity.y));
    }

    public void SetRemapSpeedAniParemetter()
    {
        Vector3 localMove = transform.InverseTransformDirection(moveVectorDir);

        // Z asix is forward

        _ani.SetFloat(remapXSpeed, localMove.x);
        _ani.SetFloat(remapYSpeed, localMove.z);
    }

    

    public void TakeHit(float Damage)
    {
        Health = Mathf.Max(0f, Health - Damage);

        if (Health == 0)
        {
            _isAlive = false;
            _ani.SetTrigger(Die);
        }
    }
}
