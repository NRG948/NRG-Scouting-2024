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

        for (int i = 0; i < manager.AutoSelectNodes.Count - 1; i++) {
            var newLine = Instantiate(line);
            newLine.transform.SetParent(gameObject.transform, false);
            newLine.GetComponent<LineUIRender>().pos1 = manager.GetNode((int) manager.AutoSelectNodes[i]).MapPosition;
            newLine.GetComponent<LineUIRender>().pos2 = manager.GetNode((int) manager.AutoSelectNodes[i + 1]).MapPosition;
        }
    }
}
