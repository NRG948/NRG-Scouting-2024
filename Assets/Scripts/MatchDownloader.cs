using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;
using static MatchDownloader;

public class MatchDownloader : MonoBehaviour
{
    string teamCachePath;
    public TeamList teamList;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void downloadMatches()
    {
        teamCachePath = $"{Application.persistentDataPath}/cache/teams";
        Directory.CreateDirectory(teamCachePath);
        for (int i = 0; i < 20; i++)
        {
            StartCoroutine(GameObject.Find("AlertBox").GetComponent<AlertBox>().ShowNotificationBox($"Downloading teams {i*500}-{(i+1)*500-1}"));
            APIRequest(i);
        }

    }
    void APIRequest(int pageNum)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://www.thebluealliance.com/api/v3/teams/" + pageNum + "/simple");
        request.Headers.Add("X-TBA-Auth-Key", "ayLg4jZVBMJ4BFKqDzt8Sn7nGTYqDgB4VEB0ZxbMXH3MVJVnhAChBZZSyuSEuEVH");
        request.Method = "GET";

        try
        {
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string result = reader.ReadToEnd();
                    teamList = JsonUtility.FromJson<TeamList>("{\"teams\": " + result + " }");
                    File.WriteAllText(teamCachePath + "/" + (pageNum * 500) + ".json", JsonUtility.ToJson(teamList));
                    Debug.Log($"Saved to {teamCachePath + (pageNum * 500) + ".json"}");
                    //foreach (var match in matchJson.matches) { 
                    //if (match.key == eventKey + "_qm1") {
                    //        return match.alliances.blue.team_keys.ToCommaSeparatedString();
                    //    }
                    // }

                }
                else
                {

                    StartCoroutine(GameObject.Find("AlertBox").GetComponent<AlertBox>().ShowNotificationBox("Teams could not be downloaded. Please check your internet connection, or disable autofill."));
                    Debug.Log($"Error: {response.StatusCode}");

                }
            }
        }
        catch (WebException ex)
        {
            Debug.Log($"WebException: {ex.Message}");
            StartCoroutine(GameObject.Find("AlertBox").GetComponent<AlertBox>().ShowNotificationBox("Teams could not be downloaded. Please check your internet connection, or disable autofill."));

        }
    }

    [System.Serializable]
    public class APITeam
    {
        public int team_number;
        public string nickname;
    }

    public class TeamList
    {
        public APITeam[] teams;
    }
}
