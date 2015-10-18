using UnityEngine;
using System.Collections.Generic;

public class ObjectStorage : MonoBehaviour
{
    public delegate void OnStoreEvent();
    public event OnStoreEvent OnStore;

    private Dictionary<int, GameObject> storedObjects = new Dictionary<int, GameObject>();

    public void Store(GameObject gameObject)
    {
        int slot = FindFreeSlot(0);
        storedObjects.Add(slot, gameObject);
        if (OnStore != null)
        {
            OnStore();
        }
    }

    public Dictionary<int, GameObject> GetObjects()
    {
        return storedObjects;
    }

    private int FindFreeSlot(int start)
    {
        foreach (KeyValuePair<int, GameObject> entry in storedObjects)
        {
            if (entry.Key == start)
            {
                start = entry.Key + 1;
            }
        }
        return start;
    }
}
