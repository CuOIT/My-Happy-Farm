using Assets._Dev.SO._CustomEvent;
using Cage;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnimalUI : MonoBehaviour
{
    [SerializeField] AnimalFarmer animalFarmer;
    [Serializable]private struct TypeBtn
    {
        public FarmProductType type;
        public Button btn;
    }
    [SerializeField] List<TypeBtn> types;
    Button currentBtn;
    public void Init(ProductNum foodRequire)
    {
        HideBtn();
        currentBtn = types.Find((e) => e.type == foodRequire.type).btn;
        currentBtn.gameObject.SetActive(true);
        currentBtn.onClick.RemoveAllListeners();
        currentBtn.onClick.AddListener(OnClickFeed);
        /*var foodNum = currentBtn.GetComponentInChildren<TextMeshProUGUI>();
        foodNum.SetText(foodRequire.num.ToString());*/
    }
    public void HideBtn()
    {
        foreach(var typeBtn in types)
        {
            typeBtn.btn.gameObject.SetActive(false);
        }
    }
    public void OnClickFeed()
    {
        if (animalFarmer.EnoughBran())
        {
            animalFarmer.Feed();
            HideBtn();
        }
        else
        {
            GameManager.Instance.Notice("Not enough barn");
        }
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }
}

