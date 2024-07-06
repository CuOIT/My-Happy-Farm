using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public ObjectPooler pooler;
    
    public LevelController levelController;
    public MoneyController moneyController;

    public static GameManager Instance
    {
        get{
            if(_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                if(_instance == null )
                {
                    GameObject newGO = new GameObject("GameManager");
                    newGO.AddComponent<GameManager>();  
                }
            }
            return _instance;
        }
        private set { }
    }

    public void Start()
    {
        pooler = GetComponent<ObjectPooler>();
    }
    public void Awake()
    {
        if(_instance != null && _instance!=this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            _instance = this;
        }
    }

    [Button]
    public void Notice(string message,float duration = 3)
    {
        StartCoroutine(Notices(message,duration));
    }
    IEnumerator Notices(string  message,float duration)
    {
        GameObject go = pooler.SpawnFromPool("MESSAGE", Vector3.zero, Quaternion.identity);
        Notice notice = go.GetComponent<Notice>();
        notice.ShowMessage(message);
        yield return new WaitForSeconds(duration);
        pooler.DespawnObject(go);
    }

    public GameObject loadingScreen;
    public Slider progressBar;

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
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
