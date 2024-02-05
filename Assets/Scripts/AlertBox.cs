using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using UnityEngine;

public class AlertBox : MonoBehaviour
{
    public bool no;
    public bool yes;
    public GameObject yesButton;
    public GameObject noButton;
    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

 
    public void outwardFacing(string messageKey)
    {
        string message = messageKey.Split('|')[0];
        string key = messageKey.Split("|")[1];
        StartCoroutine(ShowBox(message, key));
    }
    public IEnumerator ShowBoxNoResponse(string message)
    {
        yes = false;
        yesButton.GetComponent<AlertBoxButton>().on = false;
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = message;
        transform.GetChild(0).GetChild(3).gameObject.SetActive(false);
        while (!(yes))
        {
            yes = yesButton.GetComponent<AlertBoxButton>().on;
            yield return null;
        }
        transform.GetChild(0).gameObject.SetActive(false);
    }
    private IEnumerator ShowBox(string message,string key)
    {
        no = false;
        yes = false;
        yesButton.GetComponent<AlertBoxButton>().on = false; noButton.GetComponent<AlertBoxButton>().on = false;
        transform.GetChild(0).gameObject.SetActive (true);
        transform.GetChild(0).GetChild(3).gameObject.SetActive(true);

        transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = message;
        while (!(no || yes))
        {
            yes = yesButton.GetComponent<AlertBoxButton>().on;
            no = noButton.GetComponent<AlertBoxButton>().on;
            yield return null;

        };
        transform.GetChild(0).gameObject.SetActive(false);
        if (yes)
        {
            StupidSpaghettiCode(key);
        }
    }
    // Update is called once per frame
    public void setTrue()
    {
        no=true;
        yes=true;
    }
    public void setFalse() { no = true; }

    private void StupidSpaghettiCode(string key) { 
    
        switch (key)
        {
            case "settingsSave":
                GameObject.Find("Scripts").GetComponent<SaveSystem>().SaveData(); break;
            case "settingsDelete":
                GameObject.Find("Scripts").GetComponent<SaveSystem>().DeletData(); break;
            case "objSave":
                GameObject.Find("DataManager").GetComponent<DataManager>().SaveRobotScout(); break;
            case "subjSave":
                GameObject.Find("DataManager").GetComponent<DataManager>().SaveAllianceScout(); break;
            case "menu":
                GameObject.Find("HomeButton").GetComponent<HomeButton>().homeButton(); break;
        }
    }
}
