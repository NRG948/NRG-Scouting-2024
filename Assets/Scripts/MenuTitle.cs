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
    private int currentPage = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "ObjectiveScout")
        {
            pageNames = new string[] { "Setup", "Auto", "Tele-Op", "Endgame" };
        }
        if (SceneManager.GetActiveScene().name == "PitScout")
        {
            pageNames = new string[] { "Setup", "Auto", "Scoring", "Pickup" };
        }
        UpdatePage(0);
    }
    public void UpdatePage(Int32 currentPage)
    {
        if (SceneManager.GetActiveScene().name == "SubjectiveScout" || SceneManager.GetActiveScene().name == "V2SubjectiveScout")
        {
            pageNames = new string[] { "Setup", "Early Game", Team1.text == "" ? "Team 1" : Team1.text, Team2.text == "" ? "Team 2" : Team2.text, Team3.text == "" ? "Team 3" : Team3.text, "Endgame" };
        }
        transform.GetComponent<TMP_Text>().text = pageNames[currentPage];
    }
    // Update is called once per frame
    void Update()
    {
        if (currentPage != (int)-Math.Round(Content.GetComponent<RectTransform>().anchoredPosition.x / 2400))
        {
            currentPage = (int)-Math.Round(Content.GetComponent<RectTransform>().anchoredPosition.x / 2400);
            UpdatePage(currentPage);
        }
    }
}
