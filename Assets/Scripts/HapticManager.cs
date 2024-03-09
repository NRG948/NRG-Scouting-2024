using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CandyCoded.HapticFeedback;

public class HapticManager : MonoBehaviour
{
    // Start is called before the first frame update


    public static void LightFeedback() {
        if (!(SystemInfo.supportsVibration) || PlayerPrefs.GetInt("Haptic") == 0) { return; }
        Debug.Log("Light");
        HapticFeedback.LightFeedback();
    }

    public static void LightFeedback(string location)
    {
        if (!(SystemInfo.supportsVibration) || PlayerPrefs.GetInt("Haptic") == 0) { return; }
        Debug.Log("Light"+location);
        HapticFeedback.LightFeedback();
    }

    public static void HeavyFeedback()
    {
        if (!(SystemInfo.supportsVibration) || PlayerPrefs.GetInt("Haptic") == 0) { return; }
        Debug.Log("Heavy");
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
