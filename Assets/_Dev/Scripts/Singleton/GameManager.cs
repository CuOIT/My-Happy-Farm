using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public CropTypeList plantListContainer;
    public ObjectPooler pooler;

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
}
