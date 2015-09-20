using UnityEngine;
using System.Collections;

public class ObjectMarker : MonoBehaviour
{
    private GameObject selectedObject;
    private RaycastHit hit;

    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (selectedObject != null) { 
                return;
            }
            selectedObject = hit.transform.gameObject;
            selectedObject.SendMessage("Select");
        }
        else
        {
            if (selectedObject != null)
            {
                selectedObject.SendMessage("Deselect");
                selectedObject = null;
            }
        }
    }
}
