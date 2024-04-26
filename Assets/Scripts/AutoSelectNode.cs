using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class AutoSelectNode : MonoBehaviour, IPointerClickHandler
{
    public AutoSelectManager AutoSelectManager;
    public int OrderIndex = -1;
    public bool IsCurrent = false;
    public int NodeIdentifier;
    public Vector2 MapPosition;

    private GameObject ring;
    private GameObject label;
    private TMP_Text labelText;

    // Start is called before the first frame update
    void OnEnable()
    {
        ring = gameObject.transform.GetChild(1).gameObject;
        label = gameObject.transform.GetChild(2).gameObject;
        labelText = label.GetComponent<TMP_Text>();
        MapPosition = gameObject.GetComponent<RectTransform>().localPosition;

        UpdateLabelOrientation();
        UpdateColor();
    }

    public void UpdateLabel(int newLabel)
    {
        OrderIndex = newLabel;
        labelText.text = (OrderIndex == -1) ? "_" :  AutoSelectManager.Alphabet[OrderIndex].ToString();
    }

    public void Deselect()
    {
        UpdateLabel(-1);
        IsCurrent = false;
    }

    public void UpdateColor()
    {
        var color = IsCurrent ? AutoSelectManager.CurrentNodeColor : ((OrderIndex == -1) ? AutoSelectManager.UnselectedNodeColor : ((OrderIndex == 0) ? AutoSelectManager.StartingNodeColor : AutoSelectManager.DefaultNodeColor));
        ring.GetComponent<Image>().color = color;
        labelText.color = color;
    }

    public void UpdateLabelOrientation()
    {
        var labelRectTransform = label.GetComponent<RectTransform>().localScale;

        labelRectTransform.x = (AutoSelectManager.FieldMap.dataManager.subjectiveMatch.AllianceColor == "Red") ? 1 : -1;
        labelRectTransform.y = (PlayerPrefs.GetInt("FlipField", 0) == 1) ? -1 : 1;

        if (PlayerPrefs.GetInt("FlipField", 0) == 1) { labelRectTransform.x *= -1; }

        label.GetComponent<RectTransform>().localScale = labelRectTransform;
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (OrderIndex == -1)
        {
            HapticManager.LightFeedback();
            AutoSelectManager.AddNode(NodeIdentifier);
            UpdateLabel(AutoSelectManager.CurrentNodeIndex);
            UpdateColor();
        }
        else if (OrderIndex == AutoSelectManager.CurrentNodeIndex)
        {
            HapticManager.HeavyFeedback();
            AutoSelectManager.RemoveLastNode();
        }
        else
        {
            Debug.Log("Note already selected");
        }
    }
}
