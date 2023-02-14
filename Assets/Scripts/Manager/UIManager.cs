using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region Singleton
    public static UIManager Ins;
    private void Awake()
    {
        Ins = this;
    }
    #endregion

    public MainMenuGameUI mainMenuGameUI;
    public SettingUI settingUI;
    public PauseUI pauseUI;
    public MainGameUI mainGameUI;
}
