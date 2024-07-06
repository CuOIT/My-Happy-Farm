using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewProductInfo", menuName = "Product")]
public class ProductInfos : ScriptableObject
{
    [SerializeField] List<ProductInfo> productInfos;
    private Dictionary<FarmProductType,ProductInfo> _productInfos;

    public void OnEnable()
    {
        _productInfos = new();
        foreach(var  productInfo in productInfos)
        {
            if (!_productInfos.ContainsKey(productInfo.type))
            {
                _productInfos.Add(productInfo.type, productInfo);
            }
        }
    }
    public ProductInfo GetProductInfoOfType(FarmProductType type)
    {
        if (_productInfos.ContainsKey(type)) return _productInfos[type];
        else return default(ProductInfo);
    }
}
[Serializable]
public struct ProductInfo
{
    public FarmProductType type;
    public Sprite sprite;
    public string description;
    public int price;
    public ProductInfo(FarmProductType type,Sprite sprite,string des,int price)
    {
        this.type = type;
        this.sprite = sprite;
        this.description = des;
        this.price = price;
    }
}
