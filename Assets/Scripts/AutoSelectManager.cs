using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;

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
        GameObject.Find("DataManager").GetComponent<DataManager>().SetString("AutoPickups", notes.ToCommaSeparatedString(),true);
    }

    public void removeLastNote() {
        if (notes.Count > 0) {
            HapticManager.HeavyFeedback();
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
        GameObject.Find("DataManager").GetComponent<DataManager>().SetString("AutoPickups", notes.ToCommaSeparatedString(), true);
    }

    public AutoNoteSelector getNote(int index) {
        return gameObject.transform.GetChild(index).gameObject.GetComponent<AutoNoteSelector>();
    }

    public int[] getAutoPath() {
        return notes.Cast<int>().ToArray();
    }
    
    public void UpdateAllLabelOrientations() {
        foreach (Transform child in transform) {
            child.gameObject.GetComponent<AutoNoteSelector>().UpdateLabelOrientation();
        }
    }
}
