using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

public class ProductDrop : MonoBehaviour,IPooler
{
    Rigidbody _rb;
    [SerializeField] float forceMulti;
    [SerializeField] GameObject visualFruitDrop;
    [SerializeField] Transform fruitUIDrop;
    public void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    public void AutoDespawn()
    {
        GameManager.Instance.pooler.DespawnObject(gameObject); 
    }
    public void OnDeSpawn()
    {
        
    }

    public void MoveUIToTarget(Vector3 des)
    {
        visualFruitDrop.SetActive(false);
        fruitUIDrop.gameObject.SetActive(true);
        fruitUIDrop.transform.position = Camera.main.WorldToScreenPoint(visualFruitDrop.transform.position);
        fruitUIDrop.DOMove(des, 1).OnComplete(AutoDespawn);
    }public void MoveToTarget(Vector3 des)
    {
        transform.DOMove(des, 1).OnComplete(AutoDespawn);
    }
    public void OnSpawn()
    {
        visualFruitDrop.SetActive(true);
        fruitUIDrop.gameObject.SetActive(false);
        float x = (0.5f + Random.value) * (Random.value > 0.5f ? 1 : -1);
        float z = (0.5f + Random.value) * (Random.value > 0.5f ? 1 : -1);
        float y = 3;
        _rb.AddForce(new Vector3(x, y, z) * forceMulti, ForceMode.Impulse);
    }
}
