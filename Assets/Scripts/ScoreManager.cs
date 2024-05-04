using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEngine.Rendering.DebugUI;

public class ScoreManager : MonoBehaviour
{
    public int streak = 0;

    public GameObject highScore;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("PracticeModeStreak")) { PlayerPrefs.SetInt("PracticeModeStreak", 0); }
        if (!PlayerPrefs.HasKey("CurrentStreak")) { PlayerPrefs.SetInt("CurrentStreak", 0); }

        setStreak(PlayerPrefs.GetInt("CurrentStreak"));

        updateHighScore();
        updateStreak();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void resetStreak()
    {
        setStreak(0);
        PlayerPrefs.SetInt("CurrentStreak", 0);
    }

    public void setStreak(int s)
    {
        streak = s;
        updateStreak();
    }

    public void incrementStreak(int i)
    {
        streak += i;
        PlayerPrefs.SetInt("CurrentStreak", streak);
        updateStreak();


        if (streak > PlayerPrefs.GetInt("PracticeModeStreak"))
        {
            PlayerPrefs.SetInt("PracticeModeStreak", streak);
            updateHighScore();
        }
    }

    public void incrementStreak()
    {
        incrementStreak(1);
    }

    public void updateHighScore()
    {
        gameObject.GetComponent<TMP_Text>().text = "HIGH SCORE: " + PlayerPrefs.GetInt("PracticeModeStreak").ToString();
    }

    public void updateStreak()
    {
        highScore.GetComponent<TMP_Text>().text = "STREAK: " + streak.ToString();
    }


}
