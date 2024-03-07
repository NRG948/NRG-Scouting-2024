using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class PitDropdown : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private GameObject dataManObject;
    private DataManager dataManager;
    public string key;
    // Start is called before the first frame update
    void Start()
    {
        dataManager = dataManObject.GetComponent<DataManager>();
        Get(0);

    }

    public void Get(Int32 index)
    {
        string craaaazyValue = GetComponent<TMP_Dropdown>().options[index].text;

        dataManager.SetString(key, craaaazyValue, isPit: true);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        HapticManager.LightFeedback();
    }
}
