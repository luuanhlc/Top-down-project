using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseState : GameBaseState
{
    public override void EnterState(GameSateManager gameSate)
    {
        GameManager.Ins._Debug("Enter Pause State", DebugTag.GameSate);
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    public override void UpdateState(GameSateManager gameState)
    {
        GameManager.Ins._Debug("Update Pause State", DebugTag.GameSate);


    }
}
