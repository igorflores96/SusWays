using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Game Event", menuName = "Scriptable Objects/Game Events/Player Game Event", order = 0)]
public class PlayerEvent : ScriptableObject
{
    [SerializeField] private List<string> ListenerNames; 
    [SerializeField] [TextArea(5, 10)] private List<string> ListenerUsageDescription; 
    private List<PlayerEventListener> _listeners = new List<PlayerEventListener>();

    public void LaunchEvent(PlayerInfo value)
    {
        for(int i = _listeners.Count - 1; i >= 0; i--)
            _listeners[i].OnEventRaised(value);
    }

    public void RegisterListener(PlayerEventListener listener) 
	{ _listeners.Add(listener); }

	public void UnregisterListener(PlayerEventListener listener)
	{ _listeners.Remove(listener); }
}
