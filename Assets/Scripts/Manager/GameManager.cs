using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{

    [Header("Manager")]
    [SerializeField] SoundManager soundManager;

    List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();
    public Image loadingProgressBar;
    public GameObject background;

    public SoundManager GetSoundManager { get { return soundManager; } }

    public Color colorUI;
    public Color colorGameState;
    public Color colorPlayerState;

    private Color crrColor;

    #region Singleton
    public static GameManager Ins;

    private void Awake()
    {
        Ins = this;
        Init();
    }
    #endregion

    private void Init()
    {
        UIManager.Ins.mainMenuGameUI.gameObject.SetActive(true);

        scenesToLoad.Add(SceneManager.LoadSceneAsync("Looby", LoadSceneMode.Additive));
        StartCoroutine(LoadingSceen());

    }

    IEnumerator LoadingSceen()
    {
        Debug.Log("load");
        float totalProgressBar = 0f;
        for (int i = 0; i < scenesToLoad.Count; i++)
        {
            while (!scenesToLoad[i].isDone)
            {
                totalProgressBar += scenesToLoad[i].progress;
                loadingProgressBar.fillAmount = totalProgressBar / scenesToLoad.Count;

                if (i == scenesToLoad.Count - 1)
                {
                    background.gameObject.SetActive(false);
                }
                yield return null;
            }
        }
    }

    public void _Debug(string message, DebugTag debugTag)
    {
        switch (debugTag)
        {
            case DebugTag.GameSate:
                crrColor = colorGameState;
                break;
            case DebugTag.UI:
                crrColor = colorUI;
                break;
            case DebugTag.PlayerState:
                crrColor = colorPlayerState;
                break;
        }
        Debug.Log(string.Format("<color=#{0:X2}{1:X2}{2:X2}>{3}</color>", (byte)(crrColor.r * 255f), (byte)(crrColor.g * 255f), (byte)(crrColor.b * 255f), message));
    }
}
public enum DebugTag{
    UI,
    GameSate,
    PlayerState
}
