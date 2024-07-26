using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorManager : MonoBehaviour
{
    GameManager gameManager;

    public static MeteorManager Instance;

    [Header("Prefab")]
    [SerializeField] GameObject meteorPrefab;

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
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawLine(enemySpawnPos.position + Vector3.left * -randomRadius, enemySpawnPos.position + Vector3.right * randomRadius);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.up);
    }
    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.green;
    //    Gizmos.DrawLine(enemySpawnPos.position + Vector3.left * -randomRadius, enemySpawnPos.position + Vector3.right * randomRadius);
    //}


    private void Start()
    {
        gameManager = GameManager.Instance;

        gameManager.MeteorManager = this;
    }

    public void SpawnMeteor()
    {
        Debug.Log("SpawnMeteor");

        float iRand = Random.Range(-randomRadius, randomRadius);
        GameObject newMeteor = Instantiate(meteorPrefab, enemySpawnPos.position + new Vector3(iRand, 0, 0), Quaternion.Euler(0, 0, 180), dynamic);


    }


}
