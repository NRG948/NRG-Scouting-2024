using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public class APITest : MonoBehaviour
{
    public APIMatchFile fileJson;
    void Start()
    {
        string rawJson = ApiRequest("https://www.thebluealliance.com/api/v3/event/2023gal/matches/simple");
        fileJson = JsonUtility.FromJson<APIMatchFile>(rawJson);
        string filePath = Application.persistentDataPath;
        
    }
    // Start is called before the first frame update
    string ApiRequest(string url)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
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
                    return "{\"matches\": " + result + " }";
                }
                else
                {
                    return $"Error: {response.StatusCode}";
                }
            }
        }
        catch (WebException ex)
        {
            return $"WebException: {ex.Message}";
        }
    }


    
    // Update is called once per frame
    void Update()
    {
        
    }

    [System.Serializable]
    public class APIMatchFile
    {
        public APIMatch[] matches;
    }

    [System.Serializable]
    public class APIMatch
    {
        public Alliances alliances;
        public string key;
    }
    [System.Serializable]
    public class Alliances
    {
        public Blue blue;
        public Red red;
    }
    [System.Serializable]
    public class Blue
    {
        public string[] team_keys;
    }

    [System.Serializable]
    public class Red
    {
        public string[] team_keys;
    }
}
