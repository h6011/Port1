using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance;

    [SerializeField] Transform dynamic;

    [Header("Prefab")]
    [SerializeField] GameObject coin;
    [SerializeField] GameObject ruby;
    [SerializeField] GameObject diamond;
    [SerializeField] GameObject emerald;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        
    }


    private void dropMoney(eMoneyType moneyType, Vector3 position, float force = 100)
    {
        GameObject targetMoney = null;
        bool isCoin = moneyType == eMoneyType.Coin;

        if (moneyType == eMoneyType.Coin)
        {
            targetMoney = coin;
        }
        else if (moneyType == eMoneyType.Ruby)
        {
            targetMoney = ruby;
        }
        else if (moneyType == eMoneyType.Diamond)
        {
            targetMoney = diamond;
        }
        else if (moneyType == eMoneyType.Emerald)
        {
            targetMoney = emerald;
        }

        GameObject newCoin = Instantiate(targetMoney, position, Quaternion.identity, dynamic);
        Rigidbody2D rb = newCoin.GetComponent<Rigidbody2D>();

        float _x = Random.Range(-force, force);
        float _y = Random.Range(-force / 2, force / 2);

        float _torque = Random.Range(-force / 2, force / 2);

        rb.AddForce(new Vector2(_x, _y));
        if (!isCoin)
        {
            rb.AddTorque(_torque);
        }
    }

    public void DropMoney(eMoneyType moneyType, Vector3 position, int amount, float force = 100)
    {
        GameObject targetMoney = null;
        bool isCoin = moneyType == eMoneyType.Coin;

        if (moneyType == eMoneyType.Coin)
        {
            targetMoney = coin;
        }
        else if (moneyType == eMoneyType.Ruby)
        {
            targetMoney = ruby;
        }
        else if (moneyType == eMoneyType.Diamond)
        {
            targetMoney = diamond;
        }
        else if (moneyType == eMoneyType.Emerald)
        {
            targetMoney = emerald;
        }

        for (int iNum = 0; iNum < amount; iNum++)
        {
            GameObject newCoin = Instantiate(targetMoney, position, Quaternion.identity, dynamic);
            Rigidbody2D rb = newCoin.GetComponent<Rigidbody2D>();

            float _x = Random.Range(-force, force);
            float _y = Random.Range(-force / 2, force / 2);

            float _torque = Random.Range(-force / 2, force / 2);

            rb.AddForce(new Vector2(_x, _y));
            if (!isCoin)
            {
                rb.AddTorque(_torque);
            }
        }
    }


}
