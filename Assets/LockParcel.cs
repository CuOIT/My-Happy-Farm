using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class LockParcel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI moneyNum;
    private ParcelInfoSO moneyUnlock;
    private Parcel parent;
    public void OnEnable()
    {
        parent = GetComponentInParent<Parcel>();
        moneyUnlock = parent.ParcelInfo;
        SetMoney(moneyUnlock.Value);
    }
    public void SetMoney(int num)
    {
        moneyUnlock.Value = num;
        if (num >= 1000000)
        {
            float million = (float)Math.Round((float)num / 1000000,1);
            moneyNum.SetText(million.ToString()+"M");
        }
        else
        {
            if (num >= 1000)
            {
                float thousand = (float)Math.Round((float)num/1000,1);
                moneyNum.SetText(thousand.ToString()+"K");
            }
            else
            {
                moneyNum.SetText(num.ToString());
            }
        }
    }

    private IEnumerator ReduceNumberOverTime(IWallet wallet)
    {
        float startNum = moneyUnlock.Value;
        float duration = 2; 
        float elapsedTime = 0f;
        int currentNumber = (int)startNum;
        bool finished = false;
        int changeMoney;
        int nextNumber;
 
        {
            while (!finished)
            {
                elapsedTime += Time.deltaTime;
                float t = Mathf.Clamp(elapsedTime / duration,0,1);
                if (t == 1)
                {
                    finished= true;
                }
                nextNumber = Mathf.RoundToInt(Mathf.Lerp(startNum, 0, t));
                changeMoney = currentNumber - nextNumber;
                if (wallet.IsEnoughMoney(changeMoney))
                {
                    wallet.SpendMoney(changeMoney,transform);
                    currentNumber= nextNumber;
                    SetMoney(currentNumber);
                    moneyUnlock.Value = currentNumber;
                    if (finished)
                    {
                        BoughtParcel();
                    }
                }
                else
                {
                    finished = true;
                }
                yield return null; 
            }   
        }
    }
    public void BoughtParcel()
    {
        parent?.OnBoughtThisParcel();
    }
    public const int FARMER_LAYER = 7;
    public void OnTriggerEnter(Collider other)
    {
        IWallet wallet = other.GetComponent<IWallet>();
        if (wallet != null)
        {
            StartCoroutine(ReduceNumberOverTime(wallet)); 
        }
    }
    public void OnTriggerExit(Collider other)
    {
        IWallet wallet = other.GetComponent<IWallet>();
        if (wallet!=null)
        {
            StopAllCoroutines();
        }
    }
}
