using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum eEnemyType
{
    BasicEnemy,
    Boss,
}

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    GameManager gameManager;
    MoneyManager moneyManager;
    EffectManager effectManager;

    ProbabilityManager probabilityManager;

    [SerializeField] Transform dynamic;


    [Header("Prefab")]
    [SerializeField] GameObject basicEnemy;
    [SerializeField] GameObject boss;

    [Header("Pos")]
    [SerializeField] Transform enemySpawnPos;



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
        moneyManager = MoneyManager.Instance;
        probabilityManager = ProbabilityManager.Instance;
        effectManager = EffectManager.Instance;

        gameManager = GameManager.Instance;
        gameManager.EnemyManager = this;
    }


    public void SpawnEnemy(eEnemyType enemyType, bool isUpgraded, Vector3 offset = default)
    {
        if (enemyType == eEnemyType.BasicEnemy)
        {
            GameObject newEnemy = Instantiate(basicEnemy, enemySpawnPos.position + offset, Quaternion.identity, dynamic);
            if (isUpgraded)
            {
                SpriteRenderer spriteRenderer = newEnemy.GetComponent<SpriteRenderer>();
                Color _color = spriteRenderer.color;
                _color.g = 0.5f;
                _color.b = 0.5f;
                spriteRenderer.color = _color;
            }
        }
        else if (enemyType == eEnemyType.Boss)
        {
            GameObject newEnemy = Instantiate(boss, enemySpawnPos.position + offset, Quaternion.identity, dynamic);
        }
    }

    



    public void WhenEnemyDied(Transform enemyTrs, eEnemyType enemyType, bool isUpgraded = false)
    {
        effectManager.CreateEffect(eEffectType.Explosion1, enemyTrs.position);

        float coinPercentage = 100;
        float rubytPercentage = 100;
        float diamondPercentage = 100;
        float emeraldPercentage = 100;

        if (enemyType == eEnemyType.BasicEnemy)
        {
            coinPercentage = 100f;
            rubytPercentage = 40f;
            diamondPercentage = 10f;
            emeraldPercentage = 5f;
        }
        else if (enemyType == eEnemyType.Boss)
        {
            coinPercentage = 100f;
            rubytPercentage = 40f;
            diamondPercentage = 10f;
            emeraldPercentage = 5f;
        }

        bool coinS = probabilityManager.isSuccess(coinPercentage);
        bool rubyS = probabilityManager.isSuccess(rubytPercentage);
        bool diamondS = probabilityManager.isSuccess(diamondPercentage);
        bool emeraldS = probabilityManager.isSuccess(emeraldPercentage);

        gameManager.GetScoreByEnemyType(enemyType);        

        if (coinS)
        {
            int _amount = Random.Range(3, 9);
            if (isUpgraded) _amount *= 2;
            moneyManager.DropMoney(eMoneyType.Coin, enemyTrs.position, _amount);
        }

        if (rubyS)
        {
            int _amount = Random.Range(2, 5);
            if (isUpgraded) _amount *= 2;
            moneyManager.DropMoney(eMoneyType.Ruby, enemyTrs.position, _amount);
        }

        if (diamondS)
        {
            int _amount = Random.Range(1, 4);
            if (isUpgraded) _amount *= 2;
            moneyManager.DropMoney(eMoneyType.Diamond, enemyTrs.position, _amount);
        }

        if (emeraldS)
        {
            int _amount = Random.Range(1, 3);
            if (isUpgraded) _amount *= 2;
            moneyManager.DropMoney(eMoneyType.Emerald, enemyTrs.position, _amount);
        }



    }



}
