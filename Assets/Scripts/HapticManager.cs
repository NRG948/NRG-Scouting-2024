using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CandyCoded.HapticFeedback;

public class HapticManager : MonoBehaviour
{
    // Start is called before the first frame update


    public static void LightFeedback()
    {
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

    /** 
     * <summary> Repeats HeavyFeedback indefinitely; must call StopCoroutine() to stop </summary>
     * <param name="timeInterval"> The time interval between repeats </param>
    */
    public static IEnumerator RepeatedHeavyFeedback(float timeInterval = 0.3f)
    {
        while (true)
        {
            HeavyFeedback();
            yield return new WaitForSeconds(timeInterval);
        }
    }

    /** 
     * <summary> Repeats LightFeedback indefinitely; must call StopCoroutine() to stop </summary>
     * <param name="timeInterval"> The time interval between repeats </param>
    */
    public static IEnumerator RepeatedLightFeedback(float timeInterval = 0.3f)
    {
        while (true)
        {
            LightFeedback();
            yield return new WaitForSeconds(timeInterval);
        }
    }

    /** 
     * <summary> Repeats LightFeedback <paramref name="x"/> times every <paramref name="timeInterval"/> seconds </summary>
     * <param name="x"> times LightFeedback repeats </param>
     * <param name="timeInterval"> time interval between repeats </param>
    */
    public static IEnumerator RepeatLightFeedback(int x = 1, float timeInterval = 0.3f)
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
    public static IEnumerator RepeatHeavyFeedback(int x = 1, float timeInterval = 0.3f)
    {
        for (int i = 0; i < x; i++)
        {
            HeavyFeedback();
            yield return new WaitForSeconds(timeInterval);
        }
    }

}
