using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public Match match;
    
    // Start is called before the first frame update
    void Start()
    {
        match = new Match();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetInt(string key, int value)
    {
        match.GetType().GetField(key).SetValue(match,value);;
    }
    [System.Serializable]
    public class Match
    {
        public int TeamNumber;
        public string MatchType;
        public int MatchNumber;
        public bool Replay;
        public string AllianceColor;
        public int DriverStation;
        public string ScouterName;
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
        public bool Onstage;
        public bool Park;
        public bool Spotlight;
        public bool Trap;
    }
}
