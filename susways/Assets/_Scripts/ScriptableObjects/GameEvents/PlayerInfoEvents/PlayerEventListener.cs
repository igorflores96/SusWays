using UnityEngine;
using UnityEngine.Events;

public class PlayerEventListener : MonoBehaviour
{
	public PlayerEvent Event;
	public UnityEvent <PlayerInfo>Response;

	private void OnEnable()
	{ Event.RegisterListener(this); }

	private void OnDisable()
	{ Event.UnregisterListener(this); }

	public void OnEventRaised(PlayerInfo value)
	{ Response.Invoke(value); }
}
