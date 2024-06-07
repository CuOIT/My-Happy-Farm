    using _Template.Event;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.UI;

public class FieldUI : MonoBehaviour,IFieldUI
{
    [SerializeField] GameObject _growAction;
    [SerializeField] GameObject _seedAction;
    [SerializeField] GameObject _waterAction;
    [SerializeField] GameObject _collectAction;

    private GameObject currentAction;

    [SerializeField] SimpleEvent _confirmGrowEvent;
    [SerializeField] SimpleEvent _confirmWaterEvent;
    [SerializeField] SimpleEvent _confirmCollectEvent;
    [SerializeField] SimpleEvent _confirmLeaveFieldEvent;

    [Serializable]
    struct PlantButton
    {
        public FarmProductType type;
        public Button btn;
    }
    [SerializeField] List<PlantButton> plantButtons;
    private Dictionary<FarmProductType,Button> dicsBtn;
    private FarmProductType _type;
    private IField _field;
    public void Awake()
    {
        dicsBtn = new();
        foreach(var plantbutton in plantButtons)
        {
            if (!dicsBtn.ContainsKey(plantbutton.type))
            {
                dicsBtn.Add(plantbutton.type, plantbutton.btn);
            }
        }
    }

    public void InitType(FarmProductType type,IField field)
    {
        _type = type;
        _field= field;
        bool actived = _type == FarmProductType.NONE;
        foreach(var plantbutton in plantButtons)
        {
            plantbutton.btn.gameObject.SetActive(actived);
            plantbutton.btn.onClick.AddListener(()=>OnClickOfSeedAction(plantbutton.type));
            if (plantbutton.type == type)
            {
                plantbutton.btn.gameObject.SetActive(true);
            }
        }
    }

    public void SetType(FarmProductType type)
    {
        _field?.SetPlant(type);
    }
    public void ShowGrowAction()
    {
        SetCurrentAction(_growAction);
    }
    public void ShowPlantSeedAction()
    {
        SetCurrentAction(_seedAction);
    }
    public void ShowWaterAction()
    {
        SetCurrentAction(_waterAction);
    }
    public void ShowCollectAction()
    {
        SetCurrentAction(_collectAction);
    }
    public void UnShow()
    {
        SetCurrentAction(null);
        _confirmLeaveFieldEvent.RaiseEvent();
    }

    public void OnClickGrowAction()
    {
        SetCurrentAction(_seedAction);
    }
    public void OnClickOfSeedAction(FarmProductType type)
    {
        SetCurrentAction(null);
        _field.SetPlant(type);
        _confirmGrowEvent.RaiseEvent();
    }
    public void OnClickOfWaterAction()
    {
        _waterAction.SetActive(false);
        _confirmWaterEvent.RaiseEvent();
    }
    public void OnClickOfCollectAction()
    {
        _collectAction.SetActive(false);
        _confirmCollectEvent.RaiseEvent();
    }
    public void SetCurrentAction(GameObject action)
    {
        if (currentAction != null)
        {
            currentAction.SetActive(false);
        }
        currentAction = action;
        currentAction?.SetActive(true);
    }



}
