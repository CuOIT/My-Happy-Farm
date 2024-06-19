using Assets._Dev.SO._CustomEvent;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarnCollector : MonoBehaviour
{
    [SerializeField] ProductData _productData;
    public void AddProduct(ProductNum productNum)
    {
        _productData.Add(productNum);
    }
}
