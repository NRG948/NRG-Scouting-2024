using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineUIRender : MonoBehaviour
{
    public Vector3 pos1;
    public Vector3 pos2;
    public Color color;

    private RectTransform lineTransform;
    // Start is called before the first frame update
    void Start()
    {
        lineTransform = gameObject.GetComponent<RectTransform>();
        gameObject.GetComponent<Image>().color = color;

        //Credit: TheDarkVoice on Unity Forums, comments by me
        Vector3 temp;
        if (pos1.x > pos2.x)
        {
            temp = pos1;
            pos1 = pos2;
            pos2 = temp;
        }

        lineTransform.localPosition = (pos1 + pos2) / 2; //Set coords to middle point
        Vector3 dif = pos2 - pos1;
        lineTransform.sizeDelta = new Vector3(dif.magnitude, 16); //Sets size of rect to match distance
        lineTransform.localRotation = Quaternion.Euler(new Vector3(0, 0, 180 * Mathf.Atan(dif.y / dif.x) / Mathf.PI)); //Rotates the rect accordingly
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
