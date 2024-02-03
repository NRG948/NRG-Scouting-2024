
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamAtAmp : MonoBehaviour
{
    public GameObject dataManObject;
    private DataManager dataManager;
    // Start is called before the first frame update
    void Start()
    {
        dataManager = dataManObject.GetComponent<DataManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
