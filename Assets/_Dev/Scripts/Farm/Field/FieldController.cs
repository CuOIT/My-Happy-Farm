using _Template.Event;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FieldController : MonoBehaviour,IField
{
    bool isActive = false;
    [SerializeField] List<FieldCell>        fields;
    public List<FieldCell> FieldCells => fields;

    [SerializeField] FarmProductTypeData    plantType;

    public void AddCells(List<FieldCell> cells)
    {
        foreach (FieldCell cell in cells)
        {
            fields.Add(cell);
            cell.InitPlantType(plantType.Value);
        }
    }

    public void RemoveCells(List<FieldCell> cells)
    {
        foreach(FieldCell cell in cells) { 
            fields.Remove(cell);
        }
    }

    public bool IsActive()
    {
        return fields.Count > 0;
    }
    public void SetPlantType(FarmProductType type)
    {
        plantType.Value = type;
        foreach(var field in fields)
        {
            field.InitPlantType(type);
        }
    }
    public FarmProductType GetPlantType()
    {
        foreach(var fieldCell in fields)
        {
            if (fieldCell.GetState()!=FieldCell.NONE)
            {
                return plantType.Value;
            }
        }
        plantType.Value = FarmProductType.NONE;
        return plantType.Value;
    }
    


    //Dont Care
    [ContextMenu("GROW")]
    public void GrowField()
    {
        foreach (var cell in fields)
        {
            cell.GrowPlant();
        }
    }
    [ContextMenu("WATER")]
    public void WaterField()
    {
        foreach (var cell in fields)
        {
            cell.WaterPlant();
        }
    }
    [ContextMenu("COLLECT")]
    public void CollectField()
    {
        foreach (var cell in fields)
        {
            cell.CollectPlant(null);
        }
    }
}
    public enum State
    {
        GROW,
        WATER,
        COLLECT,
        COUNT,
    }



