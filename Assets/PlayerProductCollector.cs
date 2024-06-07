using Assets._Dev.Scripts;
using Assets._Dev.SO._CustomEvent;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProductCollector : MonoBehaviour, ICollector
{
    [SerializeField] ProductData products;

    public void Start()
    {
        InitProductNum();
    }

    public void InitProductNum()
    {

    }
    public bool HaveProduct(ProductNum productNum)
    {
        if (!products.Value.ContainsKey(productNum.type))
        {
            return false;
        }
        else
        {
            return products.Value[productNum.type] >= productNum.num;
        }
    }

    public void Collect(ProductNum productNum)
    {
        Dictionary<FarmProductType, int> dic=products.Value;
        int newValue = dic[productNum.type] + productNum.num;
        dic[productNum.type] = newValue;
        products.Value = dic;
    }

    public void Consume(ProductNum productNum)
    {
        Dictionary<FarmProductType, int> dic = products.Value;
        int newValue = Mathf.Clamp(dic[productNum.type] - productNum.num,0,99999999);
        dic[productNum.type] = newValue;
        products.Value = dic;
    }
   
    public void OnCollectProductGO(GameObject go)
    {
        ProductDrop productDrop = go.GetComponent<ProductDrop>();
        productDrop.MoveUIToTarget(transform.position);
    }
}
