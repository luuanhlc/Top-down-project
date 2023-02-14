public abstract class PlayerBaseState 
{
    protected bool _isRootState = false;
    private PlayerStateManager _ctx;
    private StateFactory _factory;
    private PlayerBaseState _currentSuperState;
    private PlayerBaseState _currentSubState;

    //getter and setter
    public bool IsRootState { get { return _isRootState; } set { _isRootState = value; } }
    public PlayerStateManager Ctx { get { return _ctx; } }
    public StateFactory Factory { get { return _factory;} }
    public PlayerBaseState(PlayerStateManager currentContex, StateFactory stateFactory)
    {
        _ctx = currentContex;
        _factory = stateFactory;
    }

    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
    public abstract void CheckSwitchState();

    public abstract void InitializeSubState();
    public void UpdateStates()
    {
        UpdateState();
        UnityEngine.Debug.Log("Current State " + _ctx.CurrentPlayerState);
        UnityEngine.Debug.Log("Current super state " + _currentSuperState);
        UnityEngine.Debug.Log("Current sub state " + _currentSubState);
        if (_currentSubState != null)
        {
            _currentSubState.UpdateState();
        }
    }

    protected void SwitchState(PlayerBaseState newState)
    {
        ExitState();

        //Enter New State
        newState.EnterState();
        //UnityEngine.Debug.Log("This " + this + " Switch " + newState);
        //UnityEngine.Debug.Log(newState._isRootState + " new State " + newState);
        if (newState._isRootState)
        {
            _ctx.CurrentPlayerState = newState;
        }
        else if(_currentSuperState != null)
        {
            _currentSuperState.SetSubState(newState);
        }
    }

    protected void SetSuperState(PlayerBaseState newSuperState)
    {

        _currentSuperState = newSuperState;
    }

    protected void SetSubState(PlayerBaseState newSubState)
    {
        _currentSubState = newSubState;
        _currentSuperState = this;
        newSubState.SetSuperState(this);
    }
}
