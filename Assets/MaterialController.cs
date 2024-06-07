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
    List<MaterialNum> matNum;
    [SerializeField] ProductData barnData;

    public void Awake()
    {
        matNum = GetComponentsInChildren<MaterialNum>().ToList();
    }
    public void InitMaterials(List<ProductNum> products)
    {
        if (matNum == null) matNum = GetComponentsInChildren<MaterialNum>().ToList();
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
