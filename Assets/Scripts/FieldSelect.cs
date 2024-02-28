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

    public void OnPointerClick(PointerEventData eventData) {
        selector.lockToMouse();
    }

    public void changeColor() {
        if (color != manager.match.AllianceColor) {
            selector.resetCoords(manager); //Resets pointer position if alliance color switches
        }

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
