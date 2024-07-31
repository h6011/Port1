using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorManager : MonoBehaviour
{
    GameManager gameManager;

    public static MeteorManager Instance;

    [Header("Prefab")]
    [SerializeField] GameObject meteorPrefab;
    [SerializeField] GameObject meteorWarningPrefab;

    [Header("Trs")]
    [SerializeField] Transform enemySpawnPos;
    [SerializeField] Transform dynamic;

    [Header("Spawn")]
    [SerializeField] float randomRadius = 2f;

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

        gameManager.MeteorManager = this;
    }

    public void SpawnMeteorByWarning(float x)
    {
        GameObject newMeteor = Instantiate(meteorPrefab, enemySpawnPos.position + new Vector3(x, 0, 0), Quaternion.Euler(0, 0, 180), dynamic);


    }

    public void TrySpawnMeteor()
    {
        float iRand = Random.Range(-randomRadius, randomRadius);
        GameObject newMeteorWarning = Instantiate(meteorWarningPrefab, enemySpawnPos.position + new Vector3(iRand, -2f, 0), Quaternion.identity, dynamic);
    }

    public void SpawnMeteor()
    {
        Debug.Log("SpawnMeteor");

        float iRand = Random.Range(-randomRadius, randomRadius);
        GameObject newMeteor = Instantiate(meteorPrefab, enemySpawnPos.position + new Vector3(iRand, 0, 0), Quaternion.Euler(0, 0, 180), dynamic);


    }


}
