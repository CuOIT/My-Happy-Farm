using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    private MoneyController wallet;
    List<Coroutine> cor;
    private void Start()
    {
        wallet = GameManager.Instance.moneyController;
        cor=new List<Coroutine>();
    }

    [SerializeField] GameObject coinPrefab;
    private IEnumerator PlayAnimCoin(Vector3 start, Vector3 end)
    {
        GameObject coin = Instantiate(coinPrefab,start,Quaternion.identity);
        float time = 0;
        float duration = 0.5f; // Time it takes for the coin to reach the crop
        Vector3 midPoint = (start + end) / 2 + Vector3.up * 2; // Create a midpoint for the arc

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;

            // Quadratic Bezier curve
            Vector3 m1 = Vector3.Lerp(start, midPoint, t);
            Vector3 m2 = Vector3.Lerp(midPoint, end, t);
            coin.transform.position = Vector3.Lerp(m1, m2, t);

            yield return null;
        }

        Destroy(coin);

    }
    
    public void OnTriggerEnter(Collider other)
    {
        ILockParcel lockParcel = other.GetComponent<ILockParcel>();
        if (lockParcel != null )
        {
            cor.Add(StartCoroutine(SpendMoneyToUnlock(lockParcel)));
        }
    }

    public void OnTriggerExit(Collider other)
    {
        ILockParcel lockParcel = other.GetComponent<ILockParcel>();
        if (lockParcel != null)
        {
            for (int i = cor.Count-1; i >=0; i--) {
                StopCoroutine(cor[i]);
                cor.RemoveAt(i);
            }
        }
    }

    IEnumerator SpendMoneyToUnlock(ILockParcel lockParcel)
    {
        int startNum = lockParcel.GetCost();
        float duration = 2;
        float elapsedTime = 0;
        int currentNum = startNum;
        bool finished = false;
        int changeMoney;
        int nextNum;
        int maxCoin = 5;
        int coin = 0;
        while (!finished)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp(elapsedTime / duration, 0, 1);
            if (t == 1)
            {
                finished = true;
            }
            nextNum = Mathf.RoundToInt(Mathf.Lerp(startNum, 0, t));
            changeMoney = currentNum - nextNum;
            if (wallet.HaveMoney(changeMoney))
            {
                wallet.SpendMoney(changeMoney);
                currentNum = nextNum;
                lockParcel.SetMoney(currentNum);
                if (coin == 0)
                {
                    StartCoroutine(PlayAnimCoin(transform.position, lockParcel.GetPos()));
                }
                coin = (coin + 1) % maxCoin;
                if (currentNum <= 0)
                {
                    finished = true;
                }
            }
            else{
                finished = true;
            }
            yield return null;
        }
    }
}
