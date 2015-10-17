using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour {

	private static EventManager instance;

	public delegate void SetActionProgress(Sprite sprite, float progess);
	public event SetActionProgress SetProgress;

	public delegate void DeleteEvent(GameObject go);
	public event DeleteEvent MoveToTrash;

	public static EventManager GetInstance()
	{
		if (!instance)
		{
			instance = (EventManager)FindObjectOfType(typeof(EventManager));
		}
		return instance;
	}

	public void SetTheProgress (Sprite sprite, float s)
	{
		SetProgress (sprite, s);
	}

	public void MoveToTheTrash (GameObject gameObject)
	{
		MoveToTrash (gameObject);
	}
}
