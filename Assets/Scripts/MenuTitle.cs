using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuTitle : MonoBehaviour
{
    public string[] pageNames;
    public TMP_InputField Team1;
    public TMP_InputField Team2;
    public TMP_InputField Team3;
    public GameObject Content;
    public AlertBox alert;
    private int currentPage = 0;
    public bool alertOn = false;
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "ObjectiveScout")
        {
            pageNames = new string[] { "Setup", "Auto", "Tele-Op", "Endgame" };
        }
        if (SceneManager.GetActiveScene().name == "PitScout")
        {
            pageNames = new string[] { "Setup", "Robot", "Auto", "Scoring", "Pickup", "Comments" };
        }
        if (SceneManager.GetActiveScene().name == "LocalDataViewer")
        {
            pageNames = new string[] { "Objective", "Subjective", "Pits" };
        }
        if (SceneManager.GetActiveScene().name == "SubjectiveScout" || SceneManager.GetActiveScene().name == "V2SubjectiveScout")
        {
            pageNames = new string[] { "Setup", "Misc","Comments" };
        }
        UpdatePage(0);
    }
    public void UpdatePage(Int32 currentPage)
    {
    

        if (currentPage < pageNames.Length) {
            transform.GetComponent<TMP_Text>().text = pageNames[currentPage];
        }
    }

    public void UpdateTeamNames() // Updates the team NUMBERS, im bad at naming things
    {
        foreach (var nameText in Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "Team1Name")) {
            nameText.GetComponent<TMP_Text>().text = Team1.text == "" ? "Team 1" : Team1.text;
        }
        foreach (var nameText in Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "Team2Name"))
        {
            nameText.GetComponent<TMP_Text>().text = Team2.text == "" ? "Team 2" : Team2.text;
        }
        foreach (var nameText in Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "Team3Name"))
        {
            nameText.GetComponent<TMP_Text>().text = Team3.text == "" ? "Team 3" : Team3.text;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "LocalDataViewer") { return; }
        if (currentPage != (int)-Math.Round(Content.GetComponent<RectTransform>().anchoredPosition.x / 2400))
        {
            currentPage = (int)-Math.Round(Content.GetComponent<RectTransform>().anchoredPosition.x / 2400);
            UpdatePage(currentPage);
        }
        if (!alertOn && Input.GetMouseButton(0)) {
            if (-Content.GetComponent<RectTransform>().anchoredPosition.x / 2400 > pageNames.Length - 0.98)
            {
                alert.outwardFacing($"Are you sure you want to save?|{(SceneManager.GetActiveScene().name == "ObjectiveScout" ? "objsave" : SceneManager.GetActiveScene().name == "SubjectiveScout" ? "subjSave" : "pitSave")}");
                alertOn = true;
            }
            if (Content.GetComponent<RectTransform>().anchoredPosition.x > 100) {
                alert.outwardFacing($"Are you sure you want to quit? All unsaved data will be lost!|menu");
                alertOn = true;
            }
        }

        if (!Input.GetMouseButton(0)) { alertOn = false; }
    }
}
