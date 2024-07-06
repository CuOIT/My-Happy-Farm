using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonType: MonoBehaviour
{
    private int index;
    [SerializeField] Image img;

    [SerializeField] List<Sprite> sprites;
    [SerializeField] List<FarmProductType> types;
    public FarmProductType GetPlantType()
    {
        index = (index + 1) % sprites.Count;
        img.sprite = sprites[index];
        return types[index];
    }
}
