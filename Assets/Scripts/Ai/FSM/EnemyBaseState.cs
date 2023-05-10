public abstract class EnemyBaseState
{
    protected bool _isRootState = false;
    private EnemyStateManager _ctx;
    private EnemyStateFactory _factory;
    
    private EnemyBaseState _currentSubState;
    private EnemyBaseState _currentSuperState;
    
    public bool IsRootState { get { return _isRootState; } set { _isRootState = value; } }
    public EnemyStateManager Ctx { get { return _ctx; } }
    public EnemyStateFactory Factory { get { return _factory; } }

    public EnemyBaseState(EnemyStateManager currentContex, EnemyStateFactory _stateFactory)
    {
        _ctx = currentContex;
        _factory = _stateFactory;
    }

    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
    public abstract void CheckSwitchState();

    public abstract void InitializeSubState();
    public void UpdateStates()
    {
        UpdateState();
        if (_currentSubState != null)
        {
            _currentSubState.UpdateState();
        }
    }

    protected void SwitchState(EnemyBaseState newState)
    {
        ExitState();

        //Enter New State
        newState.EnterState();
        if (newState._isRootState)
        {
            _ctx.CurrentEnemyState = newState;
        }
        else if (_currentSuperState != null)
        {
            _currentSuperState.SetSubState(newState);
        }
    }

    protected void SetSuperState(EnemyBaseState newSuperState)
    {

        _currentSuperState = newSuperState;
    }

    protected void SetSubState(EnemyBaseState newSubState)
    {
        _currentSubState = newSubState;
        _currentSuperState = this;
        newSubState.SetSuperState(this);
    }
}