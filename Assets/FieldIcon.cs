using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldIcon : MonoBehaviour
{
    public RectTransform myRectTransform;
    public DataManager manager;
    public string color = "Red";

    // Start is called before the first frame update
    
    void Start()
    {
        var temp = myRectTransform.localScale;

        if (PlayerPrefs.GetInt("FlipField",0) == 1) {
            temp.y = -1;
        } else {
            temp.y = 1;
        }

        temp.x = -1;

        myRectTransform.localScale = temp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeColor() {
        color = manager.match.AllianceColor;
        var temp = myRectTransform.localScale;

        if (color == "Red") {
            temp.x = -1;
        } else {
            temp.x = 1;
        }
        
        myRectTransform.localScale = temp;
    }
}
