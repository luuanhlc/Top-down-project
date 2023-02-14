
using UnityEngine;

public abstract class GameBaseState
{
    public abstract void EnterState(GameSateManager gameState);

    public abstract void UpdateState(GameSateManager gameState);

}
