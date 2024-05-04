using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using System;
using static TMPro.TMP_InputField;
using UnityEngine.Serialization;

public class GenericTimer : MonoBehaviour
{
    string mode = "inactive"; //inactive, decrease, increase
    float rawSecondsElapsed = 0f;
    float startTimerSeconds;
    public int displayMinutes;
    public int displaySeconds;
    public int displayMilliseconds;

    GameObject timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (mode != "inactive")
        {
            rawSecondsElapsed += Time.deltaTime;
            updateTimer();
        }
    }

    [ContextMenu("Pause Timer")]
    public void pauseTimer()
    {
        mode = "inactive";
    }

    [ContextMenu("Start Count Down")]
    public void startCountDown(float startTimeSeconds)
    {
        rawSecondsElapsed = 0f;
        startTimerSeconds = startTimeSeconds;
        mode = "decrease";
    }

    [ContextMenu("Start Count Down")]
    public void startCountDown()
    {
        startCountDown(rawSecondsElapsed);
    }

    [ContextMenu("Start Count Up")]
    public void startCountUp()
    {
        mode = "increase";
    }

    [ContextMenu("Zero Timer")]
    public void zeroTimer()
    {
        setTime(0f);
    }

    [ContextMenu("Reset Timer")]
    public void resetTimer()
    {
        zeroTimer();
        pauseTimer();
    }

    [ContextMenu("Set Time")]
    public void setTime(float secondsElapsed)
    {
        rawSecondsElapsed = secondsElapsed;
        updateTimer();
    }

    public string updateTimer()
    {
        float timeDifference = 0f;

        switch(mode)
        {
            case "inactive":
                timeDifference = rawSecondsElapsed; break;
            case "increase":
                timeDifference = rawSecondsElapsed; break;
            case "decrease":
                timeDifference = startTimerSeconds - rawSecondsElapsed; break;
        }

        displayMinutes = (int)timeDifference / 60;
        displaySeconds = (int)timeDifference - displayMinutes;
        displayMilliseconds = (int)((timeDifference - Mathf.Floor(timeDifference)) * 100f);

        string displayedTime = intToTwoDigit(displayMinutes) + ":" + intToTwoDigit(displaySeconds) + "." + intToTwoDigit(displayMilliseconds);
        timer.GetComponent<TMP_Text>().text = displayedTime;
        return displayedTime;
    }

    public string intToTwoDigit(int value)
    {
        string r = value.ToString();

        return new string('0', 2 - r.Length) + r;
    }
}
