using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{
    [SerializeField] private Animator _ani;

    EnemyBaseState _currentEnemyState;
    EnemyStateFactory _enmyStateFactory;

    [SerializeField] private Transform sentinelPosition;

    public EnemyBaseState CurrentEnemyState { get { return _currentEnemyState; } set { _currentEnemyState = value; } }

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _enmyStateFactory = new EnemyStateFactory(this);
        _currentEnemyState = _enmyStateFactory.Idle();
        _currentEnemyState.EnterState();
    }

    private float health = 100f;
    private bool _isAlive = true;
    private bool _foundPlayer = false;
    private bool _isMovement = false;
    private bool _isRun = false;
    private bool _canAttack = false;
    public float Health { get { return health; } set { health= value; } }
    public bool IsAlive { get { return _isAlive;} }
    public bool FoundPlayer { get { return _foundPlayer; } set { _foundPlayer = value; } }
    public bool IsMovement { get { return _isMovement; } set { _isMovement = value; } }
    public bool CanAttack { get { return _canAttack; } set { _canAttack = value; } }
    public bool Runing { get { return _isRun; } }
    public Vector3 SentinelPos { get { return sentinelPosition.position; } }
    //Animator Parametter
    public string Idle;
    public string Walk;
    public string Run;
    public string Death;



    private void Update()
    {
        if (!_isAlive)
            return;

        _currentEnemyState.UpdateState();
    }


    public void TakeDamage(float Damage)
    {
        _ani.SetTrigger("Hit");
        health = Mathf.Max(health - Damage, 0f);
        Debug.Log(this.gameObject.name + " health: " + health);
        if(health == 0)
        {
            _isAlive = false;
            _ani.SetTrigger(Death);
        }

    }
}
