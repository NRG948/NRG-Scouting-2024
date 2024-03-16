using CandyCoded.HapticFeedback;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    public Match match;
    public SubjectiveMatch subjectiveMatch;
    public Pit pit;
    public APIMatchFile apiMatch;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "ObjectiveScout") { match = new Match(); }
        if (SceneManager.GetActiveScene().name == "SubjectiveScout") { subjectiveMatch = new SubjectiveMatch(); }
        if (!PlayerPrefs.HasKey("EventKey")) { PlayerPrefs.SetString("EventKey", "2002nrg"); }
        if (!PlayerPrefs.HasKey("Name")) { PlayerPrefs.SetString("Name", "Anonymous"); }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetBool(string key, bool value, bool subj = false, bool isPit = false)
    {
        if (isPit)
        {
            pit.GetType().GetField(key).SetValue(pit, value);
        }
        else if (!subj) { match.GetType().GetField(key).SetValue(match, value); }
        else
        {
            subjectiveMatch.GetType().GetField(key).SetValue(subjectiveMatch, value);
        }
        if (value) { HapticManager.LightFeedback(); } else { HapticManager.HeavyFeedback(); }
    }
    public void SetString(string key, string value, bool subj = false, bool isPit = false)
    {
        if (isPit)
        {
            pit.GetType().GetField(key).SetValue(pit, value);
        }
        else if (!subj) { match.GetType().GetField(key).SetValue(match, value); }
        else
        {
            subjectiveMatch.GetType().GetField(key).SetValue(subjectiveMatch, value);
        }
    }
    public void SetInt(string key, int value, bool subj = false, bool isPit = false)
    {
        if (isPit)
        {
            pit.GetType().GetField(key).SetValue(pit, value);
        }
        else if (!subj) { match.GetType().GetField(key).SetValue(match, value); }
        else
        {
            subjectiveMatch.GetType().GetField(key).SetValue(subjectiveMatch, value);
        }
    }

    public void SaveRobotScout()
    {
        if (match.TeamNumber == 0 || match.MatchNumber == 0 || match.DataQuality == 0) { StartCoroutine(GameObject.Find("AlertBox").GetComponent<AlertBox>().ShowBoxNoResponse("COULDN'T SAVE! Missing team number, match number, or data quality.",false,true));return; }
        string objectivePath = $"{Application.persistentDataPath}/{PlayerPrefs.GetString("EventKey")}/obj/";
        if (!(Directory.Exists(objectivePath)))
        {
            Directory.CreateDirectory(objectivePath);
        }
        string currentTime = (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds.ToString().Truncate(10, "");
        string fileName = $"{match.TeamNumber}_{match.MatchType}_{match.MatchNumber}_{currentTime}.json";
        string jsonData = JsonUtility.ToJson(match, true);
        File.WriteAllText(objectivePath + fileName, jsonData);
        StartCoroutine(GameObject.Find("AlertBox").GetComponent<AlertBox>().ShowBoxNoResponse("Successfully Saved Data",true));
    }
    public void SaveSubjectiveRobotScout()
    {
        if (subjectiveMatch.TeamNumber == 0 || subjectiveMatch.MatchNumber == 0 || subjectiveMatch.DataQuality == 0) { StartCoroutine(GameObject.Find("AlertBox").GetComponent<AlertBox>().ShowBoxNoResponse("COULDN'T SAVE! Missing team nunmber, match number, or data quality.", false, true)); return; }
        string subjectivePath = $"{Application.persistentDataPath}/{PlayerPrefs.GetString("EventKey")}/subj/";
        if (!(Directory.Exists(subjectivePath)))
        {
            Directory.CreateDirectory(subjectivePath);
        }
        string currentTime = (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds.ToString().Truncate(10, "");
        string fileName = $"{subjectiveMatch.TeamNumber}_{subjectiveMatch.MatchType}_{subjectiveMatch.MatchNumber}_{currentTime}.json";
        string jsonData = JsonUtility.ToJson(subjectiveMatch, true);
        File.WriteAllText(subjectivePath + fileName, jsonData);
        //Debug.Log("wrote data to " + subjectivePath + fileName);
        StartCoroutine(GameObject.Find("AlertBox").GetComponent<AlertBox>().ShowBoxNoResponse("Successfully Saved Data",true));
    }


    public void SavePitScout()
    {
        if (pit.TeamNumber == 0) { StartCoroutine(GameObject.Find("AlertBox").GetComponent<AlertBox>().ShowBoxNoResponse("COULDN'T SAVE! Missing team number.")); return; }
        string pitjectivePath = $"{Application.persistentDataPath}/{PlayerPrefs.GetString("EventKey")}/pit/";
        if (!(Directory.Exists(pitjectivePath)))
        {
            Directory.CreateDirectory(pitjectivePath);
        }
        string currentTime = (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds.ToString().Truncate(10, "");
        string fileName = $"{pit.TeamNumber}_pit_{currentTime}.json";
        string jsonData = JsonUtility.ToJson(pit, true);
        File.WriteAllText(pitjectivePath + fileName, jsonData);
        StartCoroutine(GameObject.Find("AlertBox").GetComponent<AlertBox>().ShowBoxNoResponse("Successfully Saved Data", true));
    }

    public void AutofillTeamNumber()
    {
        string matchNum = GameObject.Find("Match Number").GetComponent<TMP_InputField>().text;
        string matchType = GameObject.Find("Match Type").GetComponent<TMP_Dropdown>().captionText.text;
        string allianceColor = GameObject.Find("Alliance Color Number").GetComponent<TMP_Dropdown>().captionText.text.Split(" ")[0]; // First word of alliance color
        int teamIndex = Int32.Parse(GameObject.Find("Alliance Color Number").GetComponent<TMP_Dropdown>().captionText.text.Split(" ")[1]) - 1; // Index in a list
        string matchKey = PlayerPrefs.GetString("EventKey","2002nrg");
        if (PlayerPrefs.GetInt("Autofill") == 0 || !(PlayerPrefs.HasKey("Autofill"))) { return; }
        if (matchNum == "") { GameObject.Find("TeamNumber").GetComponent<TMP_InputField>().text = ""; return; }
        switch (matchType)
        {
            case "Qualifications": matchKey = matchKey + "_qm" + matchNum; break;
            case "Playoffs": matchKey = matchKey + "_sf" + matchNum + "m1";break;
            case "Finals": matchKey = matchKey + "_f1m" + matchNum;break;
        }
        string filePath = Application.persistentDataPath + "/cache";
        if (!(Directory.Exists(filePath))) { return; }
        filePath = filePath + "/" + PlayerPrefs.GetString("EventKey") + ".json";
        if (!File.Exists(filePath)) { return; }
        apiMatch = JsonUtility.FromJson<APIMatchFile>(File.ReadAllText(filePath));

        foreach (var match in apiMatch.matches)
        {
            if (match.key == matchKey)
            {
                
                switch (allianceColor)
                {
                    case "Red": GameObject.Find("TeamNumber").GetComponent<TMP_InputField>().text = match.alliances.red.team_keys[teamIndex].TrimStart("frc"); return;
                    case "Blue": GameObject.Find("TeamNumber").GetComponent<TMP_InputField>().text = match.alliances.blue.team_keys[teamIndex].TrimStart("frc"); return;

                }
            }
        }
       
    }
    

    public void AutoFillTeamNameObjective()
    {
        int pageNum = match.TeamNumber / 500;
        if (!File.Exists($"{Application.persistentDataPath}/cache/teams/{pageNum * 500}.json")) { GameObject.Find("TeamName").GetComponent<TMP_Text>().text = ""; return; }
        TeamList teamNameJson = JsonUtility.FromJson<TeamList>(File.ReadAllText($"{Application.persistentDataPath}/cache/teams/{pageNum * 500}.json"));
        foreach (APITeam team in teamNameJson.teams)
        {
            if (match.TeamNumber == team.team_number)
            {
                GameObject.Find("TeamName").GetComponent<TMP_Text>().text = team.nickname;
                return;
            }
        }
        GameObject.Find("TeamName").GetComponent<TMP_Text>().text = "";
    }

    public void AutoFillTeamNamePit()
    {
        int pageNum = pit.TeamNumber / 500;
        if (!File.Exists($"{Application.persistentDataPath}/cache/teams/{pageNum * 500}.json")) { GameObject.Find("TeamName").GetComponent<TMP_InputField>().text = ""; return; }
        TeamList teamNameJson = JsonUtility.FromJson<TeamList>(File.ReadAllText($"{Application.persistentDataPath}/cache/teams/{pageNum * 500}.json"));
        foreach (APITeam team in teamNameJson.teams)
        {
            if (pit.TeamNumber == team.team_number)
            {
                GameObject.Find("TeamName").GetComponent<TMP_InputField>().text = team.nickname;
                return;
            }
        }
        GameObject.Find("TeamName").GetComponent<TMP_InputField>().text = "";
    }

    public void AutoFillTeamNameSubjective()
    {
        int pageNum = subjectiveMatch.TeamNumber / 500;
        if (!File.Exists($"{Application.persistentDataPath}/cache/teams/{pageNum * 500}.json")) { GameObject.Find("TeamName").GetComponent<TMP_Text>().text = ""; return; }
        TeamList teamNameJson = JsonUtility.FromJson<TeamList>(File.ReadAllText($"{Application.persistentDataPath}/cache/teams/{pageNum * 500}.json"));
        foreach (APITeam team in teamNameJson.teams)
        {
            if (subjectiveMatch.TeamNumber == team.team_number)
            {
                GameObject.Find("TeamName").GetComponent<TMP_Text>().text = team.nickname;
                return;
            }
        }
        GameObject.Find("TeamName").GetComponent<TMP_Text>().text = "";
    
    }

    public void ClearTeam(int num=0)
    {
        GameObject.Find($"TeamName").GetComponent<TMP_Text>().text = ""; // Clears team name
        if (SceneManager.GetActiveScene().name == "SubjectiveScout") { subjectiveMatch.TeamNumber = 0; GameObject.Find("TeamNumber").GetComponent<TMP_InputField>().text = ""; } // Resets team number (Subjective)
        if (SceneManager.GetActiveScene().name == "ObjectiveScout") { match.TeamNumber = 0; GameObject.Find("TeamNumber").GetComponent<TMP_InputField>().text = ""; } // Resets team number (Objective)
    }
    

    [System.Serializable]
    public class APITeam
    {
        public int team_number;
        public string nickname;
    }

    [System.Serializable]
    public class TeamList
    {
        public APITeam[] teams;
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
        public string StartPos;
        public string StartRegion;
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
        public bool Coopertition;
        public bool Onstage;
        public bool Park;
        public bool Spotlight;
        public bool Trap;
        public string Comments;
    }
    [System.Serializable]
    public class SubjectiveMatch
    {
        public int TeamNumber;
        public string MatchType;
        public int MatchNumber;
        public int DataQuality;
        public bool Replay;
        public string AllianceColor;
        public int DriverStation;
        public string ScouterName;
        public bool HPAtAmp;
        public string AutoPickups;
        public bool CanScoreSub;
        public bool CanScorePodium;
        public bool CanScoreOther;
        public bool Feeder;
        public bool Coopertition;
        public string HumanPlayerComments;
        public string Comments;

    }
    [System.Serializable]
    public class Pit
    {
        public string TeamName;
        public int TeamNumber;
        public string Interviewer;
        public string Interviewee;
        public string RobotHeight;
        public string RobotLengthWidth;
        public string RobotWeight;
        public bool Vision;
        public string VisionCapability;
        public string DriveTrain;
        public string RobotMechanism;

        public int AutoPieces;
        public bool ScoresAmp;
        public bool ScoresSpeaker;
        public bool LeaveWing;

        public string ScoringPreference;
        public bool CanScoreAmp;
        public bool CanScoreSpeaker;
        public string CycleTimeSpeaker;
        public string CycleTimeAmp;
        public bool PickupFromGround;
        public bool PickupFromSource;

        public string DriverExperience;
        public string TeamComments;
        public string PersonalComments;
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