using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Runtime.CompilerServices;

public class PageData : MonoBehaviour
{
    // Start is called before the first frame update
    public string[] pageNames = {"Setup", "Auto", "Tele-Op", "Endgame"};
    public int currentPage = 0;
    //1970
    public GameObject panel;
    public HorizontalLayoutGroup panelLayout;
    public RectTransform panelDimensions;
    public GameObject scrollView;
    public RectTransform scrollViewDimensions;
    public float pageWidth;
    public float pageWidthNoSpacing;
    public TMP_InputField Team1;
    public TMP_InputField Team2;
    public TMP_InputField Team3;
    public TMP_Text txt;
    void Start()
    {
        scrollViewDimensions = scrollView.GetComponent<RectTransform>();
        panelLayout = panel.GetComponent<HorizontalLayoutGroup>();
        panelDimensions = panel.GetComponent<RectTransform>();
        pageWidth = scrollViewDimensions.rect.width + panelLayout.spacing;
        pageWidthNoSpacing = scrollViewDimensions.rect.width;
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "SubjectiveScout" || SceneManager.GetActiveScene().name == "V2SubjectiveScout")
        {
            pageNames = new string[] {"Setup","Early Game", Team1.text == "" ? "Team 1" : Team1.text,Team2.text == "" ? "Team 2" : Team2.text,Team3.text == "" ? "Team 3" : Team3.text,"Endgame"};
        }
        currentPage = (int) (-panelDimensions.localPosition.x / pageWidth);
        txt.text = pageNames[currentPage];
    }

    public void setPage(int page) {
        currentPage = page;
    }

}
