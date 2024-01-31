using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snap : MonoBehaviour
{
    public PageData pageData;
    public float nearestPageX;
    public float displacementX;
    public float snapSpeed;

    public float deadband;
    public RectTransform myRectTransfrom;
    public float myRelativePositionX;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!Input.GetMouseButton(0)) {
            nearestPageX = -pageData.currentPage * pageData.pageWidth - pageData.pageWidthNoSpacing / 2;
            displacementX = nearestPageX - myRectTransfrom.localPosition.x;

            if (Mathf.Abs(displacementX) >= deadband) {
                myRectTransfrom.localPosition += Vector3.right * Time.deltaTime * (Mathf.Sign(displacementX) * (snapSpeed + Mathf.Log(Mathf.Abs(displacementX)) * snapSpeed ));
            } else {
                myRectTransfrom.localPosition = new Vector3(nearestPageX, myRectTransfrom.localPosition.y, myRectTransfrom.localPosition.z);
            }
        }

    }
}
