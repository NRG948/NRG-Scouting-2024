using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Windows;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool check(string input, string answer, float percentError)
    {
        input = input.ToLower();
        answer = answer.ToLower();

        string inp = removeUnnecessary(input);
        string ans = removeUnnecessary(answer);

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
}
