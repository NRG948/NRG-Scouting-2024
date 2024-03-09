using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoFieldMap : MonoBehaviour
{
    public DataManager manager;

    // Start is called before the first frame update
    void Start()
    {
        var temp = GetComponent<RectTransform>().localScale;

        if (PlayerPrefs.GetInt("FlipField", 0) == 1) {
            temp.y = -1;
            temp.x = -1;
        } else {
            temp.x = 1;
            temp.y = 1;
        }

        GetComponent<RectTransform>().localScale = temp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateFieldOrientation() {
        var temp = GetComponent<RectTransform>().localScale;

        if (manager.subjectiveMatch.AllianceColor == "Red") {
            temp.x = 1;
        } else {
            temp.x = -1;
        }

        if (PlayerPrefs.GetInt("FlipField", 0) == 1) {
            temp.y = -1;
            temp.x *= -1;
        } else {
            temp.y = 1;
        }

        GetComponent<RectTransform>().localScale = temp;
    }
}
