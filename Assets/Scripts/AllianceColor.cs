using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllianceColor : MonoBehaviour
{
    private DataManager data;
    public Image image;

    // Start is called before the first frame update
    void Start()
    {
        data = gameObject.GetComponent<DataManager>();
        image = gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    public void UpdateColor()
    {
        if (data.match.AllianceColor == "red") {
            image.color = new Color32(222, 75, 62, 255);
        } else {
            image.color = new Color32(76, 159, 230, 255);
        }
    }
}
