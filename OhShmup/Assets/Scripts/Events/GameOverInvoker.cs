using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class GameOverInvoker : MonoBehaviour
{
    protected Dictionary<EventName, UnityEvent> unityEvents =
        new Dictionary<EventName, UnityEvent>();

    /// <summary>
    /// Add the given listener for the given event name
    /// </summary>
    /// <param name="eventName">Event name.</param>
    /// <param name="listener">Listener.</param>
    public void AddListener(EventName eventName, UnityAction listener)
    {
        UnityEvent unityEvent = null;
        if (!unityEvents.TryGetValue(eventName, out unityEvent))
        {
            unityEvent = AddEvent(eventName);
        }

        unityEvent.AddListener(listener);
    }


    /// <summary>
    /// Add the specified eventName.
    /// </summary>
    /// <param name="eventName">Event name.</param>
    public UnityEvent AddEvent(EventName eventName)
    {
        UnityEvent unityEvent = null;
        if (!unityEvents.TryGetValue(eventName, out unityEvent))
        {
            if (eventName == EventName.GameOverEvent)
            {
                unityEvent = new GameOver();
                unityEvents.Add(EventName.GameOverEvent, unityEvent);
            }
        }

        return unityEvent;
    }

    /// <summary>
    /// Invoke the specified eventName.
    /// </summary>
    /// <param name="eventName">Event name.</param>
    public void Invoke(EventName eventName)
    {
        UnityEvent unityEvent = null;
        if (unityEvents.TryGetValue(eventName, out unityEvent))
        {
            unityEvent.Invoke();
        }
    }

    /// <summary>
    /// Removes all listeners for the specified event name
    /// </summary>
    /// <param name="eventName">Event name.</param>
    public void RemoveAllListeners(EventName eventName)
    {
        UnityEvent unityEvent = null;
        if (unityEvents.TryGetValue(eventName, out unityEvent))
        {
            unityEvent.RemoveAllListeners();
        }
    }
}
