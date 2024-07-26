using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Boss : Enemy
{
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

    private void shotRifle(Vector3 _shotPos, float angle)
    {
        bulletManager.MakeEnemyBullet(_shotPos, angle, BulletManager.eEnemyBulletType.Rifle);
    }

    private void shotShotgun(int bulletCount, float bulletAngle)
    {
        Vector3 _targetPos = playerController.transform.position;

        GameObject _prefab = bulletManager.Prefabs.ShotgunBullet;

        for (int iNum = 0; iNum < bulletCount; iNum++)
        {
            Vector3 _shotPos = shotposTrs[0].position;
            float angle = Quaternion.FromToRotation(_prefab.transform.up, _targetPos - _shotPos).eulerAngles.z;

            angle += (bulletAngle / bulletCount) * (iNum + 1)
                + (-bulletAngle / 2); // 이걸 안하면 이상함

            bulletManager.MakeEnemyBullet(_shotPos, angle, BulletManager.eEnemyBulletType.Shotgun);

        }
    }



    IEnumerator rifleAttack()
    {
        int count = Random.Range(10, 15 + 1);
        GameObject _prefab = bulletManager.Prefabs.RifleBullet;

        for (int iNum = 0; iNum < count; iNum++)
        {
            Vector3 _targetPos = playerController.transform.position;
            Vector3 _shotPos = shotposTrs[0].position;
            float angle = Quaternion.FromToRotation(_prefab.transform.up, _targetPos - _shotPos).eulerAngles.z;
            shotRifle(_shotPos, angle);

            yield return new WaitForSeconds(0.1f);
        }

        isAttacking = false;
        yield return null;
    }

    IEnumerator shotgunAttack()
    {
        int bulletCount = Random.Range(8, 12 + 1);
        int bulletAngle = 60;

        for (int i = 0; i < 3; i++)
        {
            shotShotgun(bulletCount, bulletAngle);

            yield return new WaitForSeconds(1f);
        }

        isAttacking = false;

        yield return null;
    }


    IEnumerator bombAttack()
    {
        Vector3 _shotPos = shotposTrs[0].position;

        int bulletCount = 2;

        for (int i = 0; i < bulletCount; i++)
        {
            Vector3 _targetPos = playerController.transform.position;
            float angle = Quaternion.FromToRotation(Vector3.up, _targetPos - _shotPos).eulerAngles.z;

            bulletManager.MakeEnemyBullet(_shotPos, angle, BulletManager.eEnemyBulletType.Bomb);

            yield return new WaitForSeconds(1.5f);
        }


        isAttacking = false;

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
            StartCoroutine(bombAttack());
        }

        

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
