using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SubjTextBox : MonoBehaviour
{
    private GameObject dataManObject;
    private DataManager dataManager;
    public string key;
    public GameObject matchTitle;
    // Start is called before the first frame update
    void Start()
    {
        dataManObject = GameObject.Find("DataManager");
        dataManager = dataManObject.GetComponent<DataManager>();
        if (key == "ScouterName")
        {
            GetComponent<TMP_InputField>().text = PlayerPrefs.GetString("Name");
            matchTitle.GetComponent<TMP_Text>().text = "MATCH - " + PlayerPrefs.GetString("EventKey").ToUpper();
        }

    }

    // Update is called once per frame
    public void GetText(string value)
    {
        if (value == "") { return; }
        if (key == "MatchNumber" || key == "TeamNumber")
        {
            if (value.Length >= 10) { return; } // Edge case in an edge case
            dataManager.SetInt(key, Int32.Parse(value),true); // Random edge case
        }
        else
        {

            dataManager.SetString(key, value,true);
        }
    }
}
