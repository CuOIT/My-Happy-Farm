using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class EXPUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI txt;

    [SerializeField] float length;
    [SerializeField] float time;
    public void OnEnable()
    {
        Invoke(nameof(Despawn), time);
    }

    public void Despawn()
    {
        GameManager.Instance.pooler.DespawnObject(gameObject);
    }
    public void ShowEXP(int num)
    {
        txt.SetText("+" + num);
        RectTransform rect = GetComponent<RectTransform>();
        rect.anchoredPosition = Vector3.zero;
        transform.DOMoveY(transform.position.y + length, time).SetEase(Ease.OutQuint);
    }
}
