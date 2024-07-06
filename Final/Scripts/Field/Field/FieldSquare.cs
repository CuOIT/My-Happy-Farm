using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FieldSquare : MonoBehaviour
{
    public ListIntData states;

    public ListStringData waterTime;
    [SerializeField] List<FieldCell> cells;

    private FieldController controller;

    [Button] 
    public void GetCells()
    {
        cells = GetComponentsInChildren<FieldCell>().ToList();
    }
    private void OnEnable()
    {
        controller = GetComponentInParent<FieldController>();
        controller?.AddCells(cells);
        List<int> sList = states.Value;
        List<string> waterTimes = waterTime.Value;
        int n = states.Value.Count;
        for(int i = 0;i< cells.Count; i++) {
            if (i < n)
            {
                cells[i].InitState(sList[i]);
                cells[i].InitWaterTime(waterTimes[i]);
            }
            else
            {
                cells[i].InitState(0);
                cells[i].InitWaterTime(null);
            }
            cells[i].SetId(this,i);
        }
    }
    private void OnDisable()
    {
        controller?.RemoveCells(cells); 
    }

    public void UpdateState(int id,int num)
    {
        List<int> newState = states.Value;
        newState[id]= num;
        states.Value = newState;
    }

    public void UpdateWaterTime(int id,string time)
    {
        List<string> sList = waterTime.Value;
        sList[id] = time;
        waterTime.Value = sList;
    }
}
