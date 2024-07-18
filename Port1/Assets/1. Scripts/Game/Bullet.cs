using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] bool isOwnerPlayer = true;
    [SerializeField] float bulletSpeed = 3f;
    private int damage = 1;
    bool isDestroyed = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDestroyed) return;
        if (isOwnerPlayer)
        {
            if (collision.CompareTag("Enemy"))
            {
                Enemy enemyScript = collision.GetComponent<Enemy>();
                enemyScript.GetDamage(damage);
                destroySelf();
            }
        }
        else if (!isOwnerPlayer)
        {
            if (collision.CompareTag("Player"))
            {

                destroySelf();
            }
        }
    }

    private void destroySelf()
    {
        isDestroyed = true;
        Destroy(gameObject);
    }

    private void Update()
    {
        moveAction();
    }

    private void moveAction()
    {
        transform.Translate(transform.up * bulletSpeed * Time.deltaTime);
    }

    private void OnBecameInvisible()
    {
        destroySelf();
    }

}