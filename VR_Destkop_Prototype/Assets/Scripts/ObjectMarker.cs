using UnityEngine;
using System.Collections;

public class ObjectMarker : MonoBehaviour
{
    GameObject selectedObject;
    GameObject cursor;
    RaycastHit hit;

    void Start()
    {
        cursor = GameObject.Find("Cursor Hand");
    }

    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.transform.gameObject == selectedObject) { 
                return;
            }
            selectedObject = hit.transform.gameObject;
            selectedObject.SendMessage("Select");
            cursor.SendMessage("SelectObject", selectedObject);
        }
        else
        {
            if (selectedObject != null)
            {
                selectedObject.SendMessage("Deselect");
                selectedObject = null;
                cursor.SendMessage("DeselectObject");
            }
        }
    }
}
