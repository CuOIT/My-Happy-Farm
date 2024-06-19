    using _Template.Event;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FieldUI : MonoBehaviour
{
    [SerializeField] GameObject firstAction;

    [SerializeField] TextMeshProUGUI botCostTxt;

    [SerializeField] GameObject _seedAction;

    [SerializeField] GameObject _seedBotAction;

    private GameObject currentAction;

    [Serializable]
    struct PlantButton
    {
        public FarmProductType type;
        public Button btn;
    }
    [SerializeField] List<PlantButton> plantButtons;

    [SerializeField] List<PlantButton> plantButtonsForBot;
    private FarmProductType _type;
    [SerializeField]private PlayerFieldFarmer _farmer;

    public void Awake()
    {
        foreach(var plantButton in plantButtons)
        {
            plantButton.btn.onClick.AddListener(() => OnClickSetPlantForBot(plantButton.type));
        }
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

    public void  InitBotStatus(int botCost)
    {
        if (botCost > 0)
        {
            botCostTxt.transform.parent.gameObject.SetActive(true);
            botCostTxt.SetText(botCost.ToString());
        }
        else
        {
            botCostTxt.transform.parent.gameObject.SetActive(false);
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
    public void ShowBotPlantSeedAction()
    {
        SetCurrentAction(_seedBotAction);
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

    public void OnClickHireAction()
    {
        _farmer.Hire();
        SetCurrentAction(null);
    }
    public void OnClickSetPlantForBot(FarmProductType type)
    {
        _farmer.SetPlantType(type);
        SetCurrentAction(null);
    }
}
