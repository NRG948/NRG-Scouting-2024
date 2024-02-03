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
    public float startMousePositionX;
    public float scrollThreshold;
    public float breakThreshold;
    public int targetPage = 0;
    public bool hasTarget = false;
    public int lastPage;
    private float scrollInputDisplacement;
    public float ERRORDIFF;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            startMousePositionX = Input.mousePosition.x;
            lastPage = pageData.currentPage;
        } else if (Input.GetMouseButtonUp(0)) {
            scrollInputDisplacement = Input.mousePosition.x - startMousePositionX;
            targetPage
                = (Mathf.Abs(scrollInputDisplacement) >= breakThreshold) ? pageData.currentPage
                : (scrollInputDisplacement <= -scrollThreshold) ? lastPage + 1
                : (scrollInputDisplacement >= scrollThreshold) ? lastPage - 1
                : pageData.currentPage;
            hasTarget = true;
        }

        if (hasTarget && targetPage >= 0 && targetPage < pageData.pageNames.Length) {
            displacementX = -targetPage * pageData.pageWidth - pageData.pageWidthNoSpacing / 2 - myRectTransfrom.localPosition.x;
            if (Mathf.Abs(displacementX) >= deadband) {
                myRectTransfrom.localPosition += Vector3.right * Time.deltaTime * (Mathf.Sign(displacementX) * (snapSpeed + Mathf.Log(Mathf.Abs(displacementX)) * snapSpeed));
            } else {
                myRectTransfrom.localPosition = new Vector3(-targetPage * pageData.pageWidth - pageData.pageWidthNoSpacing / 2, myRectTransfrom.localPosition.y, myRectTransfrom.localPosition.z);
                hasTarget = false;
            }
        }

    }
}
