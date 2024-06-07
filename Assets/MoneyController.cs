using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoneyController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI moneyNum;
    [SerializeField] IntData moneyData;
    [SerializeField] List<Image> moneyImages;
    [SerializeField] Transform des;
    private int _money;

    public void SetTextMoney(int num)
    {
        _money = num;
        moneyNum.SetText(num.ToString());
    }
    public IEnumerator SetMoney(int money,float delayTime=0)
    {
        int start = _money;
        moneyData.Value=money;
        float time = 0;
        if(delayTime>0) yield return new WaitForSeconds(delayTime);
        while (time < s2)
        {
            time+= Time.deltaTime;
            SetTextMoney((int)Mathf.Lerp(start, money, time / s2));
            yield return null;
        }
        if (time >= s2)
        {
            SetTextMoney(money);
        }
    }
    public void OnMoneySpent()
    {
        SetTextMoney(moneyData.Value);
    }
    public void OnEnable()
    {
        SetTextMoney(moneyData.Value);
    }
    public void OnCollectMoney(int num)
    {
        PlayAnim();
        int newMoney = num+moneyData.Value; 
        StartCoroutine(SetMoney(newMoney,s1*2));
    }
    [SerializeField] float length;
    [Button]
    public void PlayAnim()
    {
        for(int i = 0; i < 6; i++)
        {
            GameObject coin = Instantiate(coinPrefab, transform);
            RectTransform coinRectTransform = coin.GetComponent<RectTransform>();
            coinRectTransform.anchoredPosition = Vector2.zero;
            float angle = i * 2 * Mathf.PI / 6+Random.value;
            Vector2 randomDirection = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * length*(0.5f+0.5f*Random.value)+new Vector2(coinRectTransform.position.x,coinRectTransform.position.y) ;  // Random direction
            StartCoroutine(MoveCoin(coinRectTransform, randomDirection,s1));
        }
        
    }

    [SerializeField] GameObject coinPrefab;
    [SerializeField] float s1;
    [SerializeField] float s2;
    

    [SerializeField] float slerp;

    IEnumerator MoveCoin(RectTransform coinRectTransform, Vector2 targetPosition, float duration)
    {
        Vector2 startPosition = coinRectTransform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            coinRectTransform.position = Vector2.Lerp(coinRectTransform.position, targetPosition, slerp);
            yield return null;
        }
        elapsedTime = 0f;
        startPosition = coinRectTransform.position;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            coinRectTransform.position = Vector2.Lerp(startPosition, des.position, elapsedTime / s1);
            yield return null;
        }
        coinRectTransform.position = des.position;
        Destroy(coinRectTransform.gameObject);
    }

    void AddCoinsToPlayer(int amount)
    {
        // Implement logic to add coins to player's total
        // Example: Player.Instance.AddCoins(amount);
        Debug.Log("Added " + amount + " coins to player.");
    }
}
