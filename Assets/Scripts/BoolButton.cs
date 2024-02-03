using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoolButton : MonoBehaviour
{
    private GameObject dataManObject;
    private DataManager dataManager;
    public string key;
    // Start is called before the first frame update
    void Start()
    {
        dataManObject = GameObject.Find("DataManager");
        dataManager = dataManObject.GetComponent<DataManager>();

    }

    // Update is called once per frame
    public void Get(bool value)
    {
        if (SceneManager.GetActiveScene().name == "SubjectiveScout") { dataManager.SetBool(key, value, true); }
        else
        {
            dataManager.SetBool(key, value);
        }
    }
}
