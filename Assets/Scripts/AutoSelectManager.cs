using System.Collections;
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

    /// <summary>
    /// Adds node based on id to the path and sets it to the current node.
    /// </summary>
    /// <param name="id"></param> The id of the node.
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

    /// <summary>
    /// Removes last node in the path and sets the previous node to the current node.
    /// </summary>
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

    /// <summary>
    /// Returns the node script of the given index of the path.
    /// </summary>
    /// <param name="index"></param> Some index in the path of nodes.
    /// <returns></returns>
    public AutoSelectNode GetNode(int index)
    {
        return gameObject.transform.GetChild(index).gameObject.GetComponent<AutoSelectNode>();
    }

    /// <summary>
    /// Returns the path as an array of ids.
    /// </summary>
    /// <returns></returns> The path as an array of ids.
    public int[] GetAutoPath()
    {
        return AutoSelectNodes.Cast<int>().ToArray();
    }
    
    /// <summary>
    /// Updates orientations of the node labels based on alliance color and FlipField setting.
    /// </summary>
    public void UpdateAllLabelOrientations()
    {
        foreach (Transform node in transform)
        {
            node.gameObject.GetComponent<AutoSelectNode>().UpdateLabelOrientation();
        }
    }
}
