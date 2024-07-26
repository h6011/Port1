using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected EnemyManager enemyManager;
    protected GameManager gameManager;
    protected PlayerController playerController;
    protected BulletManager bulletManager;

    [Header("Enemy Stat")]
    [SerializeField] protected int hp;
    [SerializeField] protected int maxHp;

    protected float hitAlpha;
    private float hitSpriteTime = 0.05f;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    [SerializeField] eEnemyType enemyType;
    public bool isUpgraded = false;
    bool isDead = false;



    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            bool isSuccess = playerController.TryDamage(1);
            if (isSuccess)
            {
                GetDamage(1);
            }
        }
    }

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected virtual void Start()
    {
        enemyManager = EnemyManager.Instance;
        gameManager = GameManager.Instance;
        playerController = PlayerController.Instance;
        bulletManager = BulletManager.Instance;

        fillHp();
        checkColorChange();
    }

    private void fillHp()
    {
        if (isUpgraded)
        {
            maxHp *= 2;
        }
        hp = maxHp;
    }

    private void checkColorChange()
    {
        if (isUpgraded)
        {
            Color _color = spriteRenderer.color;
            _color.g = 0.5f;
            _color.b = 0.5f;
            spriteRenderer.color = _color;
        }
    }


    public void Upgrade()
    {
        isUpgraded = true;
    }


    public void getDestroy(bool ByPlayer = false)
    {
        if (isDead) return;
        isDead = true;
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

    protected virtual void OnBecameInvisible()
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
