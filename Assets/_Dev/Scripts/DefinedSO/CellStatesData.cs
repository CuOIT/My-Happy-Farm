using System;
using System.Collections.Generic;
using UnityEngine;



    [CreateAssetMenu(fileName ="CellState",menuName ="Data/CellState")]
    public class CellStatesData : LocalData<List<CellState>>
    {


    }

[Serializable]
public struct CellState
{
    public State state;
    public string waterTime;
}
