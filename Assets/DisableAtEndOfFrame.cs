using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAtEndOfFrame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(disableAfterAWhile());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator disableAfterAWhile()
    {
        yield return new WaitForEndOfFrame();
        gameObject.SetActive(false);
    }
}
