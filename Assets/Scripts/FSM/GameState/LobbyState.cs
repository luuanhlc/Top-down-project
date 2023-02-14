using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyState : GameBaseState
{
    public override void EnterState(GameSateManager gameSate)
    {
        GameManager.Ins._Debug("Enter Lobby State", DebugTag.GameSate);

        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    public override void UpdateState(GameSateManager gameState)
    {
    }
}
