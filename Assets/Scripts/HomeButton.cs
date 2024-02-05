using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeButton : MonoBehaviour
{
    
    public void homeButton ()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu") { Application.Quit(); return; }
        SceneManager.LoadScene(0);
    }

}
