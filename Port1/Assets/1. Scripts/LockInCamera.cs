using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LockInCamera : MonoBehaviour
{
    public static LockInCamera Instance;
    Vector3 playerSize;
    BoxCollider2D boxColl;

    Vector2 min;
    Vector2 max;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            initMapBound();
        }
        else
        { 
            Destroy(gameObject);
        }
    }

    private void initMapBound()
    {
        boxColl = GetComponent<BoxCollider2D>();
    }

    public void SetPlayer(Vector2 _playerColliderSize)
    {
        playerSize = _playerColliderSize * 0.5f;

        min = new Vector2(boxColl.bounds.min.x + playerSize.x, boxColl.bounds.min.y + playerSize.y);
        max = new Vector2(boxColl.bounds.max.x - playerSize.x, boxColl.bounds.max.y - playerSize.y);
    }

    public void CheckPosition(Transform TargetTransform)
    {
        

        Vector3 playerPos = TargetTransform.position;

        TargetTransform.position = new Vector3(
            Mathf.Clamp(playerPos.x, min.x, max.x),
            Mathf.Clamp(playerPos.y, min.y, max.y),
            playerPos.z);
    }


    #region memo1
    //Vector3 boundMin = bounds.min;
    //Vector3 boundMax = bounds.max;

    //Vector3 FixedPositionMin = mainCamera.WorldToViewportPoint(TargetTransform.position + boundMin);
    //Vector3 FixedPositionMax = mainCamera.WorldToViewportPoint(TargetTransform.position + boundMax);

    //DebugTitle.Instance.SetTitleTitle1(FixedPositionMin.ToString());
    //DebugTitle.Instance.SetTitleTitle2(FixedPositionMax.ToString());
    //DebugTitle.Instance.SetTitleTitle3(boundMin.ToString()+" / "+boundMax.ToString());

    //if (FixedPositionMin.x < 0)
    //{
    //    FixedPositionMin.x = 0;
    //    Vector3 _fixed = mainCamera.ViewportToWorldPoint(FixedPositionMin);
    //    TargetTransform.position = new Vector3(boundMax.x + _fixed.x, TargetTransform.position.y, TargetTransform.position.z);
    //}

    //if (FixedPositionMax.x > 1)
    //{
    //    FixedPositionMax.x = 1;
    //    Vector3 _fixed = mainCamera.ViewportToWorldPoint(FixedPositionMax);
    //    TargetTransform.position = new Vector3(boundMin.x + _fixed.x, TargetTransform.position.y, TargetTransform.position.z);
    //}

    //if (FixedPositionMin.y < 0)
    //{
    //    FixedPositionMin.y = 0;
    //    Vector3 _fixed = mainCamera.ViewportToWorldPoint(FixedPositionMin);
    //    TargetTransform.position = new Vector3(TargetTransform.position.x, boundMax.y + _fixed.y, TargetTransform.position.z);
    //}

    //if (FixedPositionMax.y > 1)
    //{
    //    FixedPositionMax.y = 1;
    //    Vector3 _fixed = mainCamera.ViewportToWorldPoint(FixedPositionMax);
    //    TargetTransform.position = new Vector3(TargetTransform.position.x, boundMin.y + _fixed.y, TargetTransform.position.z);
    //}

    #endregion

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
