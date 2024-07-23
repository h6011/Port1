using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boss : Enemy
{

    [System.Serializable]
    public class BossPrefabVars
    {
        public GameObject BossRifleBullet;
        public GameObject BossShotgunBullet;
        public GameObject BossBombBullet;
    }


    [SerializeField] BossPrefabVars prefabs;

    [SerializeField] List<Transform> shotposTrs;
    [SerializeField] Transform dynamic;

    private float startSpawnMoveTimer = 0f;
    private bool isStartSpawnMove = false;

    private Vector3 spawnedPos;

    private Vector3 moveOffset = new Vector3(0, -3, 0);

    private float attackDelay = 5f;
    private float attackTimer;
    private bool isAttacking = false;

    public enum eBossAttackType
    {
        Rifle,
        Shotgun,
        Bomb,
    }

    private int getEnumCount()
    {
        int count = System.Enum.GetValues(typeof(eBossAttackType)).Length;
        return count;
    }


    protected override void Awake()
    {
        base.Awake();
        spawnedPos = transform.position;
        isStartSpawnMove = true;
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        timerAction();

        startSpawnMoveAction();
    }

    protected override void OnBecameInvisible()
    {
        //base.OnBecameInvisible();
    }

    IEnumerator rifleAttack()
    {
        int count = Random.Range(10, 15 + 1);

        for (int iNum = 0; iNum < count; iNum++)
        {
            int shotposTrsCount = shotposTrs.Count;

            for (int i = 0; i < shotposTrsCount; i++)
            {
                GameObject newBullet = Instantiate(prefabs.BossRifleBullet, shotposTrs[i].position, Quaternion.Euler(0, 0, 180), playerController.Dynamic);
                Bullet bulletScript = newBullet.GetComponent<Bullet>();
            }

            yield return new WaitForSeconds(0.1f);
        }

        isAttacking = false;
        yield return null;
    }

    IEnumerator shotgunAttack()
    {

        yield return null;
    }


    IEnumerator bombAttack()
    {

        yield return null;
    }




    private void tryAttack()
    {
        isAttacking = true;


        eBossAttackType pickedType = eBossAttackType.Rifle;//(eBossAttackType)Random.Range(0, getEnumCount() + 1);

        Debug.Log("pickedType: " + pickedType);

        if (pickedType == eBossAttackType.Rifle)
        {
            StartCoroutine(rifleAttack());
        }
        else if (pickedType == eBossAttackType.Shotgun)
        {
            isAttacking = false;
        }
        else if (pickedType == eBossAttackType.Bomb)
        {
            isAttacking = false;
        }

        //isAttacking = false;

    }


    private void startSpawnMoveAction()
    {
        transform.position = Vector3.Lerp(spawnedPos, spawnedPos + moveOffset, startSpawnMoveTimer);
    }

    private void timerAction()
    {
        if (isStartSpawnMove == true)
        {
            startSpawnMoveTimer += Time.deltaTime;
            if (startSpawnMoveTimer >= 1)
            {
                isStartSpawnMove = false;
                startSpawnMoveTimer = 1;
            }
        }
        else
        {
            if (isAttacking) return;
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackDelay)
            {
                attackTimer = 0f;

                tryAttack();

            }
        }
    }

    public override void GetDamage(int _damage)
    {
        base.GetDamage(_damage);
    }




}
