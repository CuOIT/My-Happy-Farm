using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCropTypeList", menuName = "Crop/CropTypeList")]
public class CropTypeList : ScriptableObject
{
    public List<Plant> plantList;
}

[System.Serializable]
public struct Plant
{
    public FarmProductType plantType;
    public GameObject smallPlant;
    public GameObject mediumPlant;
    public GameObject collectableItem;
    public int price;
}
public enum FarmProductType
{
    NONE,
    RICE,
    TOMATO,
    WATERMELON,
    CORN,
    PINEAPPLE,
    EGGPLANT,
    CHICKEN_BRAN,
    SHEEP_BRAN,
    COW_BRAN,
    EGG,
    MILK,
    WOOL,
    POWDER,
    SUGAR,
    BREAD,
    FRUIT_JUICE,
    PASTA,
    CAKE,
    SOUP,
    COUNT,
}
