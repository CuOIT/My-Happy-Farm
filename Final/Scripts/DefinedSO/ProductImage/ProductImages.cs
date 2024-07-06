using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="NewProductImages",menuName ="ProductImages")]
public class ProductImages : ScriptableObject
{
    public List<ProductImage> productImages;

    public Sprite GetSprite(FarmProductType type)
    {
        return productImages.Find(x => x.type == type).sprite;
    }
}

[System.Serializable]
public struct ProductImage
{
    public FarmProductType type;
    public Sprite sprite;
}
