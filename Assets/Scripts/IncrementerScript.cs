using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IncrementerScript : MonoBehaviour
{
    public int counter = 0;
    public string key;
    private GameObject dataManObject;
    private DataManager dataManager;
    private TMP_Text txt;

    // Start is called before the first frame update
    void Start()
    {
     txt = GetComponent<TMP_Text>();
     dataManObject = GameObject.Find("DataManager");
     dataManager = dataManObject.GetComponent<DataManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void increase() {
        if (counter < 99)
        {
            counter++;
            txt.text = counter.ToString();
            dataManager.SetInt(key, counter);
        }
    }

    public void decrease() {
        if (counter > 0)
        {
            counter--;
            txt.text = counter.ToString();
            dataManager.SetInt(key, counter);
        }
        
    }
}
