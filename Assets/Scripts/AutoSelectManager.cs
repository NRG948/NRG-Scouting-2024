using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;

public class AutoSelectManager : MonoBehaviour
{
    public LineManager LineManager;
    public ArrayList AutoSelectNodes = new ArrayList();
    public int CurrentNodeIndex = -1;
    public char[] Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
    public AutoSelectMap FieldMap;

    public Color CurrentNodeColor;
    public Color DefaultNodeColor;
    public Color UnselectedNodeColor;
    public Color StartingNodeColor;

    public void AddNode(int id)
    {
        //Unhighlighting old node, if there is one
        if (CurrentNodeIndex > -1)
        {
            var previousNode = GetNode((int) AutoSelectNodes[CurrentNodeIndex]);
            previousNode.IsCurrent = false;
            previousNode.UpdateColor();
        }

        AutoSelectNodes.Add(id);
        CurrentNodeIndex++;

        //Highlighting new current node
        var currentNode = GetNode((int) AutoSelectNodes[CurrentNodeIndex]);
        currentNode.IsCurrent = true;
        currentNode.UpdateColor();

        LineManager.DrawLines();
        GameObject.Find("DataManager").GetComponent<DataManager>().SetString("AutoPickups", AutoSelectNodes.ToCommaSeparatedString(),true);
    }

    public void RemoveLastNode()
    {
        if (AutoSelectNodes.Count > 0)
        {
            HapticManager.HeavyFeedback();

            var finalNode = GetNode((int) AutoSelectNodes[CurrentNodeIndex]);
            finalNode.Deselect();
            finalNode.UpdateColor();

            AutoSelectNodes.RemoveAt(AutoSelectNodes.Count - 1);
            CurrentNodeIndex--;

            //Highlighting new current node, if there is one
            if (CurrentNodeIndex > -1)
            {
                var currentNode = GetNode((int) AutoSelectNodes[CurrentNodeIndex]);
                currentNode.IsCurrent = true;
                currentNode.UpdateColor();
            }
        }
        else
        {
            Debug.Log("No Path Selected");
        }

        LineManager.DrawLines();
        GameObject.Find("DataManager").GetComponent<DataManager>().SetString("AutoPickups", AutoSelectNodes.ToCommaSeparatedString(), true);
    }

    public AutoSelectNode GetNode(int index)
    {
        return gameObject.transform.GetChild(index).gameObject.GetComponent<AutoSelectNode>();
    }

    public int[] GetAutoPath()
    {
        return AutoSelectNodes.Cast<int>().ToArray();
    }
    
    public void UpdateAllLabelOrientations()
    {
        foreach (Transform node in transform)
        {
            node.gameObject.GetComponent<AutoSelectNode>().UpdateLabelOrientation();
        }
    }
}
