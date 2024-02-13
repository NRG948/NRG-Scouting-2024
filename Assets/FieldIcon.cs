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
        if (PlayerPrefs.GetInt("FlipField",0) == 1) {
            var temp = myRectTransform.localScale;
            temp.y = -1;
            myRectTransform.localScale = temp;
        } else {
            var temp = myRectTransform.localScale;
            temp.y = 1;
            myRectTransform.localScale = temp;
        }
        changeColor();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeColor() {
        color = manager.match.AllianceColor;

        if (color == "Red") {
            var temp = myRectTransform.localScale;
            temp.x = -1;
            myRectTransform.localScale = temp;
        } else {
            var temp = myRectTransform.localScale;
            temp.x = 1;
            myRectTransform.localScale = temp;
        }
    }
}
