using System.Collections;
using UnityEngine;

public class PlayerProductCollectorUI : MonoBehaviour
{
    [SerializeField] float delayCollect;
    public void OnCollectProductGO(GameObject go)
    {
        StartCoroutine(Collect(go));
    }

    private IEnumerator Collect(GameObject go)
    {
        yield return new WaitForSeconds(delayCollect);
        ProductDrop productDrop = go.GetComponent<ProductDrop>();
        productDrop.MoveUIToTarget(transform.position);
    }
}
