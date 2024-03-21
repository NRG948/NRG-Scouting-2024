using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour
{
    GameObject[] lives;

    private void Start()
    {
        lives = new GameObject[transform.childCount];
        for (int i = 0; i < lives.Length; i++)
        {
            lives[i] = transform.GetChild(i).GetComponent<GameObject>();
        }
    }

    public void AddLife()
    {
        //TODO: code this
    }

    public void RemoveLife()
    {
        //TODO: code this
    }
}
