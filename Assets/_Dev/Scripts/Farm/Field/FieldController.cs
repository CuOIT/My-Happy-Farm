using _Template.Event;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FieldController : MonoBehaviour,IField
{
    private Collider col;
    [SerializeField] State _state;
    [SerializeField] List<FieldCell>        fields;
    [SerializeField] int                    _childNum;
    [SerializeField] FarmProductTypeData    plantType;
    [SerializeField] GameObject             _info;

    [SerializeField] SimpleEvent            _leaveField;
    [SerializeField] GameObject             IFieldUIGO;
    private IFieldUI fieldUI;



    [System.Serializable]
    private struct EventState {
        [SerializeField] public State state;
        [SerializeField] public SimpleEvent sEvent;
    }

    [SerializeField] List<EventState> events;
    public void Awake()
    {
        col=GetComponent<Collider>();
        fieldUI = IFieldUIGO.GetComponent<IFieldUI>();
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
        if (col == null)
        {
            col = GetComponent<Collider>();
        }
        if (fields.Count==0)
        {
            col.enabled = false;
        }
        else
        {
            col.enabled=true;
        }
        int maxState = -1;
        foreach(var cell in fields)
        {
            int state = cell.GetState();
            if (state >maxState)
            {
                _childNum = 1;
               maxState= state;
            }
            else if (state == maxState) 
            {
                _childNum++;
            }
        }
        _state=(State)maxState;
    }
    public void LoadState()
    {

    }

    [ContextMenu("GROW")]
    public void GrowField()
    {
        foreach(var cell in fields)
        {
            cell.GrowPlant();
        }
    }
    [ContextMenu("WATER")]
    public void WaterField()
    {
        foreach(var cell in fields)
        {
            cell.WaterPlant();
        }
    }
    [ContextMenu("COLLECT")]
    public void CollectField()
    {
        foreach(var cell in fields)
        {
            cell.CollectPlant(null);
        }
    }

    private bool player = false;
    private void ShowInfoForPlayer()
    {
        player = true;
        //_info?.SetActive(true);
        //
        switch (_state)
        {
            case State.GROW:
                fieldUI.InitType(plantType.Value,this);
                fieldUI.ShowGrowAction();
                break;
            case State.WATER:
                fieldUI.ShowWaterAction();
                break;
            case State.COLLECT:
                fieldUI.ShowCollectAction();
                break;
        }
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
    public void NextState()
    {
        UpdateField();
        if (_state == State.GROW) plantType.Value = FarmProductType.NONE;
        ShowInfoForPlayer();
    }
    public void IncreaseState()
    {
        _childNum--;
        if (_childNum==0)
        {
            NextState();
        }
    }

    public void SetPlant(FarmProductType type)
    {
        plantType.Value = type;
        foreach(var field in fields)
        {
            field.InitPlantType(type);
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



