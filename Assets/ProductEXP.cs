using Assets._Dev.SO._CustomEvent;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductEXP : EXPCollector
{
    public void OnCollectProduct(ProductNum productNum)
    {
        int numOfType = 4;
        int num = productNum.num * numOfType;
        IncreaseEXP(num);
    }
}
