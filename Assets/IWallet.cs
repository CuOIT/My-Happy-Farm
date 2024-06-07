using UnityEngine;

public interface IWallet
{
    void SpendMoney(int amount,Transform des=null);
    bool IsEnoughMoney(int amount);
}