using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuGameUI : MonoBehaviour
{
    public Button playButton;
    public Button settingButton;
    public Button exitButton;

    public GameObject loadingInterFace;
    public Image loadingProgressBar;

    List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        playButton.onClick.AddListener(OnPlayClick);
        settingButton.onClick.AddListener(OnSettingClick);
        exitButton.onClick.AddListener(OnExitClick);
    }

    IEnumerator LoadingSceen()
    {
        float totalProgressBar = 0f;
        for(int i = 0; i < scenesToLoad.Count; i++)
        {
            while (!scenesToLoad[i].isDone)
            {
                totalProgressBar += scenesToLoad[i].progress;
                loadingProgressBar.fillAmount = totalProgressBar / scenesToLoad.Count;

                if (i == scenesToLoad.Count - 1)
                {
                    GameSateManager.Ins.ChangeGameState(GameState.mainGame);
                }
                yield return null;
            }
        }
    }

    private void OnPlayClick()
    {
        GameManager.Ins._Debug("PlayClick", DebugTag.UI);

        loadingInterFace.SetActive(true);
        SceneManager.UnloadScene(2);
        scenesToLoad.Add(SceneManager.LoadSceneAsync("TestScript", LoadSceneMode.Additive));

        StartCoroutine(LoadingSceen());
    }

    private void OnSettingClick()
    {
        GameManager.Ins._Debug("SettingClick", DebugTag.UI);

        UIManager.Ins.settingUI.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }

    private void OnExitClick()
    {
        GameManager.Ins._Debug("ExitClick", DebugTag.UI);

        Application.Quit();
    }
}
