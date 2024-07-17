using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boss : Enemy
{
    GameManager gameManager;

    private float startSpawnMoveTimer = 0f;
    private bool isStartSpawnMove = false;

    private Vector3 spawnedPos;

    private Vector3 testOffset = new Vector3(0, -3, 0);



    protected override void Awake()
    {
        base.Awake();
        spawnedPos = transform.position;
        isStartSpawnMove = true;
    }

    protected override void Update()
    {
        base.Update();
        timerAction();

        startSpawnMoveAction();
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
    }



    private void startSpawnMoveAction()
    {
        transform.position = Vector3.Lerp(spawnedPos, spawnedPos + testOffset, startSpawnMoveTimer);
    }

    private void timerAction()
    {
        if (isStartSpawnMove == true)
        {
            startSpawnMoveTimer += Time.deltaTime;
            if (startSpawnMoveTimer >= 1)
            {
                isStartSpawnMove = false;
                startSpawnMoveTimer = 1;
            }
        }
    }




}
