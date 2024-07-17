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
        gameManager = GameManager.Instance;
        gameManager.EnemyManager = this;
    }


    public void SpawnEnemy(eEnemyType enemyType, Vector3 offset = default)
    {
        if (enemyType == eEnemyType.BasicEnemy)
        {
            GameObject newEnemy = Instantiate(basicEnemy, enemySpawnPos.position + offset, Quaternion.identity, dynamic);
        }
        else if (enemyType == eEnemyType.Boss)
        {
            GameObject newEnemy = Instantiate(boss, enemySpawnPos.position + offset, Quaternion.identity, dynamic);
        }
    }



}
