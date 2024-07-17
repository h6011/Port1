using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected int hp;
    [SerializeField] protected int maxHp;

    protected float hitAlpha;
    private float hitSpriteTime = 0.05f;

    Animator animator;

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

    

    public void GetDamage(int _damage)
    {
        hp -= _damage;
        hitAlpha = 1;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    protected virtual void Update()
    {
        hitAlphaAction();
        animationAction();
    }

    protected void OnBecameInvisible()
    {
        Destroy(gameObject);
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
