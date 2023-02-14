using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameState : GameBaseState
{
    public override void EnterState(GameSateManager gameSate)
    {
        GameManager.Ins._Debug("Enter Main Game State", DebugTag.GameSate);

        CamManager.Ins.SplashCam.gameObject.SetActive(false);
        UIManager.Ins.mainMenuGameUI.gameObject.SetActive(false);
        Cursor.SetCursor(UIManager.Ins.mainGameUI._cursor, Vector2.zero, CursorMode.Auto);
    }

    public override void UpdateState(GameSateManager gameState)
    {
        //corshai follow mouse
    }
}
