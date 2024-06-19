using _Template.Event;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FieldController : MonoBehaviour,IField
{
    [SerializeField] List<FieldCell>        fields;
    public List<FieldCell> FieldCells => fields;

    [SerializeField] FarmProductTypeData    plantType;

    [SerializeField] FieldBotController bot;
    [SerializeField] int botCost;
    [SerializeField] IntData haveBot;
    public void AddCells(List<FieldCell> cells)
    {
        foreach (FieldCell cell in cells)
        {
            fields.Add(cell);
            cell.InitPlantType(plantType.Value);
        }
        //UpdateField();
    }
    public void OnEnable()
    {
        if (haveBot.Value > 0)
        {
            bot.gameObject.SetActive(true);
        }
        else
        {
            bot.gameObject.SetActive(false);
        }
    }
    public int GetBotCost()
    {
        if(haveBot.Value>0) return 0;
        return botCost;
    }
    public void Hire()
    {
        bot.gameObject.SetActive(true);
        haveBot.Value = 1; 
    }
    public void SetNextCrop(FarmProductType type)
    {
        bot.SetPlant(type);
    }
    public void RemoveCells(List<FieldCell> cells)
    {
        foreach(FieldCell cell in cells) { 
            fields.Remove(cell);
        }
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



