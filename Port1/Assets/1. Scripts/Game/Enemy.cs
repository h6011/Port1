using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected EnemyManager enemyManager;
    protected GameManager gameManager;

    [Header("Enemy Stat")]
    [SerializeField] protected int hp;
    [SerializeField] protected int maxHp;

    protected float hitAlpha;
    private float hitSpriteTime = 0.05f;

    private Animator animator;

    [SerializeField] eEnemyType enemyType;
    public bool isUpgraded = false;

    //protected virtual void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Bullet"))
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        hp = maxHp;
    }

    protected virtual void Start()
    {
        enemyManager = EnemyManager.Instance;
        gameManager = GameManager.Instance;
    }


    public void getDestroy(bool ByPlayer = false)
    {
        if (ByPlayer)
        {
            enemyManager.WhenEnemyDied(transform, enemyType, isUpgraded);
        }
        if (enemyType == eEnemyType.Boss)
        {
            gameManager.isBossStage = false;
        }
        Destroy(gameObject);
    }
    

    public virtual void GetDamage(int _damage)
    {
        hp -= _damage;
        hitAlpha = 1;
        if (hp <= 0)
        {
            getDestroy(true);
        }
    }

    protected virtual void Update()
    {
        hitAlphaAction();
        animationAction();
    }

    protected void OnBecameInvisible()
    {
        getDestroy();
    }
    private void hitAlphaAction()
    {
        hitAlpha -= Time.deltaTime / hitSpriteTime;
        hitAlpha = Mathf.Clamp01(hitAlpha);
    }

    private void animationAction()
    {
        animator.SetFloat("HitAlpha", hitAlpha);
    }


}
