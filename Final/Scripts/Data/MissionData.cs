
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName ="MissionData",menuName ="Data/Mission")]
public class MissionData : LocalData<DateTime> 
{
    [SerializeField] int missionNumADay;
    [SerializeField] List<Mission> missions;
    [SerializeField] List<Mission> todayMissions;
    public List<Mission> TodayMissions => todayMissions;

    [Button]
     void LoadAllMissions()
    {
#if UNITY_EDITOR
        string[] guids = AssetDatabase.FindAssets("t:Mission");
        missions = new List<Mission>();

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Mission mission = AssetDatabase.LoadAssetAtPath<Mission>(path);
            if (mission != null)
            {
                missions.Add(mission);
            }
        }
#endif
    }
    protected override void Init()
    {
        
    }
    protected override void OnLoadSuccessfully()
    {
        if (Value.Date != DateTime.Now.Date)
        {
            InitMission();
            Value = DateTime.Now;
        }
    }
    private void InitMission()
    {
        todayMissions = new List<Mission>();
        List<Mission> temp = new List<Mission>(missions);
        for(int i = 0; i < missionNumADay; i++)
        {
            int rand = Random.Range(0,temp.Count);
            todayMissions.Add(temp[rand]);
            temp[rand].ResetProgress();
            temp.RemoveAt(rand);
            if (temp.Count == 0) return;
        }
    }
}
