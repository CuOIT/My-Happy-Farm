using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PseudoLeaderBoard : MonoBehaviour,ILeaderboard
{
    [SerializeField] List<LeaderboardInfo> leaderboardInfos;
    private void Awake()
    {
        SortLeaderboard();
    }
    public List<LeaderboardInfo> GetLeaderboardWithTopOfAmount(int num)
    {
        int count = Mathf.Min(leaderboardInfos.Count, num);
        List<LeaderboardInfo> res = leaderboardInfos.GetRange(0, count);
        return res;
    }

    void SortLeaderboard()
    {
        leaderboardInfos = leaderboardInfos.OrderByDescending(e => e.level).ToList();
    }
    public LeaderboardInfo GetLeaderboardByName(string name, out int index)
    {

        index = leaderboardInfos.FindIndex(e=> e.name == name);
        return leaderboardInfos[index];
    }
}

public interface ILeaderboard
{
    public List<LeaderboardInfo> GetLeaderboardWithTopOfAmount(int num);
    public LeaderboardInfo GetLeaderboardByName(string name, out int index);
}

[Serializable]
public struct LeaderboardInfo
{
    public Sprite avt;
    public string name;
    public int level;
}