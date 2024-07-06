using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ItemBoxUI : MonoBehaviour
{
    [SerializeField] Image img;
    [SerializeField] GameObject greenBackground;
    [SerializeField] TextMeshProUGUI numTxt;


    public void Init(Sprite sprite,int num=0)
    {
        img.gameObject.SetActive(true);
        img.sprite = sprite;
        Highlight(false);
        if (num == 0) return;
        numTxt.SetText(num.ToString());
    }
    public void ResetThis()
    {
        img.gameObject.SetActive(false);
        if(numTxt!=null)
            numTxt.SetText("");
        Highlight(false);
    }

    public void Highlight(bool highlight)
    {
        greenBackground.SetActive(highlight);
    }
    public void AddListener(UnityAction action)
    {
        GetComponent<Button>().onClick.AddListener(action);
    }
}
