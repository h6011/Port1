using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    BulletManager bulletManager;
    

    [SerializeField] bool isOwnerPlayer = true;
    [SerializeField] float bulletSpeed = 3f;
    public float BulletSpeed => bulletSpeed;

    int damage = 1;
    bool isDestroyed = false;

    [SerializeField] bool isBomb = false;
    [SerializeField] float bombTimer;

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
                PlayerController playerController = collision.GetComponent<PlayerController>();
                bool isSuccess = playerController.TryDamage(damage);
                if (isSuccess)
                {
                    destroySelf();
                }
            }
        }
    }

    private void Start()
    {
        bulletManager = BulletManager.Instance;
    }


    private void Update()
    {
        moveAction();
        bombAction();
    }

    private void bomb()
    {
        destroySelf();

        bulletManager.BombBulletGotBomb(transform.position);


    }

    private void bombAction()
    {
        if (isBomb)
        {
            bombTimer -= Time.deltaTime;

            if (bombTimer <= 0)
            {
                bombTimer = 0;

                bomb();

                isBomb = false;
            }
        }
    }

    public void MakeToBomb(float _time)
    {
        bombTimer = _time;
        isBomb = true;
    }
    private void destroySelf()
    {
        isDestroyed = true;
        Destroy(gameObject);
    }


    

    private void moveAction()
    {
        transform.Translate(Vector3.up * bulletSpeed * Time.deltaTime);
    }

    private void OnBecameInvisible()
    {
        destroySelf();
    }

}
