using System;
using System.Collections;
using System.Collections.Generic;
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
        UpdatePage(0);
    }
    public void UpdatePage(Int32 currentPage)
    {
        if (SceneManager.GetActiveScene().name == "SubjectiveScout" || SceneManager.GetActiveScene().name == "V2SubjectiveScout")
        {
            pageNames = new string[] { "Setup", "Early Game", Team1.text == "" ? "Team 1" : Team1.text, Team2.text == "" ? "Team 2" : Team2.text, Team3.text == "" ? "Team 3" : Team3.text, "Endgame" };
        }

        if (currentPage < pageNames.Length) {
            transform.GetComponent<TMP_Text>().text = pageNames[currentPage];
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
        if (!alertOn && Input.GetMouseButton(0) && -Content.GetComponent<RectTransform>().anchoredPosition.x / 2400 > pageNames.Length - 0.8) {
            alert.outwardFacing("Are you sure you want to save?|objSave");
            alertOn = true;
        }

        if (!Input.GetMouseButton(0)) { alertOn = false; }
    }
}
