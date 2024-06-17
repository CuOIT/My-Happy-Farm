using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionUI : MonoBehaviour
{
    Mission _mission;

    [SerializeField] TextMeshProUGUI des;
    [SerializeField] TextMeshProUGUI currentProgress;
    [SerializeField] Slider progress;

    [SerializeField] TextMeshProUGUI rewardNum;
    [SerializeField] Button rewardBtn;
    MissionController missionController;

    private void Awake()
    {
        missionController=GetComponentInParent<MissionController>();    
    }
    public void InitMission(Mission mission)
    {
        _mission = mission;
        rewardBtn.onClick.AddListener(CollectRw);
        UpdateStatus();
        _mission.missionChange += UpdateStatus;
    }

    public void UpdateStatus()
    {
        des.SetText(_mission.description);
        currentProgress.SetText(_mission.currentProgress+"/"+_mission.targetProgress);
        progress.maxValue = _mission.targetProgress;
        progress.value = _mission.currentProgress;
        rewardNum.SetText(_mission.reward.ToString());
        if (_mission.IsDone())
        {
            rewardBtn.GetComponent<Image>().color = Color.white;
            rewardBtn.enabled = true;
        }
        else
        {
            rewardBtn.GetComponent<Image>().color = Color.grey;
            rewardBtn.enabled = false;
        }
    }

    public void CollectRw()
    {
        if (_mission.IsDone())
        {
            _mission.collected = true;
            GameManager.Instance.moneyController.CollectMoney(_mission.reward);
            gameObject.SetActive(false);
            missionController.OnCollectMission();
        }
    }

    public void OnDestroy()
    {
        if(_mission != null )
            _mission.missionChange -= UpdateStatus;
    }
}
