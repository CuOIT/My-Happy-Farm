using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartSceneController : MonoBehaviour
{

    public GameObject loadingScreen;
    public Slider progressBar;
    public IntData init;

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }


    public void GoGame()
    {
        if (init.Value == 0)
        {
            SetUpScene();
        }
        else
        {
            PlayScene();
        }
    }

    public string setUpScene;
    public string gameScene;
    public void SetUpScene()
    {
        LoadScene(setUpScene);
    }

    public void PlayScene()
    {
        LoadScene(gameScene);
    }
    private IEnumerator LoadSceneAsync(string sceneName)
    {
        loadingScreen.SetActive(true);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            progressBar.value = progress;
            if (operation.progress >= 0.9f)
            {


                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}