using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProbabilityManager : MonoBehaviour
{
    ProbabilityManager Instance;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool isSuccess(float Probability)
    {
        Probability = Mathf.Clamp(Probability, 0, 100);
        float picked = Random.Range(0f, Probability);
        if (picked <= Probability)
        {
            return true;
        }
        return false;
    }



}
