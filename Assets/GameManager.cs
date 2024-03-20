using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;
using TMPro;
using static SaveSystem;

public class GameManager : MonoBehaviour
{
    public string gamemode = "inactive"; //inactive, practice, marathon, sprint
    public string response = "";
    public string answer = "Jack in the Bot";
    public int question = 2910;
    public SimpleTeam[] teams;
    public SimpleTeam[] questionQueue;
    public int questionNumber = 0;
    public string eventKey = "2024wasno";

    public GameObject inputBox;
    public GameObject hintText;
    public GameObject hintOverlay;
    public GameObject questionText;
    public GameObject questionCorrectOverlay;
    public GameObject questionWrongOverlay;
    // Start is called before the first frame update
    void Start()
    {
        teams = SaveSystem.getEventTeams(eventKey);
        questionQueue = copyOf<SimpleTeam>(teams);
        Shuffle<SimpleTeam>(questionQueue);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void nextQuestion()
    {

    }

    public void setQuestion(int q, string a)
    {
        answer = a;
        question = q;
        questionText.GetComponent<TMP_Text>().text = q.ToString();
    }

    public void revealAnswer()
    {
        questionText.GetComponent<TMP_Text>().text = answer;
    }

    public void revealHint()
    {
        if (gamemode == "practice")
        {
            hintText.GetComponent<TMP_Text>().text = "First letter: " + answer.Substring(0, 1);
            hintOverlay.SetActive(true);
        } else
        {
            Debug.Log("Hint inaccessible: invalid gamemode");
        }
    }

    public void resetHint()
    {
        hintText.GetComponent<TMP_Text>().text = "HINT";
        hintOverlay.SetActive(false);
    }

    public void setResponse()
    {
        response = inputBox.GetComponent<InputField>().text;
    }

    public void checkAnswer()
    {
        float percentError = (gamemode == "sprint") ? 0.5f : 0.4f;
        if (check(response, answer, percentError)) {
            showCorrect();
            Invoke("nextQuestion", 0.5f);
        }
    }
    public void resetQuestionColor()
    {
        questionCorrectOverlay.SetActive(false);
        questionWrongOverlay.SetActive(false);
    }
    public void showCorrect()
    {
        questionText.GetComponent<TMP_Text>().text = "CORRECT";
        questionCorrectOverlay.SetActive(true);
    }
    
    public void showIncorrect()
    {
        questionWrongOverlay.SetActive(true);
    }

    public void skip()
    {
        revealAnswer();
        Invoke("nextQuestion", 2);
    }

    public bool check(string o, string a, float percentError)
    {
        o = o.ToLower();
        a = a.ToLower();

        string inp = removeUnnecessary(o);
        string ans = removeUnnecessary(a);

        int i = 0;
        int j = 0;

        int errorCount = 0;

        int minError = (int) (percentError * (inp.Length + ans.Length) / 2);

        while (i < inp.Length && j < ans.Length)
        {
            int iDiff = inp.Length - i;
            int jDiff = ans.Length - j;

            string iChar = inp.Substring(i, 1);
            string jChar = ans.Substring(j, 1);

            if (iChar == jChar)
            {
                i++;
                j++;
            }
            else
            {
                if (iDiff >= jDiff)
                {
                    i++;
                    errorCount++;
                }
                else
                {
                    j++;
                    errorCount++;
                }
            }

            if (errorCount >= minError) { return false; }
        }

        return true;
    }

    public string removeUnnecessary(string word)
    {
        string re = "";
        for (int i = 0; i < word.Length; i++)
        {
            if (word.Substring(i, 1) != " " &&
                (word.Substring(i, 1) != "s" ||
                    (i != word.Length - 1 && word.Substring(i, 1) != " ")))
            {
                re += word.Substring(i, 1);
            }
        }

        return re;
    }
    public void Shuffle<T>(T[] array)
    {
        int n = array.Length;
        while (n > 1)
        {
            n--;
            int k = UnityEngine.Random.Range(0, n);
            (array[k], array[n]) = (array[n], array[k]);
        }
    }

    public T[] copyOf<T>(T[] array)
    {
        T[] c = new T[array.Length];
        for (int i = 0; i < array.Length; i++)
        {
            c[i] = array[i];
        }
        return c;
    }
}
