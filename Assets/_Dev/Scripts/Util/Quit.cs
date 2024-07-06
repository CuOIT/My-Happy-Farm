using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Quit : MonoBehaviour
{
    public string quitScene;

    public void QuitGame()
    {
        if (string.IsNullOrEmpty(quitScene))
        {
            Application.Quit();
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }
}
