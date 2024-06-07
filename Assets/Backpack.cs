using Assets._Dev.SO._CustomEvent;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Backpack : Container
{
    [SerializeField] Button sellBtn;
    [SerializeField] Button sendStorageBtn;

    [SerializeField] SliderPopUp sliderSendToStorage;
    [SerializeField] SliderPopUp sliderToSell;

    [SerializeField] ProductNumEvent sellProductEvent;
    [SerializeField] IntEvent earnMoneyEvent;

    public async void SellProduct()
    {
        ProductInfo info = mapProductInfo[currentItem];
        int num = await sliderToSell.ShowSliderPopupAsync(info, productData.Value[info.type]);
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
        earnMoneyEvent.RaiseEvent(info.price * productNum.num);
        GetAllProduct();
    }
    public override void SetCurrentItem(ItemBoxUI item)
    {
        base.SetCurrentItem(item);
        ProductInfo info;
        if (mapProductInfo.ContainsKey(item))
        {
            info = mapProductInfo[item];
        }
        else { 
            info = new ProductInfo(FarmProductType.NONE,null,"",0);
        }
        if (info.type != FarmProductType.NONE)
        {
            t_name.SetText(info.type.ToString());
            t_price.SetText(info.price.ToString()+" $");
            t_des.SetText(info.description.ToString());
            sellBtn.gameObject.SetActive(true);
            sendStorageBtn?.gameObject.SetActive(true);
        }
        else
        {
            t_name.SetText("");
            t_price.SetText("");
            t_des.SetText("");
            sendStorageBtn?.gameObject.SetActive(false);
            sellBtn.gameObject.SetActive(false);
        }
    }

    public async void SendToStorage()
    {
        ProductInfo info = mapProductInfo[currentItem];
        int num = await sliderSendToStorage.ShowSliderPopupAsync(info, productData.Value[info.type]);
        ProductNum productNum = new ProductNum(info.type, num);
        if (num == 0) return;
        else
        {
            ExchangeProduct(productNum);
        }
    }
}
