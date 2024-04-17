using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public static class UnixConverter
{
    public static DateTime UnixTimeStampToDateTime( string unixTimeStamp )
    {
        // Unix timestamp is seconds past epoch
        DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        dateTime = dateTime.AddSeconds( Convert.ToDouble(unixTimeStamp) ).ToLocalTime();
        return dateTime;
    }
}

public class LocalDataViewer : MonoBehaviour
{
    private string filePath;
    private string objPath;
    private string subjPath;
    private string pitPath;
    public GameObject objPrefab;
    public GameObject objSpawner;
    public GameObject subjPrefab;
    public GameObject subjSpawner;
    public GameObject pitPrefab;
    public GameObject pitSpawner;
    public GameObject noMatch;
    public string deletionFilepath;
    // Start is called before the first frame update
    public void Start()
    {
        filePath = Application.persistentDataPath;
        if (!PlayerPrefs.HasKey("EventKey")) { PlayerPrefs.SetString("EventKey", "2002nrg"); }
        objPath = filePath + $"/{PlayerPrefs.GetString("EventKey")}/obj";
        subjPath = filePath + $"/{PlayerPrefs.GetString("EventKey")}/subj";
        pitPath = filePath + $"/{PlayerPrefs.GetString("EventKey")}/pit";
        foreach (var i in new string[] {objPath, subjPath,pitPath})
        {
            if (!Directory.Exists(i))
            {
                Directory.CreateDirectory(i);
            }
        }
        

        // Objective Reload
        for (int i = 0; i < objSpawner.transform.childCount; i++) {
            Destroy(objSpawner.transform.GetChild(i).gameObject);
        }
        // Objective Spawn
        if (Directory.GetFiles(objPath).Length > 0)
        {
            foreach (var match in Directory.GetFiles(objPath))
            {
                DataManager.Match objFileJson = JsonUtility.FromJson<DataManager.Match>(File.ReadAllText(match));
                GameObject newObjPrefab = objPrefab;
                newObjPrefab.transform.GetChild(0).GetChild(0).transform.GetComponent<RawImage>().color = (objFileJson.AllianceColor == "Red" ? new Color(0.8f,0f,0f) : new Color(0f,0.8f, 0.8f));
                newObjPrefab.transform.GetChild(0).GetChild(1).transform.GetComponent<TMP_Text>().text = objFileJson.DriverStation.ToString();
                newObjPrefab.transform.GetChild(2).GetComponent<TMP_Text>().text = objFileJson.TeamNumber.ToString();
                newObjPrefab.transform.GetChild(3).GetChild(0).transform.GetComponent<TMP_Text>().text = objFileJson.MatchType;
                newObjPrefab.transform.GetChild(4).GetComponent<TMP_Text>().text = objFileJson.MatchNumber.ToString();
                newObjPrefab.transform.GetChild(6).GetComponent<TMP_Text>().text = objFileJson.ScouterName;
                newObjPrefab.transform.GetChild(8).GetComponent<TMP_Text>().text = objFileJson.Comments == "" ? "" : objFileJson.Comments;
                newObjPrefab.transform.GetChild(9).GetComponent<LDV_Buttons>().filePath = match;
                newObjPrefab.transform.GetChild(10).GetComponent<LDV_Buttons>().filePath = match;
                newObjPrefab.transform.GetChild(11).GetComponent<TMP_Text>().text = UnixConverter.UnixTimeStampToDateTime(match.Split("_")[3].TrimEnd(".json")).ToString(Convert.ToBoolean(PlayerPrefs.GetInt("MilitaryTime",0)) ? "HH:mm:ss" : "hh:mm:ss tt");
                Instantiate(newObjPrefab, objSpawner.transform);
            }
        } else
        {
            noMatch.GetComponent<TMP_Text>().text = $"No matches found :(\n\nShowing matches for {PlayerPrefs.GetString("EventKey")} (are you sure this is the correct event?)";
            Instantiate(noMatch, objSpawner.transform);
        }
        // Subjective Reload
        for (int i = 0; i < subjSpawner.transform.childCount; i++)
        {
            Destroy(subjSpawner.transform.GetChild(i).gameObject);
        }
        if (Directory.GetFiles(subjPath).Length > 0)
        {
            // Subjective Spawn
            foreach (var match in Directory.GetFiles(subjPath))
            {
                DataManager.SubjectiveMatch subjFileJson = JsonUtility.FromJson<DataManager.SubjectiveMatch>(File.ReadAllText(match));
                GameObject newSubjPrefab = subjPrefab;
                newSubjPrefab.transform.GetChild(0).GetChild(0).transform.GetComponent<RawImage>().color = (subjFileJson.AllianceColor == "Red" ? new Color(0.8f, 0f, 0f) : new Color(0f, 0.8f, 0.8f));
                newSubjPrefab.transform.GetChild(0).GetChild(1).transform.GetComponent<TMP_Text>().text = subjFileJson.DriverStation.ToString();
                newSubjPrefab.transform.GetChild(2).GetComponent<TMP_Text>().text = subjFileJson.TeamNumber.ToString();
                newSubjPrefab.transform.GetChild(3).GetChild(0).transform.GetComponent<TMP_Text>().text = subjFileJson.MatchType;
                newSubjPrefab.transform.GetChild(4).GetComponent<TMP_Text>().text = subjFileJson.MatchNumber.ToString();
                newSubjPrefab.transform.GetChild(6).GetComponent<TMP_Text>().text = subjFileJson.ScouterName;
                newSubjPrefab.transform.GetChild(8).GetComponent<TMP_Text>().text = subjFileJson.Comments == "" ? "" : subjFileJson.Comments;
                newSubjPrefab.transform.GetChild(9).GetComponent<LDV_Buttons>().filePath = match;
                newSubjPrefab.transform.GetChild(10).GetComponent<LDV_Buttons>().filePath = match;
                newSubjPrefab.transform.GetChild(11).GetComponent<TMP_Text>().text = UnixConverter.UnixTimeStampToDateTime(match.Split("_")[3].TrimEnd(".json")).ToString(Convert.ToBoolean(PlayerPrefs.GetInt("MilitaryTime", 0)) ? "HH:mm:ss" : "hh:mm:ss tt");
                Instantiate(newSubjPrefab, subjSpawner.transform);
            }
        } else
        {
            noMatch.GetComponent<TMP_Text>().text = $"No matches found :(\n\nShowing matches for {PlayerPrefs.GetString("EventKey")} (are you sure this is the correct event?)";
            Instantiate(noMatch, subjSpawner.transform);
        }
        // Pits Reload
        for (int i = 0; i < pitSpawner.transform.childCount; i++)
        {
            Destroy(pitSpawner.transform.GetChild(i).gameObject);
        }
        if (Directory.GetFiles(pitPath).Length > 0)
        {
            // Subjective Spawn
            foreach (var match in Directory.GetFiles(pitPath))
            {
                DataManager.Pit pitFileJson = JsonUtility.FromJson<DataManager. Pit>(File.ReadAllText(match));
                GameObject newPitPrefab = pitPrefab;
                newPitPrefab.transform.GetChild(1).GetComponent<TMP_Text>().text = $"{pitFileJson.TeamNumber.ToString()} - {pitFileJson.TeamName}";
                newPitPrefab.transform.GetChild(3).GetComponent<TMP_Text>().text = "Interviewer: " + pitFileJson.Interviewer;
                newPitPrefab.transform.GetChild(5).GetComponent<TMP_Text>().text = "Interviewee: " + pitFileJson.Interviewee;
                newPitPrefab.transform.GetChild(6).GetComponent<LDV_Buttons>().filePath = match;
                newPitPrefab.transform.GetChild(7).GetComponent<LDV_Buttons>().filePath = match;
                newPitPrefab.transform.GetChild(8).GetComponent<TMP_Text>().text = UnixConverter.UnixTimeStampToDateTime(match.Split("_")[2].TrimEnd(".json")).ToString(Convert.ToBoolean(PlayerPrefs.GetInt("MilitaryTime",0)) ? "HH:mm:ss" : "hh:mm:ss tt");

                Instantiate(newPitPrefab, pitSpawner.transform);
            }
        }
        else
        {
            noMatch.GetComponent<TMP_Text>().text = $"No matches found :(\n\nShowing matches for {PlayerPrefs.GetString("EventKey")} (are you sure this is the correct event?)";
            Instantiate(noMatch, pitSpawner.transform);
        }

    }
    
    // Update is called once per frame
    void Update()
    {
        
    }


    public void edit(string filePath) {
        Debug.Log("Editing " + filePath); 
    }
    

    public void delete(string filePath) {
        deletionFilepath = filePath;
        GameObject.Find("AlertBox").GetComponent<AlertBox>().ShowBoxByMessageKey("Are you sure you want to delete this match? It will be lost forever (A long time)!|ldvDelete");
    }
    public void confirmDelete()
    {
        Debug.Log("Deleting " + deletionFilepath);
        File.Delete(deletionFilepath);
        Start();
    }

    public void deleteFullEvent()
    {
        Directory.Delete(filePath + "/" + PlayerPrefs.GetString("EventKey"),true);
        //File.Delete(filePath + $"/cache/{PlayerPrefs.GetString("EventKey")}.json"); Why the actual FLUFF would i do this
        SceneManager.LoadScene(0);
    }


}
