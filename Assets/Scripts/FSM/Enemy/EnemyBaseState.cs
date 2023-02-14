public abstract class EnemyBaseState 
{
    EnemyStateManager _ctx;
    EnemyStateFactory _factory;

    public EnemyStateManager Ctx { get { return _ctx; } }
    public EnemyStateFactory Factory { get { return _factory; } }

    public EnemyBaseState(EnemyStateManager _currentContex, EnemyStateFactory _enemyFactory)
    {
        _ctx = _currentContex;
        _factory = _enemyFactory;
    }
    public abstract void EnterState();

    public abstract void UpdateState();

    public abstract void ExitState();

    public abstract void CheckSwitchState();

    protected void SwitchState(EnemyBaseState newState)
    {
        ExitState();

        newState.EnterState();
        _ctx.CurrentEnemyState = newState;
    }
}
