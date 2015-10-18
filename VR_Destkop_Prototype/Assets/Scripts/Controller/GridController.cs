using UnityEngine;
using System.Collections.Generic;

public class GridController : MonoBehaviour
{
    // Get a Reference to the ParentObject
    private GameObject boxParent;

    private List<GameObject> storedObjects = new List<GameObject>();
    private List<GameObject> storageHolder = new List<GameObject>();

    void Awake()
    {
        FindStorageHolder();
        Transform parent = transform;

        // Find highest parent in GameObject's hierarchy
        while (boxParent == null)
        {
            if (parent.parent != null)
            {
                parent = parent.parent;
            }
            else
            {
                boxParent = parent.gameObject;
            }
        }
    }

    public void Open()
    {
        foreach(GameObject go in storedObjects)
        {
            go.SetActive(true);
        }
    }

    public void Close()
    {
        foreach (GameObject go in storedObjects)
        {
            go.SetActive(false);
        }
    }

    public void Store(GameObject go)
    {
        GameObject currentStorage = new GameObject();

        foreach (GameObject storage in storageHolder)
        {
            if (storage.transform.childCount == 0)
            {
                currentStorage = storage;
                break;
            }
        }
        if (currentStorage == null)
        {
            return;
        }

        go.transform.parent = currentStorage.transform;
        go.transform.position = currentStorage.transform.position;
        go.transform.rotation = currentStorage.transform.rotation;
        go.GetComponent<Rigidbody>().isKinematic = true;
        storedObjects.Add(go);
        if (!boxParent.GetComponent<TangibleObject>().isOpen)
        {
            go.SetActive(false);
        }
    }

    public void Remove(GameObject go)
    {
        go.GetComponent<Rigidbody>().isKinematic = false;
        storedObjects.Remove(go);
    }

    private void FindStorageHolder()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            storageHolder.Add(child);
        }
    }

    public void DisableGrid()
    {
        gameObject.SetActive(false);
    }
}
