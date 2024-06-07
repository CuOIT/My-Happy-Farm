using _Template.Script.Data;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalData<T> : ScriptableObject, IData<T> 
{
    [SerializeField]protected    string Name;
                      //  bool loaded = false;
    [SerializeField] protected T defaultValue;

    private bool loaded;
    private T _value;
    public T Value
    {
        get
        {
            if (!loaded) LoadData();
            return _value;
        }
        set
        {
            _value = value;
            SaveData();
        }
    }

    public void OnEnable()
    {
            LoadData();
            Debug.Log(Name+ "Load");
           // LoadData();
    }

    [ContextMenu("ShowValue")]
    public void ShowValue()
    {
        Debug.Log(Value);
    }
    
    protected virtual void Init()
    {
        Value = defaultValue;
    }
    public void LoadData()
    {
        if (string.IsNullOrEmpty(Name)) return;
        string data = PlayerPrefs.GetString(Name,"NULL");
        if (data =="NULL")
        {
            Init();
        }
        else
        {
            _value = JsonConvert.DeserializeObject<T>(data);
        }
        loaded= true;
    }

    public void SaveData()
    {
        string data = JsonConvert.SerializeObject(_value);
        PlayerPrefs.SetString(Name, data);
        loaded = false;
    }
}
