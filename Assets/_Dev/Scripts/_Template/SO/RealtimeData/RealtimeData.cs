using _Template.Script.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Test",menuName ="Test")]
public class RealtimeData<T> : ScriptableObject,IData<T> where T : struct
{
    public T Value { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public void LoadData()
    {
        throw new System.NotImplementedException();
    }

    public void SaveData()
    {
        throw new System.NotImplementedException();
    }
}

