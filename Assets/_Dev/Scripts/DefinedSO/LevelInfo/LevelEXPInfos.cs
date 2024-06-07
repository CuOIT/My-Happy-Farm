using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NewLevelEXPInfos",menuName ="Level/LevelEXPInfos")]
public class LevelEXPInfos : ScriptableObject
{
    [SerializeField] List<int> levelEXPs;

    public int GetlevelEXP(int level)
    {
        if (levelEXPs.Count > level) {
            return levelEXPs[level];
        }
        else
        {
            return 9999999;
        }
    }
}

