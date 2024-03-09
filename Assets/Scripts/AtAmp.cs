using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AtAmp : MonoBehaviour
{
    private GameObject DataManObject;
    private DataManager dataManager;
    private ToggleGroup toggleGroup;

    // Start is called before the first frame update
    void Start()
    {
        DataManObject = GameObject.Find("DataManager");
        dataManager = DataManObject.GetComponent<DataManager>();
        toggleGroup = gameObject.GetComponent<ToggleGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*public void valueChanged(int value)
    {
        if (toggleGroup.AnyTogglesOn()) { HapticManager.LightFeedback(); }
        else { HapticManager.HeavyFeedback(); }
        switch (value)
        {
            case 1:
                dataManager.subjectiveMatch.TeamAtAmp = dataManager.subjectiveMatch.Team1;
                break;
            case 2:
                dataManager.subjectiveMatch.TeamAtAmp = dataManager.subjectiveMatch.Team2;
                break;
            case 3:
                dataManager.subjectiveMatch.TeamAtAmp = dataManager.subjectiveMatch.Team3;
                break;
        }
    }*/
}
