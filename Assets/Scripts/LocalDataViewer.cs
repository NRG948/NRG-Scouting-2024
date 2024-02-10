using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

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
                Match objFileJson = JsonUtility.FromJson<Match>(File.ReadAllText(match));
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
                AllianceMatch subjFileJson = JsonUtility.FromJson<AllianceMatch>(File.ReadAllText(match));
                GameObject newSubjPrefab = subjPrefab;
                newSubjPrefab.transform.GetChild(0).GetChild(0).GetComponent<RawImage>().color = (subjFileJson.AllianceColor == "Red" ? new Color(0.8f,0f,0f) : new Color(0f,0.8f,0.8f));
                newSubjPrefab.transform.GetChild(1).GetChild(0).transform.GetComponent<TMP_Text>().text = subjFileJson.MatchType;
                newSubjPrefab.transform.GetChild(2).GetComponent<TMP_Text>().text = subjFileJson.MatchNumber.ToString();
                newSubjPrefab.transform.GetChild(3).GetComponent<TMP_Text>().text = $"{subjFileJson.Team1.ToString()} | {subjFileJson.Team2.ToString()} | {subjFileJson.Team3.ToString()}";
                newSubjPrefab.transform.GetChild(5).GetComponent<TMP_Text>().text = subjFileJson.ScouterName;
                newSubjPrefab.transform.GetChild(7).GetComponent<TMP_Text>().text = subjFileJson.RankingComments;
                newSubjPrefab.transform.GetChild(8).GetComponent<LDV_Buttons>().filePath = match;
                newSubjPrefab.transform.GetChild(9).GetComponent<LDV_Buttons>().filePath = match;
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
                Pit pitFileJson = JsonUtility.FromJson<Pit>(File.ReadAllText(match));
                GameObject newPitPrefab = pitPrefab;
                newPitPrefab.transform.GetChild(1).GetComponent<TMP_Text>().text = $"{pitFileJson.TeamNumber.ToString()} - {pitFileJson.TeamName}";
                newPitPrefab.transform.GetChild(3).GetComponent<TMP_Text>().text = "Interviewer: " + pitFileJson.Interviewer;
                newPitPrefab.transform.GetChild(5).GetComponent<TMP_Text>().text = "Interviewee: " + pitFileJson.Interviewee;
                newPitPrefab.transform.GetChild(6).GetComponent<LDV_Buttons>().filePath = match;
                newPitPrefab.transform.GetChild(7).GetComponent<LDV_Buttons>().filePath = match;
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
        GameObject.Find("AlertBox").GetComponent<AlertBox>().outwardFacing("Are you sure you want to delete this match? It will be lost forever (A long time)!|ldvDelete");
    }
    public void confirmDelete()
    {
        Debug.Log("Deleting " + deletionFilepath);
        File.Delete(deletionFilepath);
        Start();
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
}
