using Cage;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalUI : MonoBehaviour, IAnimalUI
{
    ICage currentCage;

    [Serializable]
    private struct TypeBtn
    {
        public FarmProductType type;
        public Button btn;
    }

    [SerializeField] List<TypeBtn> types;
    Button currentBtn;

    public void Init(FarmProductType type,ICage cage)
    {
        currentCage = cage;
        HideBtn();
        currentBtn = types.Find((e) => e.type == type).btn;
        currentBtn.gameObject.SetActive(true);
        currentBtn.onClick.RemoveAllListeners();
        currentBtn.onClick.AddListener(OnClickFeed);
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
        if (currentCage.EnoughBarn())
        {
            currentCage.Feed();
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

public interface IAnimalUI
{
    public void Init(FarmProductType type, ICage cage);
    public void Show();
    public void Hide();
}
