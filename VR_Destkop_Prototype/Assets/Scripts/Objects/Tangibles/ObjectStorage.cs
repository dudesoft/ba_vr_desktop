using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ObjectStorage : MonoBehaviour 
{
    private Dictionary<int, GameObject> storedObjects = new Dictionary<int, GameObject>();

    public void Store(GameObject gameObject)
    {
		int slot = FindFreeSlot(0);
		storedObjects.Add (slot, gameObject);
    }

	public Dictionary<int, GameObject> GetObjects() 
	{
		return storedObjects;
	}

	private int FindFreeSlot(int start) 
	{
		foreach(KeyValuePair<int, GameObject> entry in storedObjects)
		{
			if (entry.Key == start) 
			{
				start = entry.Key + 1;
			}
		}
		return start;
	}
}
