using General;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class ScoreManager : SingletonMonoBehaviour<ScoreManager>
{
    public float score = 0f;

    private TextMeshProUGUI scoreUI;
    private MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

    private bool pauseScore = false;

    // Start is called before the first frame update
    void Start()
    {
        scoreUI = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        Debug.LogWarning(scoreUI);
    }

    // Update is called once per frame
    void Update()
    {
        if(pauseScore)
        {
            return;
        }

        score += Time.deltaTime * 50;
        // Debug.Log(score);
        scoreUI.SetText(((int)score).ToString());
    }
    
    private string leaderboardHost = "http://vm.mxbi.net:5000/";
    

    public IEnumerator submitScore(string name)
    {
        string scoreStr = ((int)score).ToString();
        // string hash = md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(name + scoreStr + "xds"));

        string url = leaderboardHost + "/submit/" + scoreStr + "/" + name;// + "/" + hash;
        UnityWebRequest request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();
    }
    
    public string getLeaderboard() {
        string url = leaderboardHost + "/top10";

        UnityWebRequest request = UnityWebRequest.Get(url);
        request.SendWebRequest();

        while (!request.isDone)
        {
        }

        ;

        string res = request.downloadHandler.text;

        return res;
        // Dictionary<string, int> top10 = new Dictionary<string, int>();
        // List<string[]> top10 = new List<string[]>();

        // foreach (string line in res.Split('\n')) {
        //     if (line.Length > 0) {
        //         string[] user = line.Split(':');
        //         top10.Add(user);
        //     }
        // } 
        //
        // string names = "";
        // string scores = "";
        // string whens = "";
        // foreach (string[] tuple in top10) {
        //     names += tuple[0] + "\n";
        //     scores += tuple[1] + "\n";
        //     whens += tuple[2] + "\n";
        // }
        // namesObj.text = names;
        // scoresObj.text = scores;
        // datesObj.text = whens;
    }

    public void pauseScoreCount()
    {
        pauseScore = true;
    }

    public void startUpCount()
    {
        pauseScore = false;
    }
}