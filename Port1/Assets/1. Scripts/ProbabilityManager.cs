using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class eEnemyTypeMoneySettingsParent
{
    public float percentage = 100f;

    public int randomMin = 1;
    public int randomMax = 10;
}



[System.Serializable]
public class eEnemyTypeMoneySettingsCoin : eEnemyTypeMoneySettingsParent { }

[System.Serializable]
public class eEnemyTypeMoneySettingsRuby : eEnemyTypeMoneySettingsParent { }

[System.Serializable]
public class eEnemyTypeMoneySettingsDiamond : eEnemyTypeMoneySettingsParent { }

[System.Serializable]
public class eEnemyTypeMoneySettingsEmerald : eEnemyTypeMoneySettingsParent { }




[System.Serializable]
public class eEnemyTypeMoneySettings
{
    public eEnemyType enemyType;

    [Space]
    public eEnemyTypeMoneySettingsCoin Coin;
    public eEnemyTypeMoneySettingsRuby Ruby;
    public eEnemyTypeMoneySettingsDiamond Diamond;
    public eEnemyTypeMoneySettingsEmerald Emerald;
}











public class ProbabilityManager : MonoBehaviour
{
    public static ProbabilityManager Instance;

    [SerializeField] List<eEnemyTypeMoneySettings> enemyTypeMoneySettings;


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
        float picked = Random.Range(0f, 100f);
        if (picked <= Probability)
        {
            return true;
        }
        return false;
    }

    public eEnemyTypeMoneySettings GetEnemyTypeMoneySettings(eEnemyType enemyType)
    {
        int count = enemyTypeMoneySettings.Count;
        for (int iNum = 0; iNum < count; iNum++)
        {
            eEnemyTypeMoneySettings _moneySettings = enemyTypeMoneySettings[iNum];
            if (_moneySettings.enemyType == enemyType)
            {
                return _moneySettings;
            }
        }
        return null;
    }



}
