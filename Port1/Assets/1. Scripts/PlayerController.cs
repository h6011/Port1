using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
public class PlayerController : MonoBehaviour
{
    Animator animator;
    CapsuleCollider2D capsuleCollider;

    Vector2 moveDir;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }


    private void Update()
    {
        moveAction();
        animationAction();
    }


   

    private void moveAction()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        transform.Translate(new Vector2(x, y) * Time.deltaTime);

        moveDir.x = x;
        moveDir.y = y;


    }

    private void animationAction()
    {
        animator.SetInteger("Horizontal", (int)moveDir.x);
    }




}
