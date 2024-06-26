using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;



public class MainMenu : MonoBehaviour
{
    
    void Start()
    {
        if (!(Directory.Exists($"{Application.persistentDataPath}/cache/teams"))) { 
            GameObject.Find("AlertBox").GetComponent<AlertBox>().ShowBoxByMessageKey("OPTIONAL: Downloading team names allows for additional QOL features. Would you like to proceed? This requires an internet connection. (398KB)|downloadMatches");
        }
        GameObject.Find("WelcomeText").GetComponent<TMP_Text>().text = PlayerPrefs.HasKey("Name") ? $"Welcome back, {PlayerPrefs.GetString("Name")}" : "Welcome, Anonymous";
        GameObject.Find("EventText").GetComponent<TMP_Text>().text = (PlayerPrefs.HasKey("EventKey") && PlayerPrefs.GetString("EventKey") != "2002nrg") ? $"Scouting event {PlayerPrefs.GetString("EventKey")}" : "Not currently scouting an event";
        if(!(PlayerPrefs.HasKey("Haptic"))) { PlayerPrefs.SetInt("Haptic", 1); }
    }

    public void pitScout()
    {
        HapticManager.LightFeedback();
        SceneManager.LoadScene(1);
    }
    public void objectiveScout()
    {
        HapticManager.LightFeedback();
        SceneManager.LoadScene(2);
    }

    public void subjectiveScout() 
    {
        HapticManager.LightFeedback();
        SceneManager.LoadScene(3);
    }
    public void settings()
    {
        SceneManager.LoadScene(4);
    }
    public void dataViewer()
    {
        SceneManager.LoadScene(5);
    }
   
}
