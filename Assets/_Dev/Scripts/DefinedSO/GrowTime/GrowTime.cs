using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="GrowTime",menuName ="GrowTime")]
public class GrowTime : ScriptableObject
{
    [SerializeField] List<GrowPlantTime> growTimes;
    [Serializable]
    public struct GrowPlantTime
    {
        public FarmProductType type;
        public int plantDurationInSec;
    }
    
    public int GetPlantDurationInSec(FarmProductType type)
    {
        return growTimes.Find(e => e.type == type).plantDurationInSec;
    }
}
