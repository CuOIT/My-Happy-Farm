using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI indexTxt;
    [SerializeField] TextMeshProUGUI playerNameTxt;
    [SerializeField] TextMeshProUGUI levelTxt;
    [SerializeField] Image avt;

    public void Init(int index, LeaderboardInfo leaderboardInfo)
    {
        indexTxt.SetText(index.ToString());
        avt.sprite = leaderboardInfo.avt;
        playerNameTxt.SetText(leaderboardInfo.name);
        levelTxt.SetText(leaderboardInfo.level.ToString());
    }
}
