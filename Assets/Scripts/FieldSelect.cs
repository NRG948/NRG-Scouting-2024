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
            selector.reset(); //Resets pointer position if alliance color switches
        }

        color = manager.match.AllianceColor;

        if (color == "Red") {
            myRectTransform.localScale = new Vector3(1, -1, 1);
        } else {
            myRectTransform.localScale = new Vector3(1, 1, 1);
        }
    }
}
