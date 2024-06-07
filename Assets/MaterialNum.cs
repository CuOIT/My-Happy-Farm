using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MaterialNum : MonoBehaviour
{
    Image img;
    TextMeshProUGUI txt;

     void Awake()
    {
        img = GetComponent<Image>();
        txt = GetComponentInChildren<TextMeshProUGUI>();
    }
    public void Init(Sprite sprite,int num)
    {
        gameObject.SetActive(true);
        if (img == null) img = GetComponent<Image>();
        img.sprite = sprite;
        if(txt==null) txt= GetComponentInChildren<TextMeshProUGUI>();
        txt.SetText(num.ToString());
    }
}
