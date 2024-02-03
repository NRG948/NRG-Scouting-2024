using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Rater : MonoBehaviour
{
    public GameObject dataManObject;
    private DataManager dataManager;
    public string key;
    // Start is called before the first frame update
    void Awake()
    {
        dataManager = dataManObject.GetComponent<DataManager>();
        UpdateRating(3);
    }

    public void UpdateRating(int rating)
    {
        dataManager.SetInt(key, rating, true);
    }
}
