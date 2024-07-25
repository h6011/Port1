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
        GameObject _prefab = prefabs.BossRifleBullet;

        for (int iNum = 0; iNum < count; iNum++)
        {
            Vector3 _targetPos = playerController.transform.position;


            Vector3 _shotPos = shotposTrs[0].position;
            float angle = Quaternion.FromToRotation(_prefab.transform.up, _targetPos - _shotPos).eulerAngles.z;
            GameObject newBullet = Instantiate(_prefab, _shotPos, Quaternion.Euler(0, 0, angle), playerController.Dynamic);
            Bullet bulletScript = newBullet.GetComponent<Bullet>();

            yield return new WaitForSeconds(0.1f);
        }

        isAttacking = false;
        yield return null;
    }

    IEnumerator shotgunAttack()
    {
        int bulletCount = Random.Range(8, 12 + 1);
        int bulletAngle = 60;

        Vector3 _targetPos = playerController.transform.position;

        GameObject _prefab = prefabs.BossShotgunBullet;

        for (int iNum = 0; iNum < bulletCount; iNum++)
        {
            Vector3 _shotPos = shotposTrs[0].position;
            float angle = Quaternion.FromToRotation(_prefab.transform.up, _targetPos - _shotPos).eulerAngles.z;

            angle += (bulletAngle / bulletCount) * (iNum + 1) 
                + (-bulletAngle / 2); // 이걸 안하면 이상함

            GameObject newBullet = Instantiate(_prefab, _shotPos, Quaternion.Euler(0, 0, angle), playerController.Dynamic);

        }

        isAttacking = false;

        yield return null;
    }


    IEnumerator bombAttack()
    {

        yield return null;
    }




    private void tryAttack()
    {
        isAttacking = true;


        eBossAttackType pickedType = (eBossAttackType)Random.Range(0, getEnumCount());

        Debug.Log("pickedType: " + pickedType);

        if (pickedType == eBossAttackType.Rifle)
        {
            StartCoroutine(rifleAttack());
        }
        else if (pickedType == eBossAttackType.Shotgun)
        {
            StartCoroutine(shotgunAttack());
        }
        else if (pickedType == eBossAttackType.Bomb)
        {
            isAttacking = false;
        }

        isAttacking = false;

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
