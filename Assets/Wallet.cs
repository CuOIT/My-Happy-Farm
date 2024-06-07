using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallet : MonoBehaviour,IWallet
{
    [SerializeField] IntData money;
    [SerializeField] IntEvent spentMoneyEvent;

    [Button]
    public void SpendMoney(int amount,Transform des = null)
    {
        int curMon = money.Value;
        if (curMon < amount)
        {
            Debug.LogError("Not enough money to spent: " + amount.ToString());
        }
        else
        {
            money.Value=curMon-amount;
            if (des)
            {
                StartCoroutine(PlayAnimCoin(transform.position, des.position));
            }
            spentMoneyEvent.RaiseEvent(amount);
        }
    }
    [SerializeField] GameObject coinPrefab;
    private IEnumerator PlayAnimCoin(Vector3 start, Vector3 end)
    {
        GameObject coin = Instantiate(coinPrefab,start,Quaternion.identity);
        float time = 0;
        float duration = 1.0f; // Time it takes for the coin to reach the crop
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
    public bool IsEnoughMoney(int amount)
    {
        return money.Value >= amount;
    }
}
