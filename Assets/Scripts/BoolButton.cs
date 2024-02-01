using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        dataManager.SetBool(key, value);
    }
}
