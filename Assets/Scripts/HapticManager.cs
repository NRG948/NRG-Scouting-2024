using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CandyCoded.HapticFeedback;

public class HapticManager : MonoBehaviour
{
    // Start is called before the first frame update


    public static void LightFeedback() {
        Debug.Log("Light");
        if (!(SystemInfo.supportsVibration) || PlayerPrefs.GetInt("Haptic") == 0) { return; }
        HapticFeedback.LightFeedback();
        
    }
    public static void HeavyFeedback()
    {
        Debug.Log("Heavy");
        if (!(SystemInfo.supportsVibration) || PlayerPrefs.GetInt("Haptic") == 0) { return; }
        HapticFeedback.HeavyFeedback();
    }

}
