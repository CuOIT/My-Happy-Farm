using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseContainer : MonoBehaviour
{
    [SerializeField] protected ProductData productData;
    [SerializeField] protected ProductInfos productInfos;

    protected ItemBoxUI currentItem;
    [SerializeField] protected GameObject detail;

    protected Dictionary<ItemBoxUI, ProductInfo> mapProductInfo;

    public void OnEnable()
    {
        GetAllProduct();
    }   

    public virtual void GetAllProduct()
    {
        
    }

    public virtual void SetCurrentItem(ItemBoxUI item)
    {
        if (currentItem != null)
        {
            currentItem.Highlight(false);
        }
        currentItem = item;
        currentItem?.Highlight(true);
    }

    public virtual void Show() {
        detail.SetActive(true);
    }

    public virtual void UnShow()
    {
        detail.SetActive(false);
        SetCurrentItem(null);
    }
}
