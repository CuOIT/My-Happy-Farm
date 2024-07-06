using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LeaderBoardController : MonoBehaviour
{
    [SerializeField] GameObject leaderboardSrc;

    [SerializeField] List<LeaderboardUI> leaderUIs;

    [SerializeField] LeaderboardUI myUI;

    public void GetUI()
    {
        leaderUIs = GetComponentsInChildren<LeaderboardUI>().ToList();
    }
    [SerializeField] string Name="Cuong";
    public void OnEnable()
    {
        GetUI();
        ILeaderboard iLeaderboardSrc = leaderboardSrc.GetComponent<ILeaderboard>();
        List<LeaderboardInfo> leaderboardInfos = iLeaderboardSrc.GetLeaderboardWithTopOfAmount(leaderUIs.Count);

        for(int i = 0;i< leaderboardInfos.Count; i++)
        {
            leaderUIs[i].Init(i+1, leaderboardInfos[i]);
        }
        int index;
        LeaderboardInfo myInfo = iLeaderboardSrc.GetLeaderboardByName(Name,out index);
        myUI.Init(index, myInfo);
    }
}
