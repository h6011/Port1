using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class RankingManager : MonoBehaviour
{

    [SerializeField] readonly int RankCount = 50;

    private string fileName = "__Ranking";
    private string path = "";

    public static RankingManager Instance;


    public class RankInfo
    {
        public string name = "someone";
        public int score = -1;
        public int meter = -1;
    }



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
        
        path = Application.persistentDataPath + $"/{fileName}";
    }

    /// <summary>
    /// 제대로된 랭크 기록인지 확인
    /// </summary>
    private List<RankInfo> checkisFixedRank(List<RankInfo> ranks)
    {
        int count = ranks.Count;
        if (count > RankCount)
        {
            ranks.RemoveRange(RankCount, count);
        }
        for (int iNum = 0; iNum < count; iNum++)
        {
             
        }
        return ranks;
    }

    public List<RankInfo> GetRanking()
    {
        //JsonConvert.DeserializeObject()
        List<RankInfo> ranks = null;

        string fileText = getRankFileText();

        if (fileText == string.Empty)
        {
            ranks = new List<RankInfo>(RankCount);
        }
        else
        {
            ranks = JsonConvert.DeserializeObject<List<RankInfo>>(fileText);
        }


        ranks = checkisFixedRank(ranks);

        return ranks;

    }

    public void AddDataToRank(RankInfo _rankInfo)
    {
        List<RankInfo> currentRanks = GetRanking();
        int count = currentRanks.Count;
        // 생각 해보니 Ranking score 나 meter 둘중으로 하나 해야됨

    }


    public void SetRanking(List<RankInfo> ranks)
    {
        string jsoned = JsonConvert.SerializeObject(ranks);
        setRankFileText(jsoned);
    }


    private string getRankFileText()
    {
        bool isExist = File.Exists(path);
        if (isExist)
        {
            return File.ReadAllText(path);
        }
        else
        {
            return string.Empty;
        }
    }

    private void setRankFileText(string text)
    {
        File.WriteAllText(path, text);
    }



}
