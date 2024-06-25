    using _Template.Event;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FieldUI : MonoBehaviour
{
    [SerializeField] GameObject firstAction;

    [SerializeField] GameObject _seedAction;

    private GameObject currentAction;

    [Serializable]
    struct PlantButton
    {
        public FarmProductType type;
        public Button btn;
    }
    [SerializeField] List<PlantButton> plantButtons;
    [SerializeField]private PlayerFieldFarmer _farmer;


    public void InitType(FarmProductType type)
    {
        var _type = type;
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
        _farmer.LeaveField();
    }
    public void OnClickGrowAction()
    {
        InitType(_farmer.GetFieldType());
        SetCurrentAction(_seedAction);
    }
    public void OnClickOfSeedAction(FarmProductType type)
    {
        SetCurrentAction(null);
        _farmer.GrowPlant(type);
    }
    public void OnClickOfWaterAction()
    {
        _farmer.WaterPlant();
    }
    public void OnClickOfCollectAction()
    {
        _farmer.CollectPlant();
    }


}
