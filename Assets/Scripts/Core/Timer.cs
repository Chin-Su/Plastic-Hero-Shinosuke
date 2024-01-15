using System;
using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private static Timer instance;

    public static Timer Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject singleton = new GameObject("Timer-Singleton");
                singleton.AddComponent<Timer>();
            }
            return instance;
        }
        private set { }
    }

    private void Awake()
    {
        if (instance != null && instance.GetInstanceID() != GetInstanceID())
            Destroy(gameObject);
        instance = this;
    }

    /// <summary>
    /// Used to handle action after time delay
    /// </summary>
    /// <param name="action">Action need to execute</param>
    /// <param name="timeDelay">Time delay</param>
    public void Schedule(Action action, float timeDelay)
    {
        StartCoroutine(DelayAction(action, timeDelay));
    }

    private IEnumerator DelayAction(Action action, float delay)
    {
        yield return new WaitForSeconds(delay);
        action?.Invoke();
    }
}