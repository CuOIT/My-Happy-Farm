    using _Template.Event;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FieldUI : MonoBehaviour,IFieldUI
{
    [SerializeField] GameObject firstAction;

    [SerializeField] GameObject _seedAction;

    private GameObject currentAction;

    [SerializeField] SimpleEvent _confirmSeedEvent;
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
    private FieldController _currentField;
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

    public void InitField(FieldController field)
    {
        _currentField = field;
    }
    public void InitType(FarmProductType type)
    {
        _type = type;
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
        _currentField?.SetPlantType(type);
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


    public void ShowAction()
    {
        SetCurrentAction(firstAction);
    }
    public void ShowPlantSeedAction()
    {
        SetCurrentAction(_seedAction);
    }
    public void UnShow()
    {
        SetCurrentAction(null);
        _confirmLeaveFieldEvent.RaiseEvent();
    }



    public void OnClickGrowAction()
    {
        InitType( _currentField.GetPlantType());
        SetCurrentAction(_seedAction);
    }
    public void OnClickOfSeedAction(FarmProductType type)
    {
        SetCurrentAction(null);
        _currentField.SetPlantType(type);
        _confirmSeedEvent.RaiseEvent();
    }
    public void OnClickOfWaterAction()
    {
        //_waterAction.SetActive(false);
        _confirmWaterEvent.RaiseEvent();
    }
    public void OnClickOfCollectAction()
    {
       // _collectAction.SetActive(false);
        _confirmCollectEvent.RaiseEvent();
    }

}
