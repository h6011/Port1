using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LockInCamera : MonoBehaviour
{
    public static LockInCamera Instance;
    private Camera mainCamera;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        mainCamera = Camera.main;
    }

    public void CheckPosition(Transform TargetTransform, Bounds bounds)
    {
        Vector3 boundMin = bounds.min;
        Vector3 boundMax = bounds.max;

        Vector3 FixedPositionMin = mainCamera.WorldToViewportPoint(TargetTransform.position + boundMin);
        Vector3 FixedPositionMax = mainCamera.WorldToViewportPoint(TargetTransform.position + boundMax);

        if (FixedPositionMin.x < 0)
        {
            FixedPositionMin.x = 0;
            Vector3 _fixed = mainCamera.ViewportToWorldPoint(FixedPositionMin);
            TargetTransform.position = new Vector3(boundMax.x + _fixed.x, TargetTransform.position.y, TargetTransform.position.z);
        }

        if (FixedPositionMax.x > 1)
        {
            FixedPositionMax.x = 1;
            Vector3 _fixed = mainCamera.ViewportToWorldPoint(FixedPositionMax);
            TargetTransform.position = new Vector3(boundMin.x + _fixed.x, TargetTransform.position.y, TargetTransform.position.z);
        }



        if (FixedPositionMin.y < 0)
        {
            FixedPositionMin.y = 0;
            Vector3 _fixed = mainCamera.ViewportToWorldPoint(FixedPositionMin);
            TargetTransform.position = new Vector3(TargetTransform.position.x, boundMax.y + _fixed.y, TargetTransform.position.z);
        }

        if (FixedPositionMax.y > 1)
        {
            FixedPositionMax.y = 1;
            Vector3 _fixed = mainCamera.ViewportToWorldPoint(FixedPositionMax);
            TargetTransform.position = new Vector3(TargetTransform.position.x, boundMin.y + _fixed.y, TargetTransform.position.z);
        }


    }


    //public void CheckPosition(Transform TargetTransform, Collider collider)
    //{
    //    Bounds bounds = collider.bounds;

    //    Vector3 boundMin = bounds.min;
    //    Vector3 boundMax = bounds.max;

    //    Vector3 FixedPositionMin = mainCamera.WorldToViewportPoint(TargetTransform.position + boundMin);
    //    Vector3 FixedPositionMax = mainCamera.WorldToViewportPoint(TargetTransform.position + boundMax);

    //    if (FixedPositionMin.x < 0)
    //    {
    //        FixedPositionMin.x = 0;
    //        Vector3 _fixed = mainCamera.ViewportToWorldPoint(FixedPositionMin);
    //        TargetTransform.position = new Vector3(boundMax.x + _fixed.x, TargetTransform.position.y, TargetTransform.position.z);
    //    }

    //    if (FixedPositionMax.x > 1)
    //    {
    //        FixedPositionMax.x = 1;
    //        Vector3 _fixed = mainCamera.ViewportToWorldPoint(FixedPositionMax);
    //        TargetTransform.position = new Vector3(boundMin.x + _fixed.x, TargetTransform.position.y, TargetTransform.position.z);
    //    }



    //    if (FixedPositionMin.y < 0)
    //    {
    //        FixedPositionMin.y = 0;
    //        Vector3 _fixed = mainCamera.ViewportToWorldPoint(FixedPositionMin);
    //        TargetTransform.position = new Vector3(TargetTransform.position.x, boundMax.y + _fixed.y, TargetTransform.position.z);
    //    }

    //    if (FixedPositionMax.y > 1)
    //    {
    //        FixedPositionMax.y = 1;
    //        Vector3 _fixed = mainCamera.ViewportToWorldPoint(FixedPositionMax);
    //        TargetTransform.position = new Vector3(TargetTransform.position.x, boundMin.y + _fixed.y, TargetTransform.position.z);
    //    }


    //}



}
