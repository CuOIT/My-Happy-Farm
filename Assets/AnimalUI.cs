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
    private AnimalFarmer animalFarmer;
    [Serializable]private struct TypeBtn
    {
        public FarmProductType type;
        public Button btn;
    }
    [SerializeField] List<TypeBtn> types;
    [SerializeField] TextMeshProUGUI foodNum;
    Button currentBtn;

    public void Init(ProductNum foodRequire)
    {
        HideBtn();
        currentBtn = types.Find((e) => e.type == foodRequire.type).btn;
        currentBtn.gameObject.SetActive(true);
        currentBtn.onClick.RemoveAllListeners();
        currentBtn.onClick.AddListener(OnClickFeed);
        foodNum.SetText(foodRequire.num.ToString());
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
        if (animalFarmer.EnoughBarn())
        {
            animalFarmer.Feed();
            Hide();
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
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}

