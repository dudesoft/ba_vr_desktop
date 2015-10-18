using UnityEngine;
using System.Collections.Generic;

public class GridController : MonoBehaviour
{
    // Get a Reference to the ParentObject
    private GameObject boxParent;
    private TangibleObject tangible;

    // Handle interaction with Colliding Game Objects
    private List<GameObject> colliders = new List<GameObject>();
    private List<GameObject> storageHolder = new List<GameObject>();

    void Start()
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
                tangible = boxParent.GetComponent<TangibleObject>();
            }
        }

        boxParent.GetComponent<ObjectStorage>().OnStore += OnStore;
    }

    private void OnStore()
    {
        if (tangible.isOpen)
        {
            SpawnIcons();
        }
    }

    private void FindStorageHolder()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            for (int x = 0; x < child.transform.childCount; x++)
            {
                GameObject childOfChild = child.transform.GetChild(x).gameObject;
                storageHolder.Add(childOfChild);
            }
        }
    }

    void Update()
    {
        foreach (GameObject go in colliders)
        {
            if (tangible.isOpen)
            {
                go.SetActive(true);
            }

            TangibleObject to = go.GetComponent<TangibleObject>();
            if (to.isStored)
            {
                to.selectable = true;
                return;
            }
            to.selectable = false;
            EventManager.SetLayerToAllChildren(go, ApplicationConstants.MASKED_OBJECTS_LAYER);
        }
    }

    public void SpawnIcons()
    {
        Box box = boxParent.GetComponent<Box>();
        Dictionary<int, GameObject> storedObjects = boxParent.GetComponent<ObjectStorage>().GetObjects();

        if (box == null)
        {
            return;
        }
        Debug.Log("issssssccaaaaalled!!!");
        foreach (KeyValuePair<int, GameObject> entry in storedObjects)
        {
            GameObject currentObject = entry.Value;
            currentObject.SetActive(true);
            currentObject.transform.position = storageHolder[entry.Key].transform.position;
            currentObject.transform.rotation = storageHolder[entry.Key].transform.rotation;
            currentObject.transform.parent = storageHolder[entry.Key].transform;
            currentObject.layer = ApplicationConstants.DEFAULT_LAYER;
            currentObject.GetComponent<TangibleObject>().objectDistance = ApplicationConstants.STORED_OBJECT_DISTANCE;
            currentObject.GetComponent<TangibleObject>().SetSelected(false);
            currentObject.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    public void DisableGrid()
    {
        gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider col)
    {
        TangibleObject to = col.gameObject.GetComponent<TangibleObject>();
        if (to == null)
        {
            return;
        }

        if (to.grabbed)
        {
            to.hoveringOverContainer = boxParent;
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject == boxParent || colliders.Contains(col.gameObject) ||
            col.gameObject.GetComponent<TangibleObject>() == null)
        {
            return;
        }
        colliders.Add(col.gameObject);
    }

    void OnTriggerExit(Collider col)
    {
        if (colliders.Contains(col.gameObject))
        {
            TangibleObject to = col.gameObject.GetComponent<TangibleObject>();
            to.selectable = true;
            EventManager.SetLayerToAllChildren(col.gameObject, ApplicationConstants.DEFAULT_LAYER);
            colliders.Remove(col.gameObject);

            if (to.grabbed)
            {
                to.hoveringOverContainer = null;
            }
        }
    }
}
