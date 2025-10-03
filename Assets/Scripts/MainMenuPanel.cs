using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuPanel : MonoBehaviour
{
    public Button playButton;
    public Button quitButton;

    private void OnEnable()
    {
        playButton.onClick.AddListener(OnPlayButtonClicked);
        quitButton.onClick.AddListener(OnQuitButtonClicked);

#if UNITY_WEBGL
        quitButton.gameObject.SetActive(false);
#endif
    }

    private void OnPlayButtonClicked()
    {
        SceneUtils.LoadGameScene();
    }

    private void OnQuitButtonClicked()
    {
        SceneUtils.QuitApplication();
    }
}
