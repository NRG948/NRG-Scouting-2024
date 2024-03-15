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
    private readonly Color darkRed = new Color(1f, 0.435849f, 0.4986054f, 1f);
    private readonly Color darkYellow = new Color(1f, 0.8766348f, 0.2396226f, 1f);
    private readonly Color darkGrey = new Color(0.6269776f, 0.7433963f, 0.719469f, 1f);
    private readonly Color darkBlue = new Color(0.6588235f, 0.9276033f, 0.9921569f, 1f);
    private readonly Color white = new Color(1f, 1f, 1f, 1f);
    private readonly Color offWhite = new Color(0.95f, 0.95f, 0.95f, 1f);
    private readonly Color greyishWhite = new Color(0.85f, 0.85f, 0.85f, 1f);
    private readonly Color lightGrey = new Color(0.75f, 0.75f, 0.75f, 1f);
    private readonly Color darkBackground = new Color(0.04348518f, 0.03633317f, 0.07924521f, 1f);
    private readonly Color darkBackgroundBox = new Color(0.2215663f, 0.2329649f, 0.2603773f, 1f);
    private readonly Color offsetBackground = new Color(0.172f, 0.172f, 0.219f);
    private readonly Color transparentYellow = new Color(1f, 0.8766348f, 0.2396226f, 0.2f);
    private readonly Color transparentRed = new Color(1f, 0.435849f, 0.4986054f, 0.2f);

    private readonly Color hotPink = new Color(1f, 0.15686275f, 0.521156863f, 1f);
    private readonly Color darkPink = new Color(0.81568627f, 0.007874314f, 0.35686275f, 1f);
    private readonly Color lightPink = new Color(1f, 0.43137255f, 0.79215686f, 1f);
    private readonly Color pinkRed = new Color(1f, 0f, 0.11111111111f, 1f);
    private readonly Color pinkYellow = new Color(0.9725491f, 0.8588236f, 0.08235294f);
    private readonly Color pinkGrey = new Color(0.5176471f, 0.5529412f, 0.5568628f, 1f);
    private readonly Color pinkBlue = new Color(0.121885f, 0.4774481f, 0.7830189f, 1f);
    private readonly Color lighterPink = new Color(1f, 0.567f, 1f, 1f);
    private readonly Color red = new Color(1f, 0f, 0f, 1f);
    private readonly Color blue = new Color(0f, 0.48f, 1f, 1f);
    private readonly Color orange = new Color(1f, 0.7267137f, 0f, 1f);




    // Start is called before the first frame update
    void OnEnable() {
        darkMode = PlayerPrefs.GetInt("DarkMode",0) == 1;
        UpdateColor();
    }
    public void UpdateColor()
    {
        
        if (darkMode) {
            Debug.Log("Updating color!");
            if (SceneManager.GetActiveScene().name == "Mainmenu") { GameObject.Find("MainTitle").GetComponent<TextMeshProUGUI>().text = "DarkHouse"; }
            
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
            ColorAllWithTag("ContentText", white);
            ColorAllWithTag("TextOnColor", offWhite);
            ColorAllWithTag("InputField", darkGrey);
            ColorAllWithTag("SecondInputField", darkGrey);
            ColorAllWithTag("OffsetBackground", offsetBackground);

            ColorAllWithTag("PlaceholderYellowText", transparentYellow);
            ColorAllWithTag("PlaceholderRedText", transparentRed);
        }
        if (pinkMode)
        {
            if (SceneManager.GetActiveScene().name == "Mainmenu") { GameObject.FindGameObjectWithTag("Title").GetComponent<TextMeshProUGUI>().text = "PinkHouse"; }
            //Main menu
            ColorAllWithTag("HighlightRed", pinkRed);
            ColorAllWithTag("HighlightYellow", pinkYellow);
            ColorAllWithTag("HighlightGrey", pinkGrey);
            ColorAllWithTag("HighlightBlue", pinkBlue);

            ColorAllWithTag("HighlightRedBorder", pinkRed);
            ColorAllWithTag("HighlightYellowBorder", pinkYellow);
            ColorAllWithTag("HighlightGreyBorder", pinkGrey);
            ColorAllWithTag("HighlightBlueBorder", pinkBlue);

            ColorAllWithTag("TitleText", white);
            ColorAllWithTag("Title", white);
            ColorAllWithTag("Background", hotPink);
            ColorAllWithTag("BackgroundBox", lightPink);
            ColorAllWithTag("BackgroundDesign", darkPink);
            ColorAllWithTag("ContentText", white);
            ColorAllWithTag("InputField", lighterPink);
            ColorAllWithTag("SecondInputField", lighterPink);
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
