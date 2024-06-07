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

    [SerializeField] Container target;
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
}
