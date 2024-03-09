using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System.Net;
using static APITest;
using Unity.VisualScripting;
using UnityEngine.UI;
public class SaveSystem : MonoBehaviour
{
    public TMP_InputField inputField;
    public TMP_InputField inputFieldID;
    public Toggle autoFill;
    public Toggle flipField;
    public Toggle hapticFeedback;
    public Toggle militaryTime;
    public APIMatchFile matchJson;


    void Start()
    {
        if (!PlayerPrefs.HasKey("Name")) { PlayerPrefs.SetString("Name", "Anonymous"); }
        if (!PlayerPrefs.HasKey("EventKey")) { PlayerPrefs.SetString("EventKey", "2002nrg"); }
        inputField.text = PlayerPrefs.GetString("Name");
        inputFieldID.text = PlayerPrefs.GetString("EventKey");
        autoFill.isOn = PlayerPrefs.GetInt("Autofill",0) == 1;
        flipField.isOn = PlayerPrefs.GetInt("FlipField",0) == 1;
        hapticFeedback.isOn = PlayerPrefs.GetInt("Haptic",0) == 1;
        militaryTime.isOn = PlayerPrefs.GetInt("MilitaryTime",0) == 1;

        

    }

    public void SaveData()
    {
        PlayerPrefs.SetString("Name", inputField.text);
        PlayerPrefs.SetString("EventKey", inputFieldID.text == "" ? "2002nrg" : inputFieldID.text);
        PlayerPrefs.SetInt("Autofill", autoFill.isOn ? 1 : 0);
        PlayerPrefs.SetInt("FlipField", flipField.isOn ? 1 : 0);
        PlayerPrefs.SetInt("Haptic", hapticFeedback.isOn ? 1 : 0);
        PlayerPrefs.SetInt("MilitaryTime", militaryTime.isOn ? 1 : 0);

        if (autoFill.isOn) { ApiRequest(inputFieldID.text); }
        // Extra stuff
        //fileJson = JsonUtility.FromJson<APIMatchFile>(rawJson);
    }


    public void DeletData()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("Haptic", 0);
    }


    string ApiRequest(string eventKey)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://www.thebluealliance.com/api/v3/event/" + eventKey + "/matches/simple");
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
                    matchJson = JsonUtility.FromJson<APIMatchFile>("{\"matches\": " + result + " }");
                    string filePath = Application.persistentDataPath + "/cache";
                    if (!Directory.Exists(filePath)) { Directory.CreateDirectory(filePath); }
                    filePath = filePath + $"/{eventKey}.json";
                    File.WriteAllText(filePath, JsonUtility.ToJson(matchJson));
                    Debug.Log($"Saved to {filePath}");
                    //foreach (var match in matchJson.matches) { 
                    //if (match.key == eventKey + "_qm1") {
                    //        return match.alliances.blue.team_keys.ToCommaSeparatedString();
                    //    }
                   // }
                    return "";
                }
                else
                {
                    
                    StartCoroutine(GameObject.Find("AlertBox").GetComponent<AlertBox>().ShowBoxNoResponse("Matches could not be downloaded. Please check your internet connection, or disable autofill."));
                    Debug.Log($"Error: {response.StatusCode}");
                    return $"Error: {response.StatusCode}";
                }
            }
        }
        catch (WebException ex)
        {
            Debug.Log($"WebException: {ex.Message}");
            StartCoroutine(GameObject.Find("AlertBox").GetComponent<AlertBox>().ShowBoxNoResponse("Matches could not be downloaded. Please check your internet connection, or disable autofill."));
            return $"WebException: {ex.Message}";
        }
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
