using UnityEngine;
using UnityEngine.UI;

public class AtAmp : MonoBehaviour //Deprecated
{
    private GameObject dataManagerObject;
    private DataManager dataManager;
    private ToggleGroup toggleGroup;

    // Start is called before the first frame update
    void Start()
    {
        dataManagerObject = GameObject.Find("DataManager");
        dataManager = dataManagerObject.GetComponent<DataManager>();
        toggleGroup = gameObject.GetComponent<ToggleGroup>();
    }

    /*public void onValueChanged(int value)
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
