using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{

   
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
