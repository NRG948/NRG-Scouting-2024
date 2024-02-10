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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeColor() {
        color = manager.match.AllianceColor;

        if (color == "Red") {
            myRectTransform.localScale = new Vector3(1, -1, 1);
        } else {
            myRectTransform.localScale = new Vector3(1, 1, 1);
        }
    }
}
