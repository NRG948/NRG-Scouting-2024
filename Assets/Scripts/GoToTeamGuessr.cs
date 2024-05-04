using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToTeamGuessr : MonoBehaviour
{
    int clickedNum = 0;

    public void EasterEgg()
    {
        clickedNum++;
        if (clickedNum>=3)
        {
            SceneManager.LoadScene("TeamGuesser");
        }
    }

}
