using System;
using System.Collections.Generic;
using UnityEngine;

public class EventDispatcher : MonoBehaviour
{
    #region Singleton

    private static EventDispatcher instance;

    public static EventDispatcher Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject objectSingleton = new GameObject("Singleton-EventDispatcher");
                objectSingleton.AddComponent<EventDispatcher>();
                Debug.Log("Create new envent dispatcher!");
            }
            return instance;
        }
        private set { }
    }

    #endregion Singleton

    private void Awake()
    {
        if (instance != null && instance.GetInstanceID() != this.GetInstanceID())
        {
            Destroy(gameObject);
            Debug.Log("Message: delete some instance of event dispatcher!");
        }
        else
            instance = this;
    }

    private void OnDestroy()
    {
        RemoveAllListener();
        instance = null;
    }

    #region Create a dictionary to store all listener

    private Dictionary<EventId, Action<object>> listener = new Dictionary<EventId, Action<object>>();

    #endregion Create a dictionary to store all listener

    #region Create some method to register, post, remove listener

    public void RegisterListener(EventId eventId, Action<object> callback)
    {
        // Check if event id not exist in listener
        if (!listener.ContainsKey(eventId))
        {
            listener.Add(eventId, null);
            Debug.Log("Message: add new envent id: " + eventId + " in listener!");
        }
        listener[eventId] += callback;
    }

    public void PostEvent(EventId eventId, object param)
    {
        if (!listener.ContainsKey(eventId))
        {
            Debug.LogError("Warning: " + eventId + " not exist in listener!");
            return;
        }

        var callback = listener[eventId];
        if (callback != null)
        {
            callback(param);
            Debug.Log("Message: execute action " + nameof(callback));
        }
        else
        {
            listener.Remove(eventId);
        }
    }

    public void RemoveListener(EventId eventId, Action<object> callback)
    {
        if (listener.ContainsKey(eventId))
        {
            listener[eventId] -= callback;
            Debug.Log("Message: remove action " + nameof(callback) + " from listener!");
        }
        else
        {
            Debug.LogError("Warning: " + eventId + " is not exist in listener!");
        }
    }

    public void RemoveAllListener()
    {
        listener.Clear();
    }

    #endregion Create some method to register, post, remove listener
}

#region Create extension class support use shortcut

public static class EventDispatcherExtention
{
    public static void RegisterListener(this MonoBehaviour listener, EventId evenId, Action<object> callback)
    {
        EventDispatcher.Instance.RegisterListener(evenId, callback);
    }

    public static void PostEvent(this MonoBehaviour listener, EventId eventId, object param = null)
    {
        EventDispatcher.Instance.PostEvent(eventId, param);
    }
}

#endregion Create extension class support use shortcut