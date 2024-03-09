using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SubjDropdownScript1 : MonoBehaviour, IPointerClickHandler
{
    private GameObject dataManObject;
    private DataManager dataManager;
    public string key;
    // Start is called before the first frame update
    void Start()
    {
        dataManObject = GameObject.Find("DataManager");
        dataManager = dataManObject.GetComponent<DataManager>();
        Get(0);

    }

    // Update is called once per frame. Unfortunately, this is not update
    public void Get(Int32 index)
    {
        string craaaazyValue = GetComponent<TMP_Dropdown>().options[index].text;
        if (key == "AllianceColor")
        {
            string[] splitValue = craaaazyValue.Split(" ");
            dataManager.SetString("AllianceColor", splitValue[0],true);
            dataManager.SetInt("DriverStation", Int32.Parse(splitValue[1]),true);
        } else
        {
         
        dataManager.SetString(key, craaaazyValue,true);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        HapticManager.LightFeedback();
    }
}
