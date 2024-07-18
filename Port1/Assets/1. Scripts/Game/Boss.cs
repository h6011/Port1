using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boss : Enemy
{
    private float startSpawnMoveTimer = 0f;
    private bool isStartSpawnMove = false;

    private Vector3 spawnedPos;

    private Vector3 moveOffset = new Vector3(0, -3, 0);





    


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



    private void startSpawnMoveAction()
    {
        transform.position = Vector3.Lerp(spawnedPos, spawnedPos + moveOffset, startSpawnMoveTimer);
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

    public override void GetDamage(int _damage)
    {
        base.GetDamage(_damage);
    }




}
