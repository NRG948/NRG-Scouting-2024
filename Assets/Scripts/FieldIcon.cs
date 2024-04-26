using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldIcon : MonoBehaviour
{
    public RectTransform IconRectTransform;
    public DataManager Manager;
    public string Color = "Red";

    // Start is called before the first frame update
    
    void Start()
    {
        var temp = IconRectTransform.localScale;

        temp.x = -1;

        if (PlayerPrefs.GetInt("FlipField",0) == 1) {
            temp.y = -1;
            if (Color == "Red") {
                temp.x = 1;
            } else {
                temp.x = -1;
            }
        } else {
            temp.y = 1;
        }

        IconRectTransform.localScale = temp;
    }

    public void changeColor() {
        Color = Manager.match.AllianceColor;
        var temp = IconRectTransform.localScale;

        if (Color == "Red") {
            temp.x = -1;
        } else {
            temp.x = 1;
        }

        if (PlayerPrefs.GetInt("FlipField",0) == 1) {
            if (Color == "Red") {
                temp.x = 1;
            } else {
                temp.x = -1;
            }
        }
        
        IconRectTransform.localScale = temp;
    }
}
