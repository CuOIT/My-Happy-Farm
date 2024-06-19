using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class BotCollector : MonoBehaviour
{
    [SerializeField] float delay;
    public void OnCollectItem(GameObject go)
    {
        StartCoroutine(Collect(go));
        
    }


    IEnumerator Collect(GameObject go)
    {
        yield return new WaitForSeconds(delay);
        go.GetComponent<ProductDrop>().MoveToTarget(transform.position);
    }
}
