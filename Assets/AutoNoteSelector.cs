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
    public TextMeshPro label;
    public bool current;
    public int id;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateNumber(int num) {
        index = num;
        label.text = (index == -1) ? "-" :  id.ToString();
    }

    public void Unselect() {
        UpdateNumber(-1);
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        
    }
}
