using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    // Singleton instance t o make the EventMaanger globally accessible
    public static EventManager Instance { get; private set; }

    public void Awake()
    {
        // Ensure that there is only one Eventmanager instance
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Dictionary to hold all listeners for each event type
    // Key is the event type ( for now lets assume its a string), and Value is a list of listeners
    private Dictionary<string, List<Action>> eventListeners = new Dictionary<string, List<Action>>();

    // Method for listeners to subscirbe to an event
    public void Subscribe(string eventType, Action listener)
    {
        // if there is no entry for this even type, create it
        if (!eventListeners.ContainsKey(eventType))
        {
            eventListeners[eventType] = new List<Action>();
        }

        // Add the listener to the lsit of this event type
        eventListeners[eventType].Add(listener);
    }

    // Method for listeners to unsubscribe to an event
    public void Unsubscribe(string eventType, Action listener)
    {
        // if there is an entry for this event type, remove the listener
        if (eventListeners.ContainsKey(eventType))
        {
            eventListeners[eventType].Remove(listener);
        }
    }

    // Method to raise an event, notifying all listeners for  that event
    public void RaiseEvent(string eventType)
    {
        // if there are listeners for this event, notify all of them
        if (eventListeners.ContainsKey(eventType))
        {
            Debug.Log("Event raised: " + eventType + " with " + eventListeners[eventType].Count + " listeners.");

            foreach (var listener in eventListeners[eventType])
            {
                listener.Invoke();
            }
        }
        else
        {
            Debug.Log("Event raised: " + eventType + " but no listeners found.");
        }
    }

    // Singleton Pattern: We are using the singleton pattern to ensure that there is only one instance of the EventManager and that it's easily accessible from anywhere in your code through EventManager.Instance.
    // eventListeners Dictionary: This dictionary holds lists of actions(listeners) for each event type.For now, the event type is just a string.
    // Subscribe Method: This allows listeners to subscribe to an event type.If there are no listeners for that event type yet, it will create a new list.
    // Unsubscribe Method: This allows listeners to unsubscribe from an event type.
    // RaiseEvent Method: This method is called when an event happens (like a card being played). It goes through all the listeners for that event type and calls them.
}
