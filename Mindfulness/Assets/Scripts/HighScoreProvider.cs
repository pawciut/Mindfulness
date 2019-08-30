using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreProvider : MonoBehaviour
{
    [SerializeField()]
    GameObject NameInput;

    [SerializeField()]
    TextMeshProUGUI HighscoreText;



    string Name;
    int Score;
    int FinishedTime;

    [SerializeField]
    string BASE_URL = "https://docs.google.com/forms/d/e/1FAIpQLSeLZ0rJ1lvQNEmYovWQVuE2LoLsAbh92pypArORmVxDgU-c_w/formResponse";
    string SPREADSHEET_URL = "https://docs.google.com/spreadsheets/d/1yQedVqtgtSvVVOSTZjMaFVUxU4zRaXVWIGWf4Adfn6E/export?format=csv";

    IEnumerator Post(string name, int score, int finishedTime)
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.483554565", name);
        form.AddField("entry.922092537", score);
        form.AddField("entry.480887816", finishedTime);
        byte[] rawData = form.data;
        WWW www = new WWW(BASE_URL, rawData);
        yield return www;
        StartCoroutine(GetScores());
    }

    public void Send()
    {
        Name = NameInput.GetComponent<TMP_InputField>().text.Replace(",","");//zeby csv potem nie zwalilo
        Score = PlayerPrefs.GetInt(Constants.Player_Pref_Score, 0);
        FinishedTime = PlayerPrefs.GetInt(Constants.Player_Pref_Time, 0);

        StartCoroutine(Post(Name, Score, FinishedTime));
    }


    public void GetData()
    {
        StartCoroutine(GetScores());
    }

    // Start can be used as a coroutine
    IEnumerator GetScores()
    { 
        HighscoreText.text = String.Empty;
        WWWForm form = new WWWForm();
        WWW download = new WWW(SPREADSHEET_URL, form);

        yield return download;

        if (!string.IsNullOrEmpty(download.error))
        {
            Debug.Log("Error downloading: " + download.error);
        }
        else
        {
            var csvData = download.text;            
            string[] rows = csvData.Split("\n"[0]);

            //przetwarzanie
            var rows2 = rows.Skip(1);
            List<HighscoreEntry> allHighscores = new List<HighscoreEntry>();
            foreach(var r in rows2)
            {
                var rfields = r.Split(',');
                allHighscores.Add(new HighscoreEntry(rfields[0], rfields[1], int.Parse(rfields[2]), int.Parse(rfields[3])));
                yield return 0;
            }

            var top10 = allHighscores.OrderBy(h => h.time).OrderByDescending(h => h.score).Take(10);

            int place = 1;
            foreach (var h in top10)
            {
                HighscoreText.text+= $"{place}.\t{h.timestamp.Substring(0,10)}{Constants.Highscore_Col_Spacing}\t{h.name.PadRight(Constants.Name_Char_Limit)}\t{Constants.Highscore_Col_Spacing}\t{h.time}{Constants.Highscore_Col_Spacing}\t{h.score.ToString().PadLeft(Constants.Score_Char_Limit)}\n";
                ++place;
            }
        }


    }

    public struct HighscoreEntry
    {
        public string timestamp;
        public string name;
        public int score;
        public int time;

        public HighscoreEntry(string timestamp, string name, int score, int time)
        {
            this.timestamp = timestamp;
            this.name = name;
            this.score = score;
            this.time = time;
        }
    }

}
