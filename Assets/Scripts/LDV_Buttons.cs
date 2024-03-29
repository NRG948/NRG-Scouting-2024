using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LDV_Buttons : MonoBehaviour
{
    public string filePath;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RunDataViewer(string key)
    {
        switch (key)
        {
            case "edit":
                GameObject.Find("EditMatch").GetComponent<LDV_EditMatch>().startMenu(filePath, "obj"); break;
            case "editSubj":
                GameObject.Find("EditMatch").GetComponent<LDV_EditMatch>().startMenu(filePath, "subj"); break;
            case "editPit":
                GameObject.Find("EditMatch").GetComponent<LDV_EditMatch>().startMenu(filePath, "pit"); break;
            case "delete":
                GameObject.Find("LocalDataViewer").transform.GetComponent<LocalDataViewer>().delete(filePath); break;
        }
    }
}
