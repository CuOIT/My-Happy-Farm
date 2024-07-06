using Assets._Dev.SO._CustomEvent;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Container : BaseContainer
{
    

    [SerializeField] protected TextMeshProUGUI t_name;
    [SerializeField] protected TextMeshProUGUI t_price;
    [SerializeField] protected TextMeshProUGUI t_des;

    [SerializeField] SliderPopUp sliderSell;
    [SerializeField] SliderPopUp sliderSendToTarget;
    [SerializeField] ProductNumEvent sellProductEvent;

    [SerializeField] Container target;

    [SerializeField]protected Button sendToTargetBtn;
    [SerializeField]protected Button sellBtn;
    public override void GetAllProduct()
    {
        mapProductInfo = new();
        var itemBoxs = GetComponentsInChildren<ItemBoxUI>();
        int index = 0;
        foreach (KeyValuePair<FarmProductType, int> product in productData.Value)
        {
            if (product.Value > 0)
            {
                ProductInfo productInfo = productInfos.GetProductInfoOfType(product.Key);
                ItemBoxUI itemBoxUI = itemBoxs[index];
                itemBoxUI.AddListener(() => SetCurrentItem(itemBoxUI));
                index++;
                itemBoxUI.Init(productInfo.sprite, product.Value);
                if (!mapProductInfo.ContainsKey(itemBoxUI))
                {
                    mapProductInfo.Add(itemBoxUI, productInfo);
                }
            }
        }
        for (int i = index; i < itemBoxs.Length; i++)
        {
            ItemBoxUI itemBoxUI = itemBoxs[i];
            //mapProductInfo.Add(itemBoxUI, );
            itemBoxUI.AddListener(() => SetCurrentItem(itemBoxUI));
            itemBoxUI.ResetThis();
        }
    }
    public override void SetCurrentItem(ItemBoxUI item)
    {
        base.SetCurrentItem(item);
        if (item == null) return;
        if (mapProductInfo.ContainsKey(item))
        {
            ProductInfo info = mapProductInfo[item];
            t_name.SetText(info.type.ToString());   
            t_price.SetText(info.price.ToString()+" $");
            t_des.SetText(info.description);
        }
        else
        {
            t_name.SetText("");
            t_price.SetText(0 + " $");
            t_des.SetText("");
        }
    }
    public void AddProduct(ProductNum productNum)
    {
        Dictionary<FarmProductType,int> newValue=productData.Value;
        newValue[productNum.type] += productNum.num;
        productData.Value = newValue;
    }

    public void RemoveProduct(ProductNum productNum)
    {
        Dictionary<FarmProductType, int> newValue = productData.Value;
        newValue[productNum.type] -= productNum.num;
        productData.Value = newValue;
    }
    public void ExchangeProduct(ProductNum productNum)
    {
        if (target == null) return;
        RemoveProduct(productNum);
        target.AddProduct(productNum);
        GetAllProduct();
    }

    public async void SendToTarget()
    {
        ProductInfo info = mapProductInfo[currentItem];
        int num = await sliderSendToTarget.ShowSliderPopupAsync(info, productData.Value[info.type]);
        ProductNum productNum = new ProductNum(info.type, num);
        if (num == 0) return;
        else
        {
            ExchangeProduct(productNum);
        }
    }
    public async void SellProduct()
    {
        ProductInfo info = mapProductInfo[currentItem];
        int num = await sliderSell.ShowSliderPopupAsync(info, productData.Value[info.type]);
        ProductNum productNum = new ProductNum(info.type, num);
        if (num == 0) return;
        else
        {
            Sell(productNum);
        }
    }
    public void Sell(ProductNum productNum)
    {
        ProductInfo info = productInfos.GetProductInfoOfType(productNum.type);
        Dictionary<FarmProductType, int> newValue = productData.Value;
        newValue[productNum.type] -= productNum.num;
        productData.Value = newValue;
        sellProductEvent.RaiseEvent(productNum);
        GameManager.Instance.moneyController.CollectMoney(info.price * productNum.num);
        GetAllProduct();
    }
}
