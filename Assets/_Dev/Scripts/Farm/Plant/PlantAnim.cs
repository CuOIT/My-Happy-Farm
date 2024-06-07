using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantAnim : MonoBehaviour
{
    public void OnEnable()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one, 1);
    }
}
