using DG.Tweening;
using JetBrains.Annotations;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ProductUIDrop : MonoBehaviour, IPooler
{
    public FarmProductType type;
    public Image image;
    public ProductImages productImages;
    public void OnDeSpawn()
    {
    }

    public void AutoDespawm()
    {
        GameManager.Instance.pooler.DespawnObject(gameObject);
    }
    public void OnSpawn()
    {
    }

    public void SetImage(FarmProductType type)
    {
        image.sprite = productImages.GetSprite(type);
    }
    public void MoveToUIDestination(Transform target)
    {
        if(target == null)
        {
            AutoDespawm();
        }
        else
        {
            StartCoroutine(MoveToUIDes(target));
        }
    }
    private float duration = 1;
    public IEnumerator MoveToUIDes(Transform target)
    {
        float spentTime = 0;
        Vector3 startPos = transform.position;
        while (spentTime < duration)
        {
            transform.position = Vector3.Lerp(startPos, target.position,spentTime/duration);
            spentTime += Time.deltaTime;
            yield return null;
        }
        if(spentTime>=duration) {
            AutoDespawm();
        }
    }

}
