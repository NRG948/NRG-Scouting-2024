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
    public string mode;
    string[] fieldNames;
    public string globalFilePath;
    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startMenu(string filePath)
    {
        mode = "obj";
        globalFilePath = filePath;
        objFileJson = JsonUtility.FromJson<LocalDataViewer.Match>(File.ReadAllText(filePath));
        FieldInfo[] fields = objFileJson.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
        fieldNames  = new string[fields.Length];
        for (int i = 0; i < fields.Length; i++) { fieldNames[i] = fields[i].Name; } // i hate this
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(0).GetChild(1).GetComponent<TMP_Dropdown>().ClearOptions();
        transform.GetChild(0).GetChild(1).GetComponent<TMP_Dropdown>().AddOptions(fieldNames.Select(option => new TMP_Dropdown.OptionData(option)).ToList());
        transform.GetChild(0).GetChild(2).GetComponent<TMP_Text>().text = $"Editing Match {objFileJson.MatchNumber} for Team {objFileJson.TeamNumber}";
        updateMenu(0);
    }

    public void updateMenu(Int32 index)
    {
        switch (mode) {
            case "obj":
                transform.GetChild(0).GetChild(3).GetComponent<TMP_InputField>().text = objFileJson.GetType().GetField(transform.GetChild(0).GetChild(1).GetComponent<TMP_Dropdown>().options[index].text).GetValue(objFileJson).ToString(); break;
        }
        
        
    }


    public void saveMenu()
    {
        switch (mode)
        {
            case "obj":
                FieldInfo localField = objFileJson.GetType().GetField(transform.GetChild(0).GetChild(1).GetComponent<TMP_Dropdown>().captionText.text);
                switch (localField.FieldType.ToString())
                {
                    case "System.Int32":
                        Int32 parsedValue;
                        bool parseSuccess = Int32.TryParse(transform.GetChild(0).GetChild(3).GetComponent<TMP_InputField>().text, out parsedValue);
                        localField.SetValue(objFileJson, parseSuccess ? parsedValue : localField.GetValue(objFileJson));break;
                    case "System.Boolean":
                        break;
                    case "System.String":
                        break;
                }
                Debug.Log(localField.FieldType);
                File.WriteAllText(globalFilePath, JsonUtility.ToJson(objFileJson));
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
