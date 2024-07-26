using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{

    private bool isDestoyed = false;

    public float speed = 6f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDestoyed) return;
        if (collision.CompareTag("Player"))
        {
            PlayerController playerController = collision.GetComponent<PlayerController>();
            bool isSuccess = playerController.TryDamage(3);
            if (isSuccess)
            {
                destroySelf();
            }
        }
    }

    private void Update()
    {
        moveAction();
    }



    private void destroySelf()
    {
        if (isDestoyed) return;
        isDestoyed = true;
        Destroy(gameObject);
    }

    


    private void moveAction()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }


}
