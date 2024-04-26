using UnityEngine;

public class AutoSelectMap : MonoBehaviour
{
    public DataManager dataManager;

    // Start is called before the first frame update
    void Start()
    {
        // Orientation rotates halfway to account for stand location
        var autoMapRectTransform = GetComponent<RectTransform>().localScale;
        autoMapRectTransform.x = autoMapRectTransform.y = (PlayerPrefs.GetInt("FlipField", 0) == 1) ? -1 : 1;
        GetComponent<RectTransform>().localScale = autoMapRectTransform;
    }

    public void UpdateFieldOrientation()
    {
        bool isFlipFieldOn = PlayerPrefs.GetInt("FlipField", 0) == 1;
        bool isAllianceColorRed = dataManager.subjectiveMatch.AllianceColor == "Red";

        // Accounts for stand location and alliance color through rotation and reflection

        var autoMapTransform = GetComponent<RectTransform>().localScale;

        autoMapTransform.x = isAllianceColorRed ? 1 : -1;
        autoMapTransform.y = isFlipFieldOn ? -1 : 1;
        if (isFlipFieldOn) { autoMapTransform.x *= -1; }

        GetComponent<RectTransform>().localScale = autoMapTransform;
    }
}
