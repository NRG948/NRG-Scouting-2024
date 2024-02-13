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
    public AllianceMatch allianceMatch;
    public Pit pit;
    public APIMatchFile apiMatch;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "ObjectiveScout") { match = new Match(); }
        if (SceneManager.GetActiveScene().name == "SubjectiveScout") { allianceMatch = new AllianceMatch(); }
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
            allianceMatch.GetType().GetField(key).SetValue(allianceMatch, value);
        }
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
            allianceMatch.GetType().GetField(key).SetValue(allianceMatch, value);
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
        string currentTime = (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds.ToString().Truncate(10, "");
        string fileName = $"{match.TeamNumber}_{match.MatchType}_{match.MatchNumber}_{currentTime}.json";
        string jsonData = JsonUtility.ToJson(match, true);
        File.WriteAllText(objectivePath + fileName, jsonData);
        StartCoroutine(GameObject.Find("AlertBox").GetComponent<AlertBox>().ShowBoxNoResponse("Successfully Saved Data"));
    }
    public void SaveAllianceScout()
    {
        //if (allianceMatch.Team1DriverSkill == 0 || allianceMatch.Team2DriverSkill == 0 || allianceMatch.Team3DriverSkill == 0 || allianceMatch.Team1Defense == 0 || allianceMatch.Team2Defense == 0 || allianceMatch.Team3Defense == 0)
        //{ StartCoroutine(GameObject.Find("AlertBox").GetComponent<AlertBox>().ShowBoxNoResponse("MISSING DATA DETECTED! DATA NOT SAVED! Please rank every team before continuing.")); return; }
        //if (allianceMatch.Team1Defense == allianceMatch.Team2Defense || allianceMatch.Team2Defense == allianceMatch.Team3Defense || allianceMatch.Team1Defense == allianceMatch.Team3Defense)
        //{ StartCoroutine(GameObject.Find("AlertBox").GetComponent<AlertBox>().ShowBoxNoResponse("CONFLICTS DETECTED! DATA NOT SAVED! Please check defense ratings.")); return; }
        //if (allianceMatch.Team1DriverSkill == allianceMatch.Team2DriverSkill || allianceMatch.Team2DriverSkill == allianceMatch.Team3DriverSkill || allianceMatch.Team1DriverSkill == allianceMatch.Team3DriverSkill)
        //{ StartCoroutine(GameObject.Find("AlertBox").GetComponent<AlertBox>().ShowBoxNoResponse("CONFLICTS DETECTED! DATA NOT SAVED! Please check driver skill ratings.")); return; }
        string subjectivePath = $"{Application.persistentDataPath}/{PlayerPrefs.GetString("EventKey")}/subj/";
        if (!(Directory.Exists(subjectivePath)))
        {
            Directory.CreateDirectory(subjectivePath);
        }
        string currentTime = (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds.ToString().Truncate(10, "");
        string fileName = $"{allianceMatch.MatchType}_{allianceMatch.MatchNumber}_{allianceMatch.AllianceColor}_{currentTime}.json";
        string jsonData = JsonUtility.ToJson(allianceMatch, true);
        File.WriteAllText(subjectivePath + fileName, jsonData);
        StartCoroutine(GameObject.Find("AlertBox").GetComponent<AlertBox>().ShowBoxNoResponse("Successfully Saved Data"));
    }

    public void SavePitScout()
    {
        string pitjectivePath = $"{Application.persistentDataPath}/{PlayerPrefs.GetString("EventKey")}/pit/";
        if (!(Directory.Exists(pitjectivePath)))
        {
            Directory.CreateDirectory(pitjectivePath);
        }
        string currentTime = (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds.ToString().Truncate(10, "");
        string fileName = $"{pit.TeamNumber}_pit_{currentTime}.json";
        string jsonData = JsonUtility.ToJson(pit, true);
        File.WriteAllText(pitjectivePath + fileName, jsonData);
        StartCoroutine(GameObject.Find("AlertBox").GetComponent<AlertBox>().ShowBoxNoResponse("Successfully Saved Data"));
    }

    public void AutofillTeamNumberObjective()
    {
        string matchNum = GameObject.Find("Match Number").GetComponent<TMP_InputField>().text;
        string matchType = GameObject.Find("Match Type").GetComponent<TMP_Dropdown>().captionText.text;
        string allianceColor = GameObject.Find("Alliance Color Number").GetComponent<TMP_Dropdown>().captionText.text.Split(" ")[0]; // First word of alliance color
        int teamIndex = Int32.Parse(GameObject.Find("Alliance Color Number").GetComponent<TMP_Dropdown>().captionText.text.Split(" ")[1]) - 1; // Index in a list
        string matchKey = PlayerPrefs.GetString("EventKey","2002nrg");
        if (PlayerPrefs.GetInt("Autofill") == 0 || !(PlayerPrefs.HasKey("Autofill"))) { return; }
        if (matchNum == "") { return; }
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
        GameObject.Find("TeamNumber").GetComponent<TMP_InputField>().text = "";
    }

    public void AutoFillTeamNumberSubjective()
    {
        Debug.Log("start");
        string matchNum = GameObject.Find("Match Number").GetComponent<TMP_InputField>().text;
        string matchType = GameObject.Find("Match Type").GetComponent<TMP_Dropdown>().captionText.text;
        string allianceColor = GameObject.Find("Alliance Color").GetComponent<TMP_Dropdown>().captionText.text;
        string matchKey = PlayerPrefs.GetString("EventKey", "2002nrg");
        Debug.Log("get valuess");

        if (PlayerPrefs.GetInt("Autofill") == 0 || !(PlayerPrefs.HasKey("Autofill"))) { return; }
        Debug.Log("get valuess");

        if (matchNum == "") { return; }
        switch (matchType)
        {
            case "Qualifications": matchKey = matchKey + "_qm" + matchNum; break;
            case "Playoffs": matchKey = matchKey + "_sf" + matchNum + "m1"; break;
            case "Finals": matchKey = matchKey + "_f1m" + matchNum; break;
        }
        string filePath = Application.persistentDataPath + "/cache";
        if (!(Directory.Exists(filePath))) { return; }
        filePath = filePath + "/" + PlayerPrefs.GetString("EventKey") + ".json";
        if (!File.Exists(filePath)) { return; }
        apiMatch = JsonUtility.FromJson<APIMatchFile>(File.ReadAllText(filePath));

        Debug.Log("foreach");
        foreach (var match in apiMatch.matches)
        {
            Debug.Log("key");
            Debug.Log(matchKey);
            if (match.key == matchKey)
            {
                Debug.Log("alliance color");

                switch (allianceColor)
                {
                    case "Red": 
                        GameObject.Find("Team One").GetComponent<TMP_InputField>().text = match.alliances.red.team_keys[0].TrimStart("frc");
                        GameObject.Find("Team Two").GetComponent<TMP_InputField>().text = match.alliances.red.team_keys[1].TrimStart("frc");
                        GameObject.Find("Team Three").GetComponent<TMP_InputField>().text = match.alliances.red.team_keys[2].TrimStart("frc");
                        Debug.Log("fill");

                        return;
                    case "Blue":
                        GameObject.Find("Team One").GetComponent<TMP_InputField>().text = match.alliances.blue.team_keys[0].TrimStart("frc");
                        GameObject.Find("Team Two").GetComponent<TMP_InputField>().text = match.alliances.blue.team_keys[1].TrimStart("frc");
                        GameObject.Find("Team Three").GetComponent<TMP_InputField>().text = match.alliances.blue.team_keys[2].TrimStart("frc");
                        Debug.Log("fill");

                        return;
                }
            }
        }
        GameObject.Find("Team One").GetComponent<TMP_InputField>().text = "";
        GameObject.Find("Team Two").GetComponent<TMP_InputField>().text = "";
        GameObject.Find("Team Three").GetComponent<TMP_InputField>().text = "";
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

    public void AutoFillTeamNameSubjective()
    {
        for (int i = 1; i <= 3; i++) {
            int teamNum = Int32.Parse(allianceMatch.GetType().GetField($"Team{i}").GetValue(allianceMatch).ToString());
            if (teamNum == 0) { GameObject.Find($"TeamName{i}").GetComponent<TMP_Text>().text = ""; continue; } // Failsafe if team number is empty
            int pageNum = teamNum / 500; // JSON Files are organized into "pages" of 500
            if (!File.Exists($"{Application.persistentDataPath}/cache/teams/{pageNum * 500}.json")) { GameObject.Find($"TeamName{i}").GetComponent<TMP_Text>().text = ""; return; }
            TeamList teamNameJson = JsonUtility.FromJson<TeamList>(File.ReadAllText($"{Application.persistentDataPath}/cache/teams/{pageNum * 500}.json"));
            GameObject.Find($"TeamName{i}").GetComponent<TMP_Text>().text = "";
            foreach (APITeam team in teamNameJson.teams)
            {
                if (teamNum == team.team_number)
                {
                    GameObject.Find($"TeamName{i}").GetComponent<TMP_Text>().text = $"{team.team_number} - {team.nickname}";
                    break;
                }
            }
        }
    }

    public void ClearTeam(int num=0)
    {
        string numString = num == 0 ? "" : num.ToString();
        GameObject.Find($"TeamName{numString}").GetComponent<TMP_Text>().text = ""; // Clears team name
        if (SceneManager.GetActiveScene().name == "SubjectiveScout") { allianceMatch.GetType().GetField($"Team{num}").SetValue(allianceMatch, 0); } // Resets team number (Subjective)
        if (SceneManager.GetActiveScene().name == "ObjectiveScout") { match.TeamNumber = 0; } // Resets team number (Objective)
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
        public string ScouterName;
        public int Team1; // Anything after with the suffix "1" refers to robot 1
        public int Team2; // Anything after with the suffix "2" refers to robot 2
        public int Team3; // Anything after with the suffix "3" refers to robot 3
        public int TeamAtAmp;
        public int AutoCenterNotes;
        public int Team1TravelSpeed;
        public int Team2TravelSpeed;
        public int Team3TravelSpeed;
        public int Team1AlignSpeed;
        public int Team2AlignSpeed;
        public int Team3AlignSpeed;
        public int Team1Avoid;
        public int Team2Avoid;
        public int Team3Avoid;
        public int AmplifyCount;
        public int Fouls;
        public bool Coopertition;
        public int HighNotes;
        public int HighNotePotential;
        public string Harmony;
        public string RankingComments;
        public string StratComments;
        public string OtherComments;
        public bool WinMatch;

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
        public int CycleTimeSpeaker;
        public int CycleTimeAmp;
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