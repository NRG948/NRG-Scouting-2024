using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ThemeManager : MonoBehaviour
{
    public bool darkMode;
    public bool pinkMode;
    private Color darkRed = new Color(1f, 0.435849f, 0.4986054f, 1f);
    private Color darkYellow = new Color(1f, 0.8766348f, 0.2396226f, 1f);
    private Color darkGrey = new Color(0.6269776f, 0.7433963f, 0.719469f, 1f);
    private Color darkBlue = new Color(0.6588235f, 0.9276033f, 0.9921569f, 1f);
    private Color white = new Color(1f, 1f, 1f, 1f);
    private Color darkBackground = new Color(0.04348518f, 0.03633317f, 0.07924521f, 1f);
    private Color darkBackgroundBox = new Color(0.2215663f, 0.2329649f, 0.2603773f, 1f);

    private Color hotPink = new Color(1f, 0.15686275f, 0.521156863f, 1f);
    private Color darkPink = new Color(0.81568627f, 0.007874314f, 0.35686275f, 1f);
    private Color lightPink = new Color(1f, 0.43137255f, 0.79215686f, 1f);
    

    // Start is called before the first frame update
    void Start()
    {
        if (darkMode) {
            if (SceneManager.GetActiveScene().name == "Mainmenu") { GameObject.FindGameObjectWithTag("Title").GetComponent<TextMeshProUGUI>().text = "DarkHouse"; }
            
            ColorAllWithTag("HighlightRed", darkRed);
            ColorAllWithTag("HighlightYellow", darkYellow);
            ColorAllWithTag("HighlightGrey", darkGrey);
            ColorAllWithTag("HighlightBlue", darkBlue);

            ColorAllWithTag("HighlightRedText", darkRed);
            ColorAllWithTag("HighlightYellowText", darkYellow);
            ColorAllWithTag("HighlightGreyText", darkGrey);
            ColorAllWithTag("HighlightBlueText", darkBlue);

            ColorAllWithTag("HighlightRedBorder", darkBackgroundBox);
            ColorAllWithTag("HighlightYellowBorder", darkBackgroundBox);
            ColorAllWithTag("HighlightGreyBorder", darkBackgroundBox);
            ColorAllWithTag("HighlightBlueBorder", darkBackgroundBox);

            ColorAllWithTag("TitleText", white);
            ColorAllWithTag("Title", white);
            ColorAllWithTag("Background", darkBackground);
            ColorAllWithTag("BackgroundBox", darkBackgroundBox);
            ColorAllWithTag("BackgroundDesign", darkBackground);
        }
        if (pinkMode)
        {
            if (SceneManager.GetActiveScene().name == "Mainmenu") { GameObject.FindGameObjectWithTag("Title").GetComponent<TextMeshProUGUI>().text = "PinkHouse"; }

            ColorAllWithTag("HighlightRed", darkRed);
            ColorAllWithTag("HighlightYellow", darkYellow);
            ColorAllWithTag("HighlightGrey", darkGrey);
            ColorAllWithTag("HighlightBlue", darkBlue);

            ColorAllWithTag("HighlightRedText", darkRed);
            ColorAllWithTag("HighlightYellowText", darkYellow);
            ColorAllWithTag("HighlightGreyText", darkGrey);
            ColorAllWithTag("HighlightBlueText", darkBlue);

            ColorAllWithTag("HighlightRedBorder", darkBackgroundBox);
            ColorAllWithTag("HighlightYellowBorder", darkBackgroundBox);
            ColorAllWithTag("HighlightGreyBorder", darkBackgroundBox);
            ColorAllWithTag("HighlightBlueBorder", darkBackgroundBox);

            ColorAllWithTag("TitleText", white);
            ColorAllWithTag("Title", white);
            ColorAllWithTag("Background", hotPink);
            ColorAllWithTag("BackgroundBox", lightPink);
            ColorAllWithTag("BackgroundDesign", darkPink);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ColorAllWithTag(string tag, Color color) {
        foreach (var obj in GameObject.FindGameObjectsWithTag(tag)) {
            if (obj.GetComponent<TextMeshProUGUI>() != null) {
                obj.GetComponent<TextMeshProUGUI>().color = color;
            } else if (obj.GetComponent<Image>() != null) {
                obj.GetComponent<Image>().color = color;
            } else if (obj.GetComponent<RawImage>() != null) {
                obj.GetComponent<RawImage>().color = color;
            }
        }  
    }
}
