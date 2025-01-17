using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eEffectType
{
    Explosion1,
}

public class EffectManager : MonoBehaviour
{
    public static EffectManager Instance;

    [Header("Prefab")]
    [SerializeField] GameObject explosion1;

    [Header("Vars")]
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


    public void CreateEffect(eEffectType effectType, Vector3 position, float _size)
    {
        if (effectType == eEffectType.Explosion1)
        {
            GameObject newEffect = Instantiate(explosion1, position, Quaternion.identity, dynamic);
            SpriteRenderer sprite = newEffect.GetComponent<SpriteRenderer>();

            float _basicSize = sprite.sprite.rect.width;
            float ratio = _size / _basicSize;

            newEffect.transform.localScale = new Vector3(ratio, ratio);
        }
    }





}
