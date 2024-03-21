using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour
{
    GameObject[] lives;
    int liveCount = 3;

    private void Start()
    {
        lives = new GameObject[transform.GetChild(1).childCount];
        for (int i = 0; i < lives.Length; i++)
        {
            lives[i] = transform.GetChild(i).gameObject;
        }
    }

    /**
     * <summary>returns true if life is added</summary>
     */
    public bool AddLife()
    {
        if (liveCount >= 3) return false;
        else
        {
            lives[liveCount].SetActive(true);
            liveCount++;
            return true;
        }
    }

    /**
     * <summary>returns true if life is removed</summary>
     */
    public bool RemoveLife()
    {
        if (liveCount <= 0) return false;
        else
        {
            lives[liveCount - 1].SetActive(false);
            liveCount--;
            return true;
        }
    }
}
