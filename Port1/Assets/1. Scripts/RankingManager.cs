using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class RankingManager : MonoBehaviour
{

    [SerializeField] readonly int RankCount = 20;

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
        Debug.Log(path);
    }

    /// <summary>
    /// ����ε� ��ũ ������� Ȯ��
    /// </summary>
    private List<RankInfo> checkisFixedRank(List<RankInfo> ranks)
    {
        //int count = ranks.Count;
        //if (count > RankCount)
        //{
        //    ranks.RemoveRange(RankCount, count);
        //}
        //for (int iNum = 0; iNum < count; iNum++)
        //{
             
        //}
        return ranks;
    }

    public List<RankInfo> GetRanking()
    {
        //JsonConvert.DeserializeObject()
        List<RankInfo> ranks = null;

        string fileText = getRankFileText();

        Debug.Log("fileText: " + fileText);

        if (fileText == string.Empty)
        {
            ranks = new List<RankInfo>();

            for (int i = 0; i < RankCount; i++)
            {
                ranks.Add(new RankInfo());
            }

            SetRanking(ranks);
            print(1);
        }
        else
        {
            ranks = JsonConvert.DeserializeObject<List<RankInfo>>(fileText);
            print(2);
        }


        ranks = checkisFixedRank(ranks);

        return ranks;

    }

    public List<RankInfo> getNewRankInfos()
    {
        return new List<RankInfo>(RankCount);
    }

    public void SaveDataToRanking(string _name, int _score, int _meter)
    {
        RankInfo _rankInfo = new RankInfo();
        _rankInfo.name = _name;
        _rankInfo.score = _score;
        _rankInfo.meter = _meter;

        AddDataToRank(_rankInfo);
    }

    public void AddDataToRank(RankInfo _rankInfo)
    {
        List<RankInfo> currentRanks = GetRanking();
        int count = currentRanks.Count;

        Debug.Log(count);

        Debug.LogWarning(currentRanks[count - 1].score);
        Debug.LogWarning(_rankInfo.score);

        if (_rankInfo.score > currentRanks[count - 1].score) // ��� �ɼ� �ִ���
        {
            for (int i = count-1; i >= 0; i--) // 1�� ���� õõ�� Ȯ��
            {
                if (_rankInfo.score > currentRanks[i].score) // ��ũ�� �Ҽ� �ִ���
                {
                    Debug.Log("THIS : "+i);
                    List<RankInfo> saveWillPullInfos = null;//= new List<RankInfo>(count - 1);
                    // �׋� ���� ���� ������ ���
                    for (int j = i; j < count-1; j++)
                    {
                        saveWillPullInfos.Add(currentRanks[i]);
                    }

                    // �׶� ���� ���� ������ ���� (���� Ȯ��)
                    currentRanks.RemoveRange(i, count - 1);

                    saveWillPullInfos.RemoveAt(saveWillPullInfos.Count - 1);

                    // ��ũ��
                    currentRanks.Add(_rankInfo);



                    // ���� ��ĭ �ڷ� �ѱ�� �״�� �ٽ� ����

                    int saveWillPullInfosCount = saveWillPullInfos.Count;
                    for (int k = 0; k < saveWillPullInfosCount; k++)
                    {
                        currentRanks.Add(saveWillPullInfos[k]);
                    }

                    SetRanking(currentRanks);

                    print(1);

                    return;
                }
            }
        }
        else
        {
            Debug.Log("Couldnt rank in");
        }





        // ���� �غ��� Ranking score �� meter �������� �ϳ� �ؾߵ�
        // score �켱���� �ҿ���




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
