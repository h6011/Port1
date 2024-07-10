using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    [Header("Background Transforms")]

    [SerializeField] Transform topTrs;
    [SerializeField] Transform middleTrs;
    [SerializeField] Transform bottomTrs;

    [Header("Speed of Backgrounds")]

    [SerializeField] float topSpeed = 1f;
    [SerializeField] float middleSpeed = 1f;
    [SerializeField] float bottomSpeed = 1f;

    Material topMaterial;
    Material middleMaterial;
    Material bottomMaterial;


    private void Start()
    {
        SpriteRenderer topSprite = topTrs.GetComponent<SpriteRenderer>();
        SpriteRenderer middleSprite = middleTrs.GetComponent<SpriteRenderer>();
        SpriteRenderer bottomSprite = bottomTrs.GetComponent<SpriteRenderer>();

        topMaterial = topSprite.material;
        middleMaterial = middleSprite.material;
        bottomMaterial = bottomSprite.material;
    }

    private void Update()
    {
        backGroundMoveAction();
    }

    private void backGroundMoveAction()
    {
        Vector2 vecTop = topMaterial.mainTextureOffset;
        Vector2 vecMid = middleMaterial.mainTextureOffset;
        Vector2 vecBot = bottomMaterial.mainTextureOffset;

        vecTop += new Vector2(0, topSpeed * Time.deltaTime);
        vecMid += new Vector2(0, middleSpeed * Time.deltaTime);
        vecBot += new Vector2(0, bottomSpeed * Time.deltaTime);

        vecTop.y = Mathf.Repeat(vecTop.y, 1.0f);
        vecMid.y = Mathf.Repeat(vecMid.y, 1.0f);
        vecBot.y = Mathf.Repeat(vecBot.y, 1.0f);

        Debug.Log(topMaterial);

        topMaterial.mainTextureOffset = vecTop;
        middleMaterial.mainTextureOffset = vecMid;
        bottomMaterial.mainTextureOffset = vecBot;
    }


}
