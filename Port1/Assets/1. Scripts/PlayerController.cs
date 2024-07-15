using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
public class PlayerController : MonoBehaviour
{
    Animator animator;
    CapsuleCollider2D capsuleCollider;
    Bounds bounds;

    Vector2 moveDir;

    [Header("Stat")]
    [SerializeField] float speed = 1f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        bounds = capsuleCollider.bounds;
    }

    private void Start()
    {
        LockInCamera.Instance.SetPlayer(capsuleCollider.size);
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

        transform.Translate(new Vector2(x, y) * speed * Time.deltaTime);

        moveDir.x = x;
        moveDir.y = y;

        //LockInCamera.Instance.CheckPosition(transform, capsuleCollider.bounds);
        LockInCamera.Instance.CheckPosition(transform);
    }

    private void animationAction()
    {
        animator.SetInteger("Horizontal", (int)moveDir.x);
    }




}
