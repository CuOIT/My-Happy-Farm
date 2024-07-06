using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionController : MonoBehaviour
{
    [SerializeField] MissionData missionData;    
    
    [SerializeField] List<Mission> missions;
    // Start is called before the first frame update
    [SerializeField] List<MissionUI> missionUIs;


    [SerializeField] GameObject noticeGO;

    public void GetAllMissions()
    {
        missions = missionData.TodayMissions;
    }

    public void OnMissionComplete()
    {
        noticeGO.SetActive(true);
    }
    public void Start()
    {
        GetAllMissions();
        InitMissionUIs();
    }
    public void InitMissionUIs()
    {
        bool done = false;
        for(int i=0;i<missionUIs.Count;i++) {
            if (missions.Count<=i)
            {
                missionUIs[i].gameObject.SetActive(false);
            }
            else
            {
                if (missions[i].collected)
                {
                    missionUIs[i].gameObject.SetActive(false);
                }
                else
                {
                    missionUIs[i].InitMission(missions[i]);
                    if (missions[i].IsDone()) done = true;
                }
            }
        }
        noticeGO.SetActive(done);
    }


    public void OnCollectMission()
    {
        foreach(var mission in missions)
        {
            if (mission.IsDone() && !mission.collected)
            {
                noticeGO.SetActive(true);
                return;
            }
        }
        noticeGO.SetActive(false);
    }
}
