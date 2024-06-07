/*using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName ="NewParcelsData",menuName ="Data/ParcelsData")]
public class ParcelsData : LocalData<Dictionary<string,int>>
{
    [SerializeField] List<ParcelInfoSO> parcelInfos;
    [SerializeField] string folderPath;

    protected override void Init()
    {
        defaultValue = new();
        parcelInfos = new List<ParcelInfoSO>();
        string[] guids = AssetDatabase.FindAssets("t:ScriptableObject", new[] { folderPath });
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            ParcelInfoSO instance = AssetDatabase.LoadAssetAtPath<ParcelInfoSO>(path);
            // Do something with the instance
            if(instance != null)
            {
                if (!string.IsNullOrEmpty(instance.id))
                {
                    if(!defaultValue.ContainsKey(instance.id)) 
                    defaultValue.Add(instance.id, instance.moneyUnlock);
                }
            }
        }
        base.Init();
    }

#if UNITY_EDITOR
    public void FirstInit()
    {
        Init();
    }
#endif
}

#if UNITY_EDITOR
[CustomEditor(typeof(ParcelsData), true)]
public class ParcelsDataEditor : Editor
{
    ParcelsData parcelsData;

    public void OnEnable()
    {
        parcelsData = target as ParcelsData;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("RaiseEvent"))
        {
            parcelsData.FirstInit();
        }
    }
}


#endif*/