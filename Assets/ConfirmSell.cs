using Assets._Dev.SO._CustomEvent;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmSell : MonoBehaviour
{
    [SerializeField] Image img;
    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI maxNumTxt;
    [SerializeField] TextMeshProUGUI numTxt;
    [SerializeField] TextMeshProUGUI priceTxt;
    [SerializeField] ProductNumEvent sellProductEvent;
    [SerializeField] IntEvent earnMoneyEvent;
    private FarmProductType type;
    private int curNum;
    private int maxNum;
    [SerializeField] Button cancelBtn;
    [SerializeField] Button sellBtn;
    int price;
    public void Awake()
    {
        cancelBtn.onClick.AddListener(Cancel);
        sellBtn.onClick.AddListener(Sell);
    }
    public void Init(ProductInfo productInfo, int num)
    {
        gameObject.SetActive(true);
        price = productInfo.price;
        type=productInfo.type;
        maxNum = num;
        img.sprite = productInfo.sprite;
        maxNumTxt.SetText(maxNum.ToString());
        slider.value = 1;
        SetNum(maxNum);
        cancelBtn.gameObject.SetActive(true);
        sellBtn.gameObject.SetActive(true);
        transform.DOScale(1, 0.5f).SetEase(Ease.OutBack);
    }

    void SetNum(int num)
    {
        curNum = num;
        numTxt.SetText(num.ToString());
        priceTxt.SetText((price*num).ToString());
    }
    public void OnSliderChangeValue()
    {
        SetNum( (int)(slider.value * (float)maxNum));
    }
    public void Cancel()
    {
        transform.DOScale(0, 0.5f).SetEase(Ease.InBack).OnComplete(()=>gameObject.SetActive(false));
    }
    public void Sell()
    {
        if (type.IsUnityNull()) return;
        ProductNum productNum = new ProductNum(type,curNum);
        sellProductEvent.RaiseEvent(productNum);
        earnMoneyEvent.RaiseEvent(price * curNum);
        cancelBtn.gameObject.SetActive(false);
        sellBtn.gameObject.SetActive(false);
        StartCoroutine(SuccessSell());
    }

    [SerializeField] float duration;
    public IEnumerator SuccessSell()
    {
        float time = 0;
        int num=curNum;
        while (time < duration)
        {
            time += Time.deltaTime;
            num = Mathf.Clamp((int)(curNum-(time / duration) * curNum),0,curNum);
            numTxt.SetText(num.ToString());
            yield return null;
        }
        if (time >= duration)
        {
            Cancel();
        }
    }
}

