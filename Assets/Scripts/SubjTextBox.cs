using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class SubjTextBox : MonoBehaviour
{
    private GameObject dataManObject;
    private DataManager dataManager;
    public string key;
    public GameObject matchTitle;
    public GameObject menuTitle;
    // Start is called before the first frame update
    void Start()
    {
        dataManObject = GameObject.Find("DataManager");
        dataManager = dataManObject.GetComponent<DataManager>();
        try { menuTitle.GetComponent<MenuTitle>().UpdateTeamNames(); } catch { }
        if (key == "ScouterName")
        {
            GetComponent<TMP_InputField>().text = PlayerPrefs.GetString("Name");
            matchTitle.GetComponent<TMP_Text>().text = "MATCH - " + PlayerPrefs.GetString("EventKey").ToUpper();
        }

    }

    // Update is called once per frame
    public void GetText(string value)
    {
        if (value == "") { dataManager.ClearTeam(); return; }
        if (key == "MatchNumber" || key == "TeamNumber")
        {
            if (value.Length >= 10) { GetComponent<TMP_InputField>().text = value.Substring(0, value.Length - 1); return; } // Edge case in an edge case
            try
            {
                dataManager.SetInt(key, Int32.Parse(value),true); // Random edge case}
            }
            catch
            {
                GetComponent<TMP_InputField>().text = value.Substring(0, value.Length - 1);
            }
        }
        else
        {

            dataManager.SetString(key, value,true);
        }
        if (key == "MatchNumber" || key == "TeamNumber")
        {
            if (key == "MatchNumber")
            {
                dataManager.AutofillTeamNumberSubjective();
            }
            if (Directory.Exists(Application.persistentDataPath + "/cache/teams"))
            {
                dataManager.AutoFillTeamNameSubjective();
            }
        }
    }
}
