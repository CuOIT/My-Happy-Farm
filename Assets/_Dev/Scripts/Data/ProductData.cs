using Assets._Dev.SO._CustomEvent;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NewProductData",menuName ="Data/Product")]
public class ProductData : LocalData<Dictionary<FarmProductType,int>>
{
    [SerializeField] List<ProductNum> defaultProductNum;

    public int GetCapacity()
    {
        int num = 0;
        foreach (var item in Value)
        {
            num += item.Value;
        }
        return num;
    }
    
    protected override void Init()
    {
        defaultValue = new Dictionary<FarmProductType, int>();
        if(defaultProductNum != null )
        {
            foreach(var item in defaultProductNum)
            {
                defaultValue[item.type] = item.num;
            }
        }
        base.Init();
        for(int i = 1; i < (int)FarmProductType.COUNT; i++)
        {
            if (!Value.ContainsKey((FarmProductType)i))
            {
                Value.Add((FarmProductType)i, 0);
            }
        }
    }
    [Button]
    public void Add(ProductNum productNum)
    {
        if (!Value.ContainsKey(productNum.type))
        {
            Value.Add(productNum.type, 0);
        }
        Value[productNum.type] += productNum.num;
        SaveData();
    }
    
    public void Consume(ProductNum productNum)
    {
        if (!Value.ContainsKey(productNum.type))
        {
            Value.Add(productNum.type, 0);
            return;
        }
        int num = Value[productNum.type] - productNum.num;
        if (num < 0) { num= 0; }
        Value[productNum.type] = num;
        SaveData();
    }
}
