using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Rater : MonoBehaviour
{
    public GameObject dataManObject;
    private DataManager dataManager;
    private ToggleGroup toggleGroup;
    public string key;
    // Start is called before the first frame update
    void Awake()
    {
        dataManager = dataManObject.GetComponent<DataManager>();
        toggleGroup = gameObject.GetComponent<ToggleGroup>();
    }

    public void UpdateRating(int rating) // This is used by subjective data raters
    {
        if (toggleGroup.AnyTogglesOn()) { HapticManager.LightFeedback(); } 
        else { HapticManager.HeavyFeedback(); }

        if (toggleGroup.AnyTogglesOn()) { dataManager.SetInt(key, rating, true); } else { dataManager.SetInt(key, 0,true); }
    }

    public void UpdateRatingObjective(int rating)
    {
        if (toggleGroup.AnyTogglesOn()) { HapticManager.LightFeedback(); }
        else { HapticManager.HeavyFeedback(); }

        if (toggleGroup.AnyTogglesOn()) { dataManager.SetInt(key, rating); } else { dataManager.SetInt(key, 0); }
    }
}
