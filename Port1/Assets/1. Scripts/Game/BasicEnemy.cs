using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : Enemy
{

    [SerializeField] float speed = 4f;


    private void Update()
    {
        moveAction();
    }
    

    private void moveAction()
    {
        transform.Translate(-transform.up * speed * Time.deltaTime);
    }


}
