using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour 
{
	public delegate void SetActionProgress(Sprite sprite, float progess);
	public event SetActionProgress SetProgress;

	public delegate void DeleteEvent(GameObject go);
	public event DeleteEvent MoveToTrash;

	private static EventManager instance;

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

	public static void SetLayerToAllChildren(GameObject obj, int newLayer)
	{
		if (null == obj)
		{
			return;
		}
		obj.layer = newLayer;
		
		foreach (Transform child in obj.transform)
		{
			// UI Layer should be kept
			if (null == child || obj.layer == ApplicationConstants.GUI_LAYER)
			{
				continue;
			}
			SetLayerToAllChildren(child.gameObject, newLayer);
		}
	}
}
