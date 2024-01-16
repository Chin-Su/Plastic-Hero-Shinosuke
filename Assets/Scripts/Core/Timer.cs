using System;
using System.Collections;
using UnityEngine;

public class Timer
{
    private static MonoBehaviour timer;

    /// <summary>
    /// Used to handle action after time delay
    /// </summary>
    /// <param name="action">Action need to execute</param>
    /// <param name="timeDelay">Time delay</param>
    public static void Schedule(MonoBehaviour monoBehaviour, Action action, float timeDelay)
    {
        timer = monoBehaviour;
        if (timer != null)
            timer.StartCoroutine(DelayAction(action, timeDelay));
    }

    private static IEnumerator DelayAction(Action action, float delay)
    {
        yield return new WaitForSeconds(delay);
        action?.Invoke();
    }
}