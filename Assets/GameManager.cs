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
    //Game Statistics
    public string gamemode = "inactive"; //inactive, practice, marathon, sprint
    public string response = "";
    public string filteredReponse;
    public string answer;
    public string filteredAnswer;
    public int question;
    public SimpleTeam[] teams;
    public SimpleTeam[] questionQueue;
    public int questionNumber = -1;
    public string eventKey;
    public int currentErrorCount;
    public int currentErrorThreshold;

    //Input Timer
    public float timeBetweenKeystrokes = 0f;
    public float minTimeCheckingThreshold = 0.3f;
    public bool checkedCurrentAnswer = true;

    //Start Timer
    public float startTimeLeft = 3f;
    public bool isStarting = false;

    //Associated GameObjects
    public GameObject inputBox;
    public GameObject hintText;
    public GameObject hintOverlay;
    public GameObject questionText;
    public GameObject questionCorrectOverlay;
    public GameObject questionIncorrectOverlay;
    public GameObject startCountDown;
    public GameObject startButton;

    //Score
    public ScoreManager scoreManager;
    

    //Marathon GameObjects
    //public GameObject LifeContainer;
    //public LifeManager lifeManager;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("LastQuestion")) { PlayerPrefs.SetInt("LastQuestion", -1); }
        eventKey = PlayerPrefs.GetString("EventKey");
        lockInput();
    }

    // Update is called once per frame
    void Update()
    {
        if (gamemode != "inactive")
        {
            if (timeBetweenKeystrokes < minTimeCheckingThreshold)
            {
                timeBetweenKeystrokes += Time.deltaTime;
            }
            else if (!checkedCurrentAnswer)
            {
                Debug.Log("Answer checked");

                float percentError = (gamemode == "sprint") ? 0.4f : 0.3f;
                if (check(response, filteredAnswer, percentError))
                {
                    lockInput();
                    onCorrect();
                    scoreManager.incrementStreak();
                    PlayerPrefs.SetInt("LastQuestion", -1);
                    Invoke("nextQuestion", 0.5f);
                }
                checkedCurrentAnswer = true;
            }
        }
        else if (isStarting)
        {
            startCountDown.GetComponent<TMP_Text>().text = ((int)startTimeLeft).ToString();
            startTimeLeft -= Time.deltaTime;
        }
    }

    public void startGame()
    {
        gamemode = "practice";
        teams = SaveSystem.getEventTeams(eventKey);
        questionQueue = copyOf<SimpleTeam>(teams);
        Shuffle<SimpleTeam>(questionQueue);
        nextQuestion();

        if (PlayerPrefs.GetInt("LastQuestion") != -1)
        {
            setQuestion(PlayerPrefs.GetInt("LastQuestion"), findTeamNicknameByNumber(PlayerPrefs.GetInt("LastQuestion")));
            Debug.Log("overriden");

            resetHint();
            resetQuestionColor();
            inputBox.GetComponent<TMP_InputField>().ActivateInputField();
        }

        startButton.SetActive(false);
    }

    void nextQuestion()
    {
        questionNumber++;
        if (questionNumber >= questionQueue.Length)
        {
            Shuffle<SimpleTeam>(questionQueue);
            questionNumber = 0;
        }
        setQuestion(questionQueue[questionNumber].team_number, questionQueue[questionNumber].nickname);

        resetHint();
        resetQuestionColor();
        inputBox.GetComponent<TMP_InputField>().ActivateInputField();

        if (PlayerPrefs.GetInt("LastQuestion") == -1) { PlayerPrefs.SetInt("LastQuestion", question); }
    }

    public IEnumerator setQuestion(int q, string a, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        setQuestion(q, a);
    }

    public void setQuestion(int q, string a)
    {
        answer = a;
        filteredAnswer = removeUnnecessary(a.ToLower());
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
        }
        else
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
        response = inputBox.GetComponent<TMP_InputField>().text;
    }

    public void onInputChange()
    {
        setResponse();
        checkedCurrentAnswer = false;
        timeBetweenKeystrokes = 0;
    }
    public void resetQuestionColor()
    {
        questionCorrectOverlay.SetActive(false);
        questionIncorrectOverlay.SetActive(false);
    }
    public void onCorrect()
    {
        questionText.GetComponent<TMP_Text>().text = "CORRECT";
        questionCorrectOverlay.SetActive(true);
    }

    public void onIncorrect()
    {
        questionText.GetComponent<TMP_Text>().text = "INCORRECT";
        questionIncorrectOverlay.SetActive(true);
    }

    public void skip()
    {
        /**
        if (gamemode == "marathon")
        {
            if (!lifeManager.RemoveLife()) ;
            //TODO: code gameover...
        }
        **/
        if (gamemode != "inactive")
        {
            scoreManager.resetStreak();
            onIncorrect();
            lockInput();
            revealAnswer();
            Invoke("nextQuestion", 2);
        }
    }
    public void lockInput()
    {
        inputBox.GetComponent<TMP_InputField>().DeactivateInputField();
        inputBox.GetComponent<TMP_InputField>().text = "";
    }
    public void resetResponse()
    {
        response = "";
        filteredReponse = "";
    }

    public bool check(string inp, string ans, float percentError)
    {
        inp = removeUnnecessary(inp.ToLower());
        filteredReponse = inp;

        int i = 0;
        int j = 0;

        int errorCount = 0;

        int minError = (int)(percentError * 2f * Mathf.Sqrt(ans.Length));

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

            if (errorCount / 2 > minError) { return false; }
        }

        errorCount += 2 * Mathf.Abs(ans.Length - inp.Length);
        Debug.Log("diff text: " + 2 * Mathf.Abs(ans.Length - inp.Length));

        currentErrorCount = errorCount / 2;
        currentErrorThreshold = minError;


        if (errorCount / 2 > minError) { return false; }

        return true;
    }

    public string removeUnnecessary(string word)
    {
        string re = "";
        int parenthesesCount = 0;
        bool inQuotation = false;
        int indexBeforeTheEnd = 0;
        for (int i = 0; i < word.Length; i++)
        {
            string l = word.Substring(i, 1);
            if (inQuotation) { }
            else if (indexBeforeTheEnd > 0)
            {
                indexBeforeTheEnd--;
            }
            else if (i < word.Length - 2 && word.Substring(i, 3) == "the")
            {
                indexBeforeTheEnd = 2;
            }
            else if (l == "(")
            {
                parenthesesCount++;
            }
            else if (l == ")")
            {
                parenthesesCount--;
            }
            else if (l == "\"")
            {
                inQuotation = !inQuotation;
            }
            else if (parenthesesCount == 0 && l != " " && l != "-" && (l != "s" || (i != word.Length - 1 && l != " ")))
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
    
    public string findTeamNicknameByNumber(int number)
    {
        for (int i = 0; i < teams.Length; i++)
        {
            if (teams[i].team_number == number)
            {
                return teams[i].nickname;
            }
        }

        return "";
    }
}
