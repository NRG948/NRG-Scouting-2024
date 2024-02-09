using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchSelect : MonoBehaviour
{
    public Image img;
    public FieldSelect field;
    public GameObject dataManObject;
    public RectTransform myRectTransform;
    public RectTransform fieldRect;
    private DataManager data;
    public float FIELD_WIDTH_METERS = 8.21f;
    public float FIELD_LENGTH_METERS = 16.54f;
    // Start is called before the first frame update
    void Start()
    {
        img.enabled = false;
        data = dataManObject.GetComponent<DataManager>();
        myRectTransform = GetComponent<RectTransform>();
        fieldRect = field.myRectTransform;
        data.SetString("StartPos", "0,0");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void lockToMouse()
    {
        img.enabled = true;
        transform.position = Input.mousePosition;
        Vector2 coords = getCoords();
        data.SetString("StartPos",coords.x + ", " + coords.y);
    }

    public void reset()
    {
        img.enabled = false;
        data.SetString("StartPos","0,0");
    }

    public Vector2 getCoords()
    {
        //color = data.match.AllianceColor;
        float rawX = myRectTransform.anchoredPosition.x;
        float rawY = myRectTransform.anchoredPosition.y;
        
        //height/width flipped because the entire field is turned sideways
        float CORNER_X = fieldRect.anchoredPosition.x - fieldRect.rect.height / 2;
        float CORNER_Y = fieldRect.anchoredPosition.y + fieldRect.rect.width / 2;

        float CONVERSION_RATE = FIELD_WIDTH_METERS / fieldRect.rect.height;

        float diffX = (rawX - CORNER_X) * CONVERSION_RATE;
        float diffY = (CORNER_Y - rawY) * CONVERSION_RATE;

        /*
        Debug.Log(
            "Raw Coords: " + rawX + ", " + rawY +
            "\nRaw Corner: " + CORNER_X + ", " + CORNER_Y +
            "\tRaw Field: " + fieldRect.anchoredPosition.x + ", " + fieldRect.anchoredPosition.y +
            "\tField Dimension: " + fieldRect.rect.height + ", " + fieldRect.rect.width
            );
        */

        //Adjusting based on alliance color
        if (field.color == "Red") {
            diffX = FIELD_WIDTH_METERS - diffX;
            diffY = FIELD_LENGTH_METERS - diffY;
        }

        return new Vector2(diffY, diffX); //returning with X and Y flipped because the field is turned sideways
    }
}