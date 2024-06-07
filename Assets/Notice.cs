using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Notice : MonoBehaviour
{
    TextMeshProUGUI txt;
    public void Awake()
    {
        txt = GetComponent<TextMeshProUGUI>();   
    }
    [SerializeField] float length = 0;
    [SerializeField] float time = 0;
    [Button]
    public void ShowMessage(string message)
    {
        RectTransform rect = GetComponent<RectTransform>();
        rect.anchoredPosition = Vector3.zero;
        txt.SetText(message);
        transform.DOMoveY(transform.position.y+length,time).SetEase(Ease.OutCubic);
    }
}
