using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.VolumeComponent;

public class LDV_EditMatch : MonoBehaviour
{
    DataManager.Match objFileJson;
    DataManager.SubjectiveMatch subjFileJson;
    DataManager.Pit pitFileJson;
    public string mode;
    string[] fieldNames;
    FieldInfo[] fields;
    public string globalFilePath;
    FieldInfo localField;
    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startMenu(string filePath, string key)
    {
        HapticManager.LightFeedback();
        mode = key;
        globalFilePath = filePath;
        switch (key) {
            case "obj":
                objFileJson = JsonUtility.FromJson<DataManager.Match>(File.ReadAllText(filePath));
                fields = objFileJson.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
                fieldNames = new string[fields.Length];
                for (int i = 0; i < fields.Length; i++) { fieldNames[i] = fields[i].Name; } // i hate this
                transform.GetChild(0).gameObject.SetActive(true);
                transform.GetChild(0).GetChild(1).GetComponent<TMP_Dropdown>().ClearOptions();
                transform.GetChild(0).GetChild(1).GetComponent<TMP_Dropdown>().AddOptions(fieldNames.Select(option => new TMP_Dropdown.OptionData(option)).ToList());
                transform.GetChild(0).GetChild(2).GetComponent<TMP_Text>().text = $"Editing Match {objFileJson.MatchNumber}";break;
            case "subj":
                subjFileJson = JsonUtility.FromJson<DataManager.SubjectiveMatch>(File.ReadAllText(filePath));
                fields = subjFileJson.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
                fieldNames = new string[fields.Length];
                for (int i = 0; i < fields.Length; i++) { fieldNames[i] = fields[i].Name; } // i hate this
                transform.GetChild(0).gameObject.SetActive(true);
                transform.GetChild(0).GetChild(1).GetComponent<TMP_Dropdown>().ClearOptions();
                transform.GetChild(0).GetChild(1).GetComponent<TMP_Dropdown>().AddOptions(fieldNames.Select(option => new TMP_Dropdown.OptionData(option)).ToList());
                transform.GetChild(0).GetChild(2).GetComponent<TMP_Text>().text = $"Editing Match {subjFileJson.MatchNumber} on the {subjFileJson} alliance"; break;
            case "pit":
                pitFileJson = JsonUtility.FromJson<DataManager.Pit>(File.ReadAllText(filePath));
                fields = pitFileJson.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
                fieldNames = new string[fields.Length];
                for (int i = 0; i < fields.Length; i++) { fieldNames[i] = fields[i].Name; } // i hate this
                transform.GetChild(0).gameObject.SetActive(true);
                transform.GetChild(0).GetChild(1).GetComponent<TMP_Dropdown>().ClearOptions();
                transform.GetChild(0).GetChild(1).GetComponent<TMP_Dropdown>().AddOptions(fieldNames.Select(option => new TMP_Dropdown.OptionData(option)).ToList());
                transform.GetChild(0).GetChild(2).GetComponent<TMP_Text>().text = $"Editing {pitFileJson.Interviewer}'s pit scout of {pitFileJson.TeamNumber.ToString()}"; break;
        }
        updateMenu(0);
    }

    public void updateMenu(Int32 index)
    {
        switch (mode) {
            case "obj":
                transform.GetChild(0).GetChild(3).GetComponent<TMP_InputField>().text = objFileJson.GetType().GetField(transform.GetChild(0).GetChild(1).GetComponent<TMP_Dropdown>().options[index].text).GetValue(objFileJson).ToString(); break;
            case "subj":
                transform.GetChild(0).GetChild(3).GetComponent<TMP_InputField>().text = subjFileJson.GetType().GetField(transform.GetChild(0).GetChild(1).GetComponent<TMP_Dropdown>().options[index].text).GetValue(subjFileJson).ToString(); break;
            case "pit":
                transform.GetChild(0).GetChild(3).GetComponent<TMP_InputField>().text = pitFileJson.GetType().GetField(transform.GetChild(0).GetChild(1).GetComponent<TMP_Dropdown>().options[index].text).GetValue(pitFileJson).ToString(); break;
        }
        
        
    }


