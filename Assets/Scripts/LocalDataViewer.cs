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
    public string deletionFilepath;
    // Start is called before the first frame update
    void Start()
    {
        filePath = Application.persistentDataPath;
        objPath = filePath + $"\\{PlayerPrefs.GetString("EventKey")}\\obj";
        subjPath = filePath + $"\\{PlayerPrefs.GetString("EventKey")}\\subj";
        pitPath = filePath + $"\\{PlayerPrefs.GetString("EventKey")}\\pit";
        
        for (int i = 0; i < objSpawner.transform.childCount; i++) {
            Destroy(objSpawner.transform.GetChild(i).gameObject);
        }
        foreach (var match in Directory.GetFiles(objPath))
        {
            Match objFileJson = JsonUtility.FromJson<Match>(File.ReadAllText(match));
            GameObject newObjPrefab = objPrefab;
            newObjPrefab.transform.GetChild(0).GetComponent<RawImage>().color = (objFileJson.AllianceColor == "Red" ? new Color(0.8862745098f, 0.3294117647f, 0.3294117647f) : new Color(0.3294117647f, 0.60784313725f, 0.8862745098f));
            newObjPrefab.transform.GetChild(2).GetComponent<TMP_Text>().text = objFileJson.TeamNumber.ToString();
            newObjPrefab.transform.GetChild(3).GetComponent<TMP_Text>().text = objFileJson.MatchType;
            newObjPrefab.transform.GetChild(4).GetComponent<TMP_Text>().text = objFileJson.MatchNumber.ToString();
            newObjPrefab.transform.GetChild(6).GetComponent<TMP_Text>().text = objFileJson.ScouterName;
            newObjPrefab.transform.GetChild(8).GetComponent<TMP_Text>().text = objFileJson.Comments == "" ? "" : objFileJson.Comments;
            newObjPrefab.transform.GetChild(9).GetComponent<LDV_Buttons>().filePath = match;
            newObjPrefab.transform.GetChild(10).GetComponent<LDV_Buttons>().filePath = match;
            Instantiate(newObjPrefab,objSpawner.transform);
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
