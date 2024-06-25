using Assets._Dev.SO._CustomEvent;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MaterialController : MonoBehaviour
{
    [SerializeField] ProductInfos productInfos;
    [SerializeField] List<MaterialNum> matNum;
    [SerializeField] ProductData barnData;

    public void InitMaterials(List<ProductNum> products)
    {      
        int n = products.Count;
        for(int i=0;i<matNum.Count;i++)
        {
            if (i < n)
            {
                matNum[i].Init(productInfos.GetProductInfoOfType(products[i].type).sprite, products[i].num);
            }
            else
            {
                matNum[i].gameObject.SetActive(false);
            }
        }
    }
}
