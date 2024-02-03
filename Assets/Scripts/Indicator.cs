using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;

public class Indicator : MonoBehaviour
{
    public GameObject panel;
    public HorizontalLayoutGroup panelLayout;
    public RectTransform panelDimensions;
    public GameObject scrollView;
    public RectTransform scrollViewDimensions;
    public float pageWidth;
    public float pageWidthNoSpacing;
    public int currentPage;
    // Start is called before the first frame update
    void Start()
    {
        scrollViewDimensions = scrollView.GetComponent<RectTransform>();
        panelLayout = panel.GetComponent<HorizontalLayoutGroup>();
        panelDimensions = panel.GetComponent<RectTransform>();
        pageWidth = scrollViewDimensions.rect.width + panelLayout.spacing;
        pageWidthNoSpacing = scrollViewDimensions.rect.width;
        currentPage = 122341;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentPage != (int)(-panelDimensions.localPosition.x / pageWidth))
        { 

            if (currentPage != 122341) { transform.GetChild(currentPage).GetChild(0).gameObject.SetActive(false); }
            currentPage = (int)(-panelDimensions.localPosition.x / pageWidth);
            transform.GetChild(currentPage).GetChild(0).gameObject.SetActive(true);
        }
        
    }
}
