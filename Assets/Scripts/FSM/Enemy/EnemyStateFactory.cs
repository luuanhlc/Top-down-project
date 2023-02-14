public class EnemyStateFactory 
{
    EnemyStateManager _contex;

    public EnemyStateFactory(EnemyStateManager contex)
    {
        _contex = contex;
    }

    public EnemyBaseState Idle()
    {
        return new EnemyIdleState(_contex, this);
    }

    public EnemyBaseState Walk()
    {
        return new EnemyWalkState(_contex, this);
    }

    public EnemyBaseState Run()
    {
        return new EnemyRunState(_contex, this);
    }
    public EnemyBaseState Attack()
    {
        return new EnemyAttackState(_contex, this);
    }
}
