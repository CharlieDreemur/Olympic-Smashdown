using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

public class JsonEvent :  UnityEvent<string>{}
//Notes: Set the execution order of EventManager to -20 before everything starts.
public class EventManager : Singleton<EventManager>
{
    [ShowInInspector]
    [Tooltip("key is the event name, value is the listners that will be invoked when the event is invoked, support convery any type use Json")]
    private Dictionary<string, JsonEvent> _eventDictionary;
    public bool isPrintMessage = true;
    void Awake()
    {
        if (Instance._eventDictionary == null)
        {
            Instance._eventDictionary = new Dictionary<string, JsonEvent>();
        }
    }
    
    private void OnDisable() {
        foreach(var item in _eventDictionary){
            if(item.Value == null) return;
            item.Value.RemoveAllListeners();
        }
    }

    public static void AddListener(string eventName, UnityAction<string> listener){
        if(Instance == null){
            Debug.LogError("EventManager does not init. Try to use script execution order in project setting to make sure EventManager is init before other scripts");
            return;
        }
        JsonEvent thisEvent = null;
        if(Instance._eventDictionary.TryGetValue(eventName, out thisEvent)){
            thisEvent.AddListener(listener);
        }
        else{
            thisEvent = new JsonEvent();
            thisEvent.AddListener(listener);
            Instance._eventDictionary.Add(eventName, thisEvent);
        }
    }   

    public static void RemoveListener(string eventName, UnityAction<string> listener){
        if (Instance == null)
        {
            Debug.LogError("EventManager does not init. Try to use script execution order in project setting to make sure EventManager is init before other scripts");
            return;
        }

        if (!Instance.enabled)
        {
            Debug.LogError("EventManager disabled");
            return;
        }

        JsonEvent thisEvent = null;
        if(Instance._eventDictionary.TryGetValue(eventName, out thisEvent)){
            thisEvent.RemoveListener(listener);
        }
    }

    public static void Invoke(string eventName, string value){
        JsonEvent thisEvent = null;
        if(Instance._eventDictionary.TryGetValue(eventName, out thisEvent)){
            if(Instance.isPrintMessage){
                MessageManager.AddMessage($"Call [{eventName}]");
            }
            thisEvent.Invoke(value);
        }
        else{
            Debug.LogWarning($"The event: {eventName} does not exist in the EventManager, value: {value}");
        }
    }

    public static void RemoveAllListener(string listenerName)
    {
        if (Instance == null)
        {
            Debug.LogError("EventManager does not init. Try to use script execution order in project setting to make sure EventManager is init before other scripts");

            return;
        }

        if (!Instance.enabled)
        {
            Debug.LogError("EventManager disabled");
            return;
        }

        if (Instance._eventDictionary.TryGetValue(listenerName, out JsonEvent thisEvent))
        {
            thisEvent.RemoveAllListeners();
        }
    }
    
}
