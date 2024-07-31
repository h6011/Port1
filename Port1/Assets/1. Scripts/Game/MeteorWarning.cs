using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorWarning : MonoBehaviour
{
    [Header("Vars")]

    MeteorManager meteorManager;
    PlayerController playerController;

    float spawnDelay = 2f;
    float followTime = 2f;

    private void Awake()
    {
        meteorManager = MeteorManager.Instance;
        playerController = PlayerController.Instance;
    }

    private void Start()
    {
        StartCoroutine(StartCor());
    }

    IEnumerator StartCor()
    {
        yield return new WaitForSeconds(spawnDelay);
        meteorManager.SpawnMeteorByWarning(transform.position.x);
        Destroy(gameObject);
        yield return null;
    }

    private void Update()
    {
        Vector3 TargetPos = new Vector3(playerController.transform.position.x, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, TargetPos, Time.deltaTime / followTime);
    }


}
