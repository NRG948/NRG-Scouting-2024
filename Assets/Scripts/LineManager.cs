using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineManager : MonoBehaviour
{
    public AutoSelectManager manager;
    public GameObject line;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DrawLines() {
        foreach(Transform oldLine in gameObject.transform)
        {
            Destroy(oldLine.gameObject);
        }

        for (int i = 0; i < manager.notes.Count - 1; i++) {
            var newLine = Instantiate(line);
            newLine.transform.SetParent(gameObject.transform, false);
            newLine.GetComponent<LineUIRender>().pos1 = manager.getNote((int) manager.notes[i]).pos;
            newLine.GetComponent<LineUIRender>().pos2 = manager.getNote((int) manager.notes[i + 1]).pos;
        }
    }
}
