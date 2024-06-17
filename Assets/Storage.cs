using _Template.Event;
using Assets._Dev.SO._CustomEvent;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Storage : Container
{
    [SerializeField] SimpleEvent storeEvent;
   // [SerializeField] SliderPopUp sliderPopUpBack;
    //[SerializeField] SliderPopUp sliderSell;
   // [SerializeField] Button SendToBackpackBtn;
   // [SerializeField] Button SellBtn;
   // [SerializeField] ProductNumEvent sellProductEvent;
    public override void SetCurrentItem(ItemBoxUI item)
    {
        base.SetCurrentItem(item);
        if(item!=null)
        {
            storeEvent.RaiseEvent();
        }
    }
  /*  public async void SendToBackpack()
    {
        ProductInfo info = mapProductInfo[currentItem];
        int num = await sliderPopUpBack.ShowSliderPopupAsync(info, productData.Value[info.type]);
        ProductNum productNum = new ProductNum(info.type,num);
        if(num==0) return;
        else
        {
            ExchangeProduct(productNum);
        }
    }*/

    /*public async void SellProduct()
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
    }*/
}
