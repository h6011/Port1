using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public static BulletManager Instance;

    PlayerController playerController;

    public enum eEnemyBulletType
    {
        Rifle,
        Shotgun,
        Bomb,
    }


    [System.Serializable]
    public class EnemyPrefabVars
    {
        public GameObject RifleBullet;
        public GameObject ShotgunBullet;
        public GameObject BombBullet;
    }


    [SerializeField] EnemyPrefabVars prefabs;
    public EnemyPrefabVars Prefabs => prefabs;

    [SerializeField] Transform dynamic;










    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        playerController = PlayerController.Instance;
    }

    public void BombBulletGotBomb(Vector3 _Position)
    {
        int bulletCount = Random.Range(4, 6 + 1);

        for (int iNum = 0; iNum < bulletCount; iNum++)
        {
            float angle = (360 / bulletCount) * (iNum + 1);
            MakeEnemyBullet(_Position, angle, eEnemyBulletType.Shotgun);

        }
    }

    public GameObject MakeEnemyBullet(Vector3 _Position, float _Angle, eEnemyBulletType _BulletType)
    {
        if (_BulletType == eEnemyBulletType.Rifle)
        {
            GameObject _prefab = prefabs.RifleBullet;
            GameObject newBullet = Instantiate(_prefab, _Position, Quaternion.Euler(0, 0, _Angle), dynamic);
            return newBullet;
        }
        else if (_BulletType == eEnemyBulletType.Shotgun)
        {
            GameObject _prefab = prefabs.ShotgunBullet;
            GameObject newBullet = Instantiate(_prefab, _Position, Quaternion.Euler(0, 0, _Angle), dynamic);
            return newBullet;
        }
        else if (_BulletType == eEnemyBulletType.Bomb)
        {
            GameObject _prefab = prefabs.BombBullet;
            GameObject newBullet = Instantiate(_prefab, _Position, Quaternion.Euler(0, 0, _Angle), dynamic);
            Bullet bulletScript = newBullet.GetComponent<Bullet>();

            Vector3 targetPos = playerController.transform.position;

            float _Distance = Vector3.Distance(_Position, targetPos);

            float _Time = _Distance / bulletScript.BulletSpeed;

            bulletScript.MakeToBomb(_Time);
            return newBullet;
        }
        return null;
    }

    public void MakePlayerBullet(Vector3 _Position, float _Angle)
    {

    }


}
