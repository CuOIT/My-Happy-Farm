using _Template.Event;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FieldController : MonoBehaviour,IField
{
    private Collider col;
    [SerializeField] List<FieldCell>        fields;

    [SerializeField] FarmProductTypeData    plantType;

    [SerializeField] GameObject             _info;

    [SerializeField] FieldUI fieldUI;

    public void Awake()
    {
        col=GetComponent<Collider>();
    }
    public void Start()
    {
        UpdateField();
    }
    public void AddCells(List<FieldCell> cells)
    {
        foreach (FieldCell cell in cells)
        {
            fields.Add(cell);
            cell.InitPlantType(plantType.Value);
        }
        UpdateField();
    }
    public void RemoveCells(List<FieldCell> cells)
    {
        foreach(FieldCell cell in cells) { 
            fields.Remove(cell);
        }
    }
    public void UpdateField()
    {
        /*if (col == null) col = GetComponent<Collider>();

        if (fields.Count==0)
        {
            col.enabled = false;
        }
        else
        {
            col.enabled=true;
        }*/
    }
    private bool player = false;
    private void ShowInfoForPlayer()
    {
        player = true;
        //_info?.SetActive(true);
        fieldUI.InitField(this);
        fieldUI.ShowAction();
    }
    private void HideInfoForPlayer()
    {
        player = false;
        //_info?.SetActive(false);
        fieldUI.UnShow();
    }
    public void OnTriggerEnter(Collider other)
    {
        ShowInfoForPlayer();
    }
    public void OnTriggerExit(Collider other)
    {
        HideInfoForPlayer();
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
            if (fieldCell.GetState()!=0)
            {
                return plantType.Value;
            }
        }
        plantType.Value = FarmProductType.NONE;
        return plantType.Value;
    }
    
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



