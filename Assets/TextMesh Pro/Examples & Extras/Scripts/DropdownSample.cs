using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropdownSample : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private TextMeshProUGUI text = null;

    [SerializeField]
    private TMP_Dropdown dropdownWithoutPlaceholder = null;

    [SerializeField]
    private TMP_Dropdown dropdownWithPlaceholder = null;

    public void OnPointerClick(PointerEventData eventData)
    {
        HapticManager.LightFeedback();
    }

    public void OnButtonClick()
    {
        text.text = dropdownWithPlaceholder.value > -1 ? "Selected values:\n" + dropdownWithoutPlaceholder.value + " - " + dropdownWithPlaceholder.value : "Error: Please make a selection";
    }
}
