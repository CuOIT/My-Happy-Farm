using Assets._Dev.SO._CustomEvent;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductEXP : EXPCollector
{
    [SerializeField] List<ProductNum> productEXPs;
    public void OnCollectProduct(ProductNum productNum)
    {
        int numOfType = 1;
        try {
            numOfType = productEXPs.Find(e => e.type == productNum.type).num;
        }
        catch
        {
            numOfType = 1;
        }
        int num = productNum.num * numOfType;
        IncreaseEXP(num);
    }
}
