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
            Enemy enemyScript = newEnemy.GetComponent<Enemy>();
            if (isUpgraded)
            {
                enemyScript.Upgrade();
            }
        }
        else if (enemyType == eEnemyType.Boss)
        {
            GameObject newEnemy = Instantiate(boss, enemySpawnPos.position + offset, Quaternion.identity, dynamic);
        }
    }

    



    public void WhenEnemyDied(Transform enemyTrs, eEnemyType enemyType, bool isUpgraded = false)
    {
        SpriteRenderer spriteRenderer = enemyTrs.GetComponent<SpriteRenderer>();

        float explosionSize = Mathf.Max(spriteRenderer.sprite.rect.width, spriteRenderer.sprite.rect.height);

        effectManager.CreateEffect(eEffectType.Explosion1, enemyTrs.position, explosionSize);

        eEnemyTypeMoneySettings _settings = probabilityManager.GetEnemyTypeMoneySettings(enemyType);

        float CoinPercentage = _settings.Coin.percentage;
        float RubyPercentage = _settings.Ruby.percentage;
        float DiamondPercentage = _settings.Diamond.percentage;
        float EmeraldPercentage = _settings.Emerald.percentage;

        if (isUpgraded)
        {
            float upgradeAlpha = 2.0f;
            CoinPercentage *= upgradeAlpha;
            RubyPercentage *= upgradeAlpha;
            DiamondPercentage *= upgradeAlpha;
            EmeraldPercentage *= upgradeAlpha;
        }
        bool coinS = probabilityManager.isSuccess(CoinPercentage);
        bool rubyS = probabilityManager.isSuccess(RubyPercentage);
        bool diamondS = probabilityManager.isSuccess(DiamondPercentage);
        bool emeraldS = probabilityManager.isSuccess(EmeraldPercentage);

        gameManager.GetScoreByEnemyType(enemyType);        


        if (coinS)
        {
            int _amount = Random.Range(_settings.Coin.randomMin, _settings.Coin.randomMax + 1);
            if (isUpgraded) _amount *= 2;
            moneyManager.DropMoney(eMoneyType.Coin, enemyTrs.position, _amount);
        }

        if (rubyS)
        {
            int _amount = Random.Range(_settings.Ruby.randomMin, _settings.Ruby.randomMax + 1);
            if (isUpgraded) _amount *= 2;
            moneyManager.DropMoney(eMoneyType.Ruby, enemyTrs.position, _amount);
        }

        if (diamondS)
        {
            int _amount = Random.Range(_settings.Diamond.randomMin, _settings.Diamond.randomMax + 1);
            if (isUpgraded) _amount *= 2;
            moneyManager.DropMoney(eMoneyType.Diamond, enemyTrs.position, _amount);
        }

        if (emeraldS)
        {
            int _amount = Random.Range(_settings.Emerald.randomMin, _settings.Emerald.randomMax + 1);
            if (isUpgraded) _amount *= 2;
            moneyManager.DropMoney(eMoneyType.Emerald, enemyTrs.position, _amount);
        }



    }



}
