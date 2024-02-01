using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SaveSystem : MonoBehaviour
{
    public TMP_InputField inputField;
    public TMP_InputField inputFieldID;


    void Start()
    {
        inputField.text = PlayerPrefs.GetString("Name");
        inputFieldID.text = PlayerPrefs.GetString("EventKey");

    }

    public void SaveData()
    {
        PlayerPrefs.SetString("Name", inputField.text);
        PlayerPrefs.SetString("EventKey", inputFieldID.text);
    }


    public void DeletData()
    {
        PlayerPrefs.DeleteAll();
    }
}
