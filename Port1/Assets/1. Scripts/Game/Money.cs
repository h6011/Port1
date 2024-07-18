using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eMoneyType
{
    Coin = 1,
    Ruby = 10,
    Diamond = 20,
    Emerald = 30,
}

public class Money : MonoBehaviour
{
    GameManager gameManager;

    [SerializeField] eMoneyType moneyType;

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !gameManager.IsGameOver)
        {
            gameManager.GetMoney(transform, moneyType);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }



}
