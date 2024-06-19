using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PseudoLeaderBoard : MonoBehaviour,ILeaderboard
{
    [SerializeField] List<LeaderboardInfo> leaderboardInfos;
    public List<LeaderboardInfo> GetLeaderboardWithTopOfAmount(int num)
    {
        int count = Mathf.Min(leaderboardInfos.Count, num);
        leaderboardInfos.OrderByDescending(e => e.level);
        List<LeaderboardInfo> res = leaderboardInfos.GetRange(0, count);
        return res;
    }

    public LeaderboardInfo GetLeaderboardByName(string name)
    {
        return leaderboardInfos.Find(e=>e.name == name);
    }
}

public interface ILeaderboard
{
    public List<LeaderboardInfo> GetLeaderboardWithTopOfAmount(int num);
    public LeaderboardInfo GetLeaderboardByName(string name);
}

[Serializable]
public struct LeaderboardInfo
{
    public string name;
    public int level;
}