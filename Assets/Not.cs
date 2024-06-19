using Assets._Dev.SO._CustomEvent;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Not : MonoBehaviour
{
    [SerializeField] ProductData productData;
    // Start is called before the first frame update
    public bool HaveBarn(ProductNum productNum)
    {
        if (!productData.Value.ContainsKey(productNum.type))
        {
            return false;
        }
        else
        {
            return productData.Value[productNum.type] > productNum.num;
        }
    }

    public void FeedAnimal(ProductNum productNum)
    {
        var newValue = productData.Value;
        newValue[productNum.type] -= productNum.num;
        productData.Value = newValue;
    }
}
public interface IFeeder
{
}