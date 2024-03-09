using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class FieldSelect : MonoBehaviour, IPointerClickHandler
{
    public TouchSelect selector;
    public RectTransform myRectTransform;
    public DataManager manager;
    public string color = "Red";

    // Start is called before the first frame update
    void Start()
    {
        myRectTransform = GetComponent<RectTransform>();
        var temp = myRectTransform.localScale;

        temp.x = -1;

        if (PlayerPrefs.GetInt("FlipField",0) == 1) {
            temp.y = -1;
            if (color == "Red") {
                temp.x = 1;
            } else {
                temp.x = -1;
            }
        } else {
            temp.y = 1;
        }
        myRectTransform.localScale = temp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData) {
        selector.lockToMouse();
        HapticManager.LightFeedback();
    }

    public void changeColor() {
        if (color != manager.match.AllianceColor) {
            selector.resetCoords(manager); //Resets pointer position if alliance color switches
        }

        color = manager.match.AllianceColor;
        var temp = myRectTransform.localScale;

        if (color == "Red") {
            temp.x = -1;
        } else {
            temp.x = 1;
        }

        if (PlayerPrefs.GetInt("FlipField",0) == 1) {
            if (color == "Red") {
                temp.x = 1;
            } else {
                temp.x = -1;
            }
        }
        myRectTransform.localScale = temp;
    }
}
