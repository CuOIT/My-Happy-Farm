using _Template.Event;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BotUI : MonoBehaviour
{
    [SerializeField] IntData botCost;
    [SerializeField] BotFieldFarmer botController;
    [SerializeField] GameObject UnActive;
    [SerializeField] Button HireBtn;
    [SerializeField] TextMeshProUGUI costTxt;
    [SerializeField] ButtonType typeBtn;
    [SerializeField] GameObject SetPlantBtn;
    [SerializeField] SimpleEvent hireEvent;

    public void OnEnable()
    {
        UpdateUI();
    }
    public void UpdateUI()
    {
        if (!botController.HaveFieldActive())
        {
            UnActive.SetActive(true);
            SetPlantBtn.SetActive(false);
            HireBtn.gameObject.SetActive(false);
        }
        else
        {
            UnActive.SetActive(false);
            SetPlantBtn.SetActive(HaveBot());
            HireBtn.gameObject.SetActive(!HaveBot());
            string cost;
            if (botCost.Value >= 1000000) { cost = botCost.Value / 1000000 + "M"; }
            else if(botCost.Value>=1000) { cost = botCost.Value /1000 + "k"; }
            else { cost = botCost.Value + ""; }
            costTxt.SetText(cost);
            if (GameManager.Instance.moneyController.HaveMoney(botCost.Value))
            {
                HireBtn.GetComponent<Image>().color = Color.white;
            }
            else
            {
                HireBtn.GetComponent<Image>().color = Color.grey;
            }
        }
    }
    public void SetPlant()
    {
        botController.SetPlant(typeBtn.GetPlantType());
    }
    public void Hire()
    {
        if (GameManager.Instance.moneyController.HaveMoney(botCost.Value))
        {
            botCost.Value = 0;
            UpdateUI();
            hireEvent.RaiseEvent();
        }
        else
        {
            GameManager.Instance.Notice("Not enough money!");
        }

    }
    bool HaveBot()
    {
        return botCost.Value == 0;
    }
}
