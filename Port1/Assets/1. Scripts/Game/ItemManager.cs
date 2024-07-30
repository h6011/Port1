using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{

    public enum eItemType
    {
        UpgradeBullet,
        Shield,
        Magnet,
    }

    public static ItemManager Instance;

    ProbabilityManager probabilityManage;


    [SerializeField] Transform dynamic;

    [Header("Prefab")]
    [SerializeField] GameObject upgradeBullet;
    [SerializeField] GameObject shield;
    [SerializeField] GameObject magnet;


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

    public void TryDropItem(eItemType itemType, Vector3 Position)
    {

    }


    public void DropItem(eItemType itemType, Vector3 Position)
    {
        if (itemType == eItemType.UpgradeBullet)
        {

        }
        else if (itemType == eItemType.Shield)
        {

        }
        else if (itemType == eItemType.Magnet)
        {

        }
    }






}
