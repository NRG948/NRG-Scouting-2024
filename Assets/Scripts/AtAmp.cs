using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtAmp : MonoBehaviour
{
    private GameObject DataManObject;
    private DataManager dataManager;
    // Start is called before the first frame update
    void Start()
    {
        DataManObject = GameObject.Find("DataManager");
        dataManager = DataManObject.GetComponent<DataManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void valueChanged(int value)
    {
        switch (value)
        {
            case 1:
                dataManager.allianceMatch.TeamAtAmp = dataManager.allianceMatch.Team1;
                break;
            case 2:
                dataManager.allianceMatch.TeamAtAmp = dataManager.allianceMatch.Team2;
                break;
            case 3:
                dataManager.allianceMatch.TeamAtAmp = dataManager.allianceMatch.Team3;
                break;
        }
    }
}
