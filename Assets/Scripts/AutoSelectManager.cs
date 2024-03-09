using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AutoSelectManager : MonoBehaviour
{
    public LineManager lineManager;
    public ArrayList notes = new ArrayList();
    public int currentNoteIndex = -1;
    public char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
    public Color currentNoteColor;
    public Color defaultNoteColor;
    public Color unselectedNoteColor;
    public Color startingNoteColor;
    public AutoFieldMap field;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addNote(int id) {
        //Unhighlighting old note, if there is one
        if (currentNoteIndex > -1) {
            var previousNote = getNote((int) notes[currentNoteIndex]);
            previousNote.current = false;
            previousNote.UpdateColor();
        }

        notes.Add(id);
        currentNoteIndex++;

        //Highlighting new current note
        var currentNote = getNote((int) notes[currentNoteIndex]);
        currentNote.current = true;
        currentNote.UpdateColor();

        lineManager.DrawLines();
    }

    public void removeLastNote() {
        if (notes.Count > 0) {
            var lastNote = getNote((int) notes[currentNoteIndex]);
            lastNote.Unselect();
            lastNote.UpdateColor();
            notes.RemoveAt(notes.Count - 1);
            currentNoteIndex--;

            //Highlighting new current note, if there is one
            if (currentNoteIndex > -1) {
                var currentNote = getNote((int) notes[currentNoteIndex]);
                currentNote.current = true;
                currentNote.UpdateColor();
            }
        } else {
            Debug.Log("No Path Selected");
        }

        lineManager.DrawLines();
    }

    public AutoNoteSelector getNote(int index) {
        return gameObject.transform.GetChild(index).gameObject.GetComponent<AutoNoteSelector>();
    }

    public int[] getAutoPath() {
        return notes.Cast<int>().ToArray();
    }
}
