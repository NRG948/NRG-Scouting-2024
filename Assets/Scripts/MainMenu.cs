using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;



public class MainMenu : MonoBehaviour
{
    
    void Start()
    {
        if (!(Directory.Exists($"{Application.persistentDataPath}/cache/teams"))) { 
            GameObject.Find("AlertBox").GetComponent<AlertBox>().outwardFacing("Team names have not been downloaded. Would you like to do so now? This requires an internet connection.|downloadMatches");
        }
    }

    public void pitScout()
    {
        SceneManager.LoadScene(1);
    }
    public void objectiveScout()
    {
        SceneManager.LoadScene(2);
    }

    public void subjectiveScout() 
    {
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
