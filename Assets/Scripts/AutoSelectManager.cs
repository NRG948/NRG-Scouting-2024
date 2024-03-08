using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSelectManager : MonoBehaviour
{
    public ArrayList notes = new ArrayList();
    public int currentNoteIndex = 0;
    public string[] alphabet = {"A", "B", "C", "D", "E", "F", "E", "G"};

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addNote(int id) {
        notes.Add(id);
    }

    public void removeNote() {
        if (notes.Count > 0) {
            notes.RemoveAt(notes.Count - 1);
            var lastNote = gameObject.transform.GetChild(notes[currentNoteIndex]).GameObject.GetComponent<AutoNoteSelector>();
            lastNote.Unselect();
            currentNoteIndex--;
        } else {
            Debug.Log("No Path Selected");
        }
    }
}
