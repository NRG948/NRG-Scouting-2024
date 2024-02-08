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
    LocalDataViewer.Match objFileJson;
    LocalDataViewer.AllianceMatch subjFileJson;
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
        mode = key;
        globalFilePath = filePath;
        switch (key) {
            case "obj":
                objFileJson = JsonUtility.FromJson<LocalDataViewer.Match>(File.ReadAllText(filePath));
                fields = objFileJson.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
                fieldNames = new string[fields.Length];
                for (int i = 0; i < fields.Length; i++) { fieldNames[i] = fields[i].Name; } // i hate this
                transform.GetChild(0).gameObject.SetActive(true);
                transform.GetChild(0).GetChild(1).GetComponent<TMP_Dropdown>().ClearOptions();
                transform.GetChild(0).GetChild(1).GetComponent<TMP_Dropdown>().AddOptions(fieldNames.Select(option => new TMP_Dropdown.OptionData(option)).ToList());
                transform.GetChild(0).GetChild(2).GetComponent<TMP_Text>().text = $"Editing Match {objFileJson.MatchNumber}";break;
            case "subj":
                subjFileJson = JsonUtility.FromJson<LocalDataViewer.AllianceMatch>(File.ReadAllText(filePath));
                fields = subjFileJson.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
                fieldNames = new string[fields.Length];
                for (int i = 0; i < fields.Length; i++) { fieldNames[i] = fields[i].Name; } // i hate this
                transform.GetChild(0).gameObject.SetActive(true);
                transform.GetChild(0).GetChild(1).GetComponent<TMP_Dropdown>().ClearOptions();
                transform.GetChild(0).GetChild(1).GetComponent<TMP_Dropdown>().AddOptions(fieldNames.Select(option => new TMP_Dropdown.OptionData(option)).ToList());
                transform.GetChild(0).GetChild(2).GetComponent<TMP_Text>().text = $"Editing Match {subjFileJson.MatchNumber} on the {subjFileJson} alliance"; break;
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
        }
        
        
    }


    public void saveMenu()
    {
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
                GameObject.Find("AlertBox").GetComponent<AlertBox>().ShowBoxNoResponse("Successfully updated data.");
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
                        localField.SetValue(subjFileJson, savedValue); break;
                }
                File.WriteAllText(globalFilePath, JsonUtility.ToJson(subjFileJson));
                transform.GetChild(0).gameObject.SetActive(false);
                GameObject.Find("LocalDataViewer").GetComponent<LocalDataViewer>().Start();
                GameObject.Find("AlertBox").GetComponent<AlertBox>().ShowBoxNoResponse("Successfully updated data.");
                break;
        }
    }

    public void cancel()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
