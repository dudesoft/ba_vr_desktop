using System;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public GameObject armReference;
    private Transform armTransform;

    // Rotation-offset from myo transform
    private Vector3 myoDirection;

    // Remember last selected object
    private GameObject selectedObject;
    private GameObject lookedAtObject;
    private RaycastHit hit;

    // Event handler for selection
    public delegate void OnSelectEvent(GameObject g);
    public event OnSelectEvent OnSelect;
    public event OnSelectEvent OnDeselect;

    private static SelectionManager instance;

    public static SelectionManager GetInstance()
    {
        if (!instance)
        {
            instance = (SelectionManager)FindObjectOfType(typeof(SelectionManager));
        }
        return instance;
    }

    void Start()
    {
        EventManager.GetInstance().MoveToTrash += RemoveObject;
        armTransform = armReference.transform;
    }

    void Update()
    {
        HandleSelection();
    }

    // Removed Objects must be cleared in Selection Manager to avoid NPE
    void RemoveObject(GameObject go)
    {
        if (go == selectedObject)
        {
            CursorController.SetTransparency(ApplicationConstants.ALPHA_DEFAULT);
            selectedObject = null;
        }
    }

    private void HandleSelection()
    {
        if (Physics.Raycast(armTransform.position, armTransform.forward, out hit))
        {
            if (selectedObject != null && !selectedObject.GetComponent<TangibleObject>().isStored && 
                hit.transform.tag == ApplicationConstants.Tags.TANGIBLE && hit.transform.gameObject.GetComponent<TangibleObject>().isStored)
            {
                return;
            }

            if (selectedObject != null && hit.transform.gameObject != selectedObject)
            {
                OnDeselect(selectedObject);
                selectedObject = hit.transform.gameObject;
                OnSelect(selectedObject);
            }

            if (selectedObject != null || hit.transform.tag != ApplicationConstants.Tags.TANGIBLE)
            {
                return;
            }
            CursorController.SetTransparency(ApplicationConstants.ALPHA_HALF_TRANSPARENT);
            selectedObject = hit.transform.gameObject;
            OnSelect(hit.transform.gameObject);
        }
        else
        {
            if (selectedObject != null)
            {
                CursorController.SetTransparency(ApplicationConstants.ALPHA_DEFAULT);
                OnDeselect(selectedObject);
                selectedObject = null;
            }
        }
    }

    public void ResetSelectedObject()
    {
        selectedObject = null;
    }
}