    public void saveMenu()
    {
        HapticManager.HeavyFeedback();
        string savedValue = transform.GetChild(0).GetChild(3).GetComponent<TMP_InputField>().text;
        switch (mode)
        {
            case "obj":
                localField = objFileJson.GetType().GetField(transform.GetChild(0).GetChild(1).GetComponent<TMP_Dropdown>().captionText.text);
                switch (localField.FieldType.ToString())
                {
                    case "System.Int32":
                        Int32 parsedValue;
                        bool parseSuccess = Int32.TryParse(savedValue, out parsedValue);
                        localField.SetValue(objFileJson, parseSuccess ? parsedValue : localField.GetValue(objFileJson));break;
                    case "System.Boolean":
                        localField.SetValue(objFileJson,savedValue.ToLower() == "true" ? true : false); break;
                    case "System.String":
                        localField.SetValue(objFileJson, savedValue); break;
                }
                Debug.Log(localField.FieldType);
                File.WriteAllText(globalFilePath, JsonUtility.ToJson(objFileJson));
                transform.GetChild(0).gameObject.SetActive(false);
                GameObject.Find("LocalDataViewer").GetComponent<LocalDataViewer>().Start();
                GameObject.Find("AlertBox").GetComponent<AlertBox>().ShowNotificationBox("Successfully updated data.");
                break;

            case "subj":
                localField = subjFileJson.GetType().GetField(transform.GetChild(0).GetChild(1).GetComponent<TMP_Dropdown>().captionText.text);
                switch (localField.FieldType.ToString())
                {
                    case "System.Int32":
                        Int32 parsedValue;
                        bool parseSuccess = Int32.TryParse(savedValue, out parsedValue);
                        localField.SetValue(subjFileJson, parseSuccess ? parsedValue : localField.GetValue(subjFileJson)); break;
                    case "System.Boolean":
                        localField.SetValue(subjFileJson, savedValue.ToLower() == "true" ? true : false); break;
                    case "System.String":

                        // Test case for AutoPickups Format
                        if (localField.Name == "AutoPickups")
                        {
                            string[] autoPickupTestCase = savedValue.Split(new string[] { ", " }, StringSplitOptions.None);
                            if (autoPickupTestCase.Length == 1 || autoPickupTestCase.Length > 8)
                            { goto AutoPickupFailure; }
                            foreach (var item in autoPickupTestCase)
                            {
                                if (autoPickupTestCase.Count(s => s == item) > 1) { goto AutoPickupFailure; }
                                if (Int32.TryParse(item, out parsedValue)) { if (parsedValue <= 7) { localField.SetValue(subjFileJson, savedValue); goto AutoPickupSuccess; } }
                            }
                        }
                        
                        // Other strings don't require a test case
                        else
                        {

                            localField.SetValue(subjFileJson, savedValue); break;
                        }
                        // This only runs if AutoPickups is enabled and the test case doesn't break (the test case only breaks when it succeeds)
                        AutoPickupFailure:
                        StartCoroutine(GameObject.Find("AlertBox").GetComponent<AlertBox>().ShowNotificationBox("DATA INCORRECTLY FORMATTED! Each number should have a comma and a space after it, such as \"1, 2, 3\". Please ask your strategy coordinator for help.")); return;
                }
                AutoPickupSuccess:
                File.WriteAllText(globalFilePath, JsonUtility.ToJson(subjFileJson));
                transform.GetChild(0).gameObject.SetActive(false);
                GameObject.Find("LocalDataViewer").GetComponent<LocalDataViewer>().Start();
                GameObject.Find("AlertBox").GetComponent<AlertBox>().ShowNotificationBox("Successfully updated data.");
                break;

            case "pit":
                localField = pitFileJson.GetType().GetField(transform.GetChild(0).GetChild(1).GetComponent<TMP_Dropdown>().captionText.text);
                switch (localField.FieldType.ToString())
                {
                    case "System.Int32":
                        Int32 parsedValue;
                        bool parseSuccess = Int32.TryParse(savedValue, out parsedValue);
                        localField.SetValue(pitFileJson, parseSuccess ? parsedValue : localField.GetValue(pitFileJson)); break;
                    case "System.Boolean":
                        localField.SetValue(pitFileJson, savedValue.ToLower() == "true" ? true : false); break;
                    case "System.String":
                        localField.SetValue(pitFileJson, savedValue); break;
                }
                File.WriteAllText(globalFilePath, JsonUtility.ToJson(pitFileJson));
                transform.GetChild(0).gameObject.SetActive(false);
                GameObject.Find("LocalDataViewer").GetComponent<LocalDataViewer>().Start();
                GameObject.Find("AlertBox").GetComponent<AlertBox>().ShowNotificationBox("Successfully updated data.");
                break;
        }
    }

    public void cancel()
    {
        HapticManager.HeavyFeedback();
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
