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
                AddProduction((FarmProductType)i, 0);
            }
        }
    }
    [Button]
    public void AddProduction(FarmProductType type,int num)
    {
        if (Value.ContainsKey(type)) Value.Remove(type);
        Value.Add(type, num);
        SaveData();
    }
    
}
