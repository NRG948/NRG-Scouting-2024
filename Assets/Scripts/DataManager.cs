using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    public Match match;
    public AllianceMatch allianceMatch;
    
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "ObjectiveScout") { match = new Match(); }
        if (SceneManager.GetActiveScene().name == "SubjectiveScout") { allianceMatch = new AllianceMatch(); }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetBool (string key, bool value,bool subj=false)
    {

        if (!subj) { match.GetType().GetField(key).SetValue(match, value); } else
        {
            allianceMatch.GetType().GetField (key).SetValue(allianceMatch, value);
        }
    }
    public void SetString(string key, string value,bool subj=false)
    {
        if (!subj) { match.GetType().GetField(key).SetValue(match, value); }
        else
        {
            allianceMatch.GetType().GetField(key).SetValue(allianceMatch, value);
        }
    }
    public void SetInt(string key, int value, bool subj = false)
    {
        if (!subj) { match.GetType().GetField(key).SetValue(match, value); }
        else
        {
            allianceMatch.GetType().GetField(key).SetValue(allianceMatch, value);
        }
    }

    public void SaveRobotScout()
    {
      string objectivePath = $"{Application.persistentDataPath}/{PlayerPrefs.GetString("EventKey")}/obj/";
      if (!(Directory.Exists(objectivePath)))
        {
            Directory.CreateDirectory(objectivePath);
        }
      string currentTime = (DateTime.UtcNow - new DateTime(1970,1,1)).TotalSeconds.ToString().Truncate(10,"");
      string fileName = $"{match.TeamNumber}_{match.MatchType}_{match.MatchNumber}_{currentTime}.json";
      string jsonData = JsonUtility.ToJson(match,true);
      File.WriteAllText(objectivePath + fileName, jsonData);
      StartCoroutine(GameObject.Find("AlertBox").GetComponent<AlertBox>().ShowBoxNoResponse("Successfully Saved Data"));
    }
    public void SaveAllianceScout()
    {
        string objectivePath = $"{Application.persistentDataPath}/{PlayerPrefs.GetString("EventKey")}/subj/";
        if (!(Directory.Exists(objectivePath)))
        {
            Directory.CreateDirectory(objectivePath);
        }
        string currentTime = (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds.ToString().Truncate(10,"");
        string fileName = $"{allianceMatch.MatchType}_{allianceMatch.MatchNumber}_{allianceMatch.AllianceColor}_{currentTime}.json";
        string jsonData = JsonUtility.ToJson(allianceMatch, true);
        File.WriteAllText(objectivePath + fileName, jsonData);
    }
    [System.Serializable]
    public class Match
    {
        public int TeamNumber;
        public string MatchType;
        public int DataQuality;
        public int MatchNumber;
        public bool Replay;
        public string AllianceColor;
        public int DriverStation;
        public string ScouterName;
        public bool Preload;
        public bool LeftWing;
        public int AutoSpeaker;
        public int AutoAmp;
        public int AutoPickUpWing;
        public int AutoPickUpCenter;
        public bool AStop;
        public int PickUpGround;
        public int PickUpSource;
        public int SpeakerNotesUnamped;
        public int SpeakerNotesAmped;
        public int AmpNotes;
        public bool Feeder;
        public bool Coopertition;
        public bool Onstage;
        public bool Park;
        public bool Spotlight;
        public bool Trap;
        public string Comments;
    }
    [System.Serializable]
    public class AllianceMatch
    {
        public int MatchNumber;
        public string MatchType;
        public int DataQuality;
        public bool Replay;
        public string AllianceColor;
        public int DriverStation;
        public string ScouterName;
        public int Team1; // Anything after with the suffix "1" refers to robot 1
        public int Team2; // Anything after with the suffix "2" refers to robot 2
        public int Team3; // Anything after with the suffix "3" refers to robot 3
        public int TeamAtAmp;
        public int AutoCenterNotes;
        public int Team1Defense;
        public int Team2Defense;
        public int Team3Defense;
        public int Team1DriverSkill;
        public int Team2DriverSkill;
        public int Team3DriverSkill;
        public int AmplifyCount;
        public int Fouls;
        public bool Coopertition;
        public int HighNotes;
        public int HighNotePotential;
        public string Harmony;
        public string Team1Comments;
        public string Team2Comments;
        public string Team3Comments;

    }
    
}
