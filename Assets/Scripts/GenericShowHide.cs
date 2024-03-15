using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericShowHide : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void show()
    {
        HapticManager.HeavyFeedback();
        transform.gameObject.SetActive(true);
    }
    public void hide()
    {
        HapticManager.LightFeedback();
        transform.gameObject.SetActive(false);  
    }
}
