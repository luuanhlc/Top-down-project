public class StateFactory
{
    PlayerStateManager _contex;

    public StateFactory(PlayerStateManager currentContex)
    {
        _contex = currentContex;
    }

    public PlayerBaseState Idle()
    {
        return new PlayerIdleState(_contex, this);
    }
    public PlayerBaseState Run()
    {
        return new PlayerRunState(_contex, this);
    }
    public PlayerBaseState Jump()
    {
        return new PlayerJumpState(_contex, this);
    }
    public PlayerBaseState Grounded()
    {
        return new PlayerGroundedState(_contex, this);
    }
    public PlayerBaseState Walk()
    {
        return new PlayerWalkState(_contex, this);
    }
    public PlayerBaseState Crouch()
    {
        return new PlayerCrouchState(_contex, this);
    }
    public PlayerBaseState Crawl()
    {
        return new PlayerCrawlingState(_contex, this);
    }

    public PlayerBaseState Attack()
    {
        return new PlayerAttackState(_contex, this);
    }


}
