using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsBoolHaptic : MonoBehaviour
{
    private Toggle toggle;
    // Start is called before the first frame update
    void Start()
    {
        toggle = gameObject.GetComponent<Toggle>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void doHapticStuff()
    {
        if (toggle.isOn)
        {
            HapticManager.LightFeedback();
        } else
        {
            HapticManager.HeavyFeedback();
        }
    }
}
