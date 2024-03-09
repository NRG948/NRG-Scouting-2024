using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CandyCoded.HapticFeedback;

public class HapticManager : MonoBehaviour
{
    // Start is called before the first frame update


    public static void LightFeedback()
    {
        Debug.Log("Light");
        if (!(SystemInfo.supportsVibration) || PlayerPrefs.GetInt("Haptic") == 0) { return; }
        HapticFeedback.LightFeedback();
    }

    public static void LightFeedback(string location)
    {
        Debug.Log("Light" + location);
        if (!(SystemInfo.supportsVibration) || PlayerPrefs.GetInt("Haptic") == 0) { return; }
        HapticFeedback.LightFeedback();
    }

    public static void HeavyFeedback()
    {
        Debug.Log("Heavy");
        if (!(SystemInfo.supportsVibration) || PlayerPrefs.GetInt("Haptic") == 0) { return; }
        HapticFeedback.HeavyFeedback();
    }

    /** 
     * <summary> Repeats HeavyFeedback indefinitely; must call StopCoroutine() to stop </summary>
    */
    public static IEnumerator RepeatedHeavyFeedback()
    {
        while (true)
        {
            HeavyFeedback();
            yield return new WaitForSeconds(0.3f);
        }
    }

    /** 
     * <summary> Repeats LightFeedback indefinitely; must call StopCoroutine() to stop </summary>
    */
    public static IEnumerator RepeatedLightFeedback()
    {
        while (true)
        {
            LightFeedback();
            yield return new WaitForSeconds(0.3f);
        }
    }

    /** 
     * <summary> Repeats LightFeedback <paramref name="x"/> times every <paramref name="timeInterval"/> seconds </summary>
     * <param name="x"> times LightFeedback repeats </param>
     * <param name="timeInterval"> time interval between repeats </param>
    */
    public static IEnumerator RepeatedLightFeedback(int x = 1, float timeInterval = 0.3f)
    {
        for (int i = 0; i < x; i++)
        {
            LightFeedback();
            yield return new WaitForSeconds(timeInterval);
        }
    }

    /** 
    * <summary> Repeats HeavyFeedback <paramref name="x"/> times every <paramref name="timeInterval"/> seconds </summary>
    * <param name="x"> times HeavyFeedback repeats </param>
    * <param name="timeInterval"> time interval between repeats </param>
    */
    public static IEnumerator RepeatedHeavyFeedback(int x = 1, float timeInterval = 0.3f)
    {
        for (int i = 0; i < x; i++)
        {
            HeavyFeedback();
            yield return new WaitForSeconds(timeInterval);
        }
    }

}
