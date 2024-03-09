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

    public static void LightFeedback(string location)
    {
        Debug.Log("Light"+location);
        if (!(SystemInfo.supportsVibration) || PlayerPrefs.GetInt("Haptic") == 0) { return; }
        HapticFeedback.LightFeedback();
    }

    public static void HeavyFeedback()
    {
        Debug.Log("Heavy");
        if (!(SystemInfo.supportsVibration) || PlayerPrefs.GetInt("Haptic") == 0) { return; }
        HapticFeedback.HeavyFeedback();
    }

    /// <summary>
    /// Repeats HeavyFeedback indefinetly; must call StopCoroutine() to stop
    /// </summary>
    public static IEnumerator RepeatedHeavyFeedback()
    {
        while (true)
        {
            HeavyFeedback();
            yield return new WaitForSeconds(0.3f);
        }
    }

    /// <summary>
    /// Repeats LightFeedback indefinetly; must call StopCoroutine() to stop
    /// </summary>
    public static IEnumerator RepeatedLightFeedback()
    {
        while (true)
        {
            LightFeedback();
            yield return new WaitForSeconds(0.3f);
        }
    }

}
