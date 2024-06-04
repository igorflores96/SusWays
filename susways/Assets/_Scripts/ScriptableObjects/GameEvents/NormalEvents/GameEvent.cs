using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Game Event", menuName = "Scriptable Objects/Game Events/Game Event", order = 0)]
public class GameEvent : ScriptableObject
{
    [SerializeField] private List<string> ListenerNames; 
    [SerializeField] [TextArea(5, 10)] private List<string> ListenerUsageDescription; 
    private List<GameEventListener> _listeners = new List<GameEventListener>();

    public void LaunchEvent()
    {
        for(int i = _listeners.Count - 1; i >= 0; i--)
            _listeners[i].OnEventRaised();
    }

    public void RegisterListener(GameEventListener listener) 
	{ _listeners.Add(listener); }

	public void UnregisterListener(GameEventListener listener)
	{ _listeners.Remove(listener); }
}
