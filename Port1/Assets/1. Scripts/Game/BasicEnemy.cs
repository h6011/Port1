using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : Enemy
{

    [Header("Basic Enemy Stat")]
    [SerializeField] float speed = 4f;


    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        moveAction();
    }


    private void moveAction()
    {
        transform.Translate(-transform.up * speed * Time.deltaTime);
    }

    


}
