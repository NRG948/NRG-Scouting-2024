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
    public GameObject robotIcon;
    public RectTransform fieldIcon;
    public float FIELD_WIDTH_METERS = 8.21f;
    public float FIELD_LENGTH_METERS = 16.54f;
    public float STARTING_ZONE_WIDTH_METERS = 1.934f;
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
        Debug.Log(getCoords());
        Debug.Log(getFieldRegion());
    }

    public void resetCoords(DataManager dataManager)
    {
        img.enabled = false;
        dataManager.SetString("StartPos","0,0");
    }

    public Vector2 getCoords()
    {
        float rawX = myRectTransform.anchoredPosition.x;
        float rawY = myRectTransform.anchoredPosition.y;

        float CORNER_Y = fieldRect.anchoredPosition.x - fieldRect.rect.height / 2;
        float CORNER_X = fieldRect.anchoredPosition.y - fieldRect.rect.width / 2;

        float CONVERSION_RATE = FIELD_WIDTH_METERS / fieldRect.rect.height;

        float rawDiffX = field.color == "Red" ^ PlayerPrefs.GetInt("FlipField", 0) == 1 ? fieldRect.anchoredPosition.y + fieldRect.rect.width / 2 - rawX : rawX - CORNER_X;
        float rawDiffY = PlayerPrefs.GetInt("FlipField", 0) == 1 ? fieldRect.anchoredPosition.x + fieldRect.rect.height / 2 - rawY : rawY - CORNER_Y;

        float diffX = rawDiffX * CONVERSION_RATE;
        float diffY = rawDiffY * CONVERSION_RATE;

        float iconX = fieldIcon.rect.width * rawDiffX / fieldRect.rect.width;
        float iconY = fieldIcon.rect.height * rawDiffY / fieldRect.rect.height;

        robotIcon.SetActive(true);

        robotIcon.GetComponent<RectTransform>().anchoredPosition = new Vector3(iconX, iconY);

        /* For debugging

        Debug.Log(
            "Raw Coords: " + rawX + ", " + rawY +
            "\nRaw Corner: " + CORNER_X + ", " + CORNER_Y +
            "\tRaw Field: " + fieldRect.anchoredPosition.x + ", " + fieldRect.anchoredPosition.y +
            "\tField Dimension: " + fieldRect.rect.height + ", " + fieldRect.rect.width
            );
        */

        return new Vector2(diffX, diffY);
    }

    public string getFieldRegion(Vector2 coords) {
        float x = coords.x;
        float y = coords.y;

        if (x >= 0.9 && InRange(y, 5f, 6f, true)) {
            return "Subwoofer Middle";
        } else if (x <= 0.9f && InRange(y, 3.5f, 5f, true)) {
            return "Wall Subwoofer Source";
        } else if (x <= 0.9f && y >= 6f) {
            return "Wall Subwoofer Amp";
        } else if (x >= 0.9f && InRange(y, 3.5f, 5f, true)) {
            return "Outer Subwoofer Source";
        } else if (x >= 0.9f && y >= 6f) {
            return "Outer Subwoofer Amp";
        } else if (y <= 3.5f) {
            return "Source";
        } else {
            return "Out Of Bounds";
        }
    }

    public string getFieldRegion() {
        return getFieldRegion(getCoords());
    }

    public bool InRange(float val, float min, float max, bool inclusive) {
        return inclusive ? val >= min && val <= max : val > min && val < max;
    }
}