using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShower : MonoBehaviour
{
    [SerializeField] Ease ease;
    [Button]
    public void Show()
    {
        gameObject.SetActive(true);
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one, 0.5f).SetEase(ease);
    }

    [SerializeField] Ease easeUnshow;
    [Button]
    public void UnShow()
    {
        transform.DOScale(Vector3.zero, 0.2f).SetEase(easeUnshow).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }
}
