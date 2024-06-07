using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartSceneScript : MonoBehaviour
{

    [SerializeField] private Slider slider;
    [SerializeField] private float fakeTimeLoad;
                     private float sliderValue=0;
    void Start()
    {
        StartCoroutine(LoadSceneAsync());
    }



    IEnumerator LoadSceneAsync()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(1);
        op.allowSceneActivation = false;

        while (!op.isDone)
        {
            if (op.progress >= 0.9f && sliderValue >= 1 && !op.allowSceneActivation)
            {
                yield return new WaitForSeconds(0.5f);
                op.allowSceneActivation = true;
                yield break;
            }
            else
            {
                sliderValue += Time.deltaTime / fakeTimeLoad;
                if (sliderValue > 1) sliderValue = 1;
                slider.value = sliderValue;
            }
            yield return null;
        }
    }
}
