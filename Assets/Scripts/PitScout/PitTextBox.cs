using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PitTextBox : MonoBehaviour
{
    private GameObject dataManObject;
    private DataManager dataManager;
    public string key;

    // Start is called before the first frame update
    void Start()
    {
        dataManObject = GameObject.Find("DataManager");
        dataManager = dataManObject.GetComponent<DataManager>();
        if (key == "Interviewer")
        {
            GetComponent<TMP_InputField>().text = PlayerPrefs.GetString("Name");
        }

    }

    public void GetText(string value)
    {
        if (value == "") { return; }
        bool IsInt = key == "TeamNumber" || key == "AutoPieces" || key == "CycleTimeSpeaker" || key == "CycleTimeAmp";
        if (IsInt)
        {
            if (value.Length >= 10) { GetComponent<TMP_InputField>().text = value.Substring(0, value.Length - 1); return; } // Edge case in an edge case
            try
            {
                dataManager.SetInt(key, Int32.Parse(value), isPit: true); // Random edge case}
            }
            catch
            {
                GetComponent<TMP_InputField>().text = value.Substring(0, value.Length - 1);
            }
        }
        else
        {

            dataManager.SetString(key, value, isPit: true);
        }
    }
}
