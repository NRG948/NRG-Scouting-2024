using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class AutoNoteSelector : MonoBehaviour, IPointerClickHandler
{
    public AutoSelectManager manager;
    public int index = -1;
    public bool current = false;
    public int id;
    public Vector2 pos;

    private GameObject ring;
    private GameObject tag;
    private TMP_Text label;

    // Start is called before the first frame update
    void Start()
    {
        ring = gameObject.transform.GetChild(1).gameObject;
        tag = gameObject.transform.GetChild(2).gameObject;
        label = tag.GetComponent<TMP_Text>();
        pos = gameObject.GetComponent<RectTransform>().localPosition;

        var temp = tag.GetComponent<RectTransform>().localScale;

        if (manager.field.manager.match.AllianceColor == "Red") {
            temp.x = 1;
        } else {
            temp.x = -1;
        }

        if (PlayerPrefs.GetInt("FlipField", 0) == 1) {
            temp.y = -1;
            temp.x *= -1;
        } else {
            temp.y = 1;
        }

        tag.GetComponent<RectTransform>().localScale = temp;

        UpdateColor();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateNumber(int num) {
        index = num;
        label.text = (index == -1) ? "_" :  manager.alphabet[index].ToString();
    }

    public void Unselect() {
        UpdateNumber(-1);
        current = false;
    }

    public void UpdateColor() {
        var color = current ? manager.currentNoteColor : ((index == -1) ? manager.unselectedNoteColor : ((index == 0) ? manager.startingNoteColor : manager.defaultNoteColor));

        ring.GetComponent<Image>().color = color;
        label.color = color;
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (index == -1) {
            manager.addNote(id);
            UpdateNumber(manager.currentNoteIndex);
            UpdateColor();
        } else if (index == manager.currentNoteIndex) {
            manager.removeLastNote();
        } else {
            Debug.Log("Note already selected");
        }
    }
}
