using System;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public GameObject armReference;
    private Transform viewReference;
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

    // Event handler for look-direction
    public delegate void OnViewEvent(GameObject g);
    public event OnViewEvent OnStartLookingAt;
    public event OnViewEvent OnStopLookingAt;

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
        viewReference = Camera.main.transform;
        armTransform = armReference.transform;
    }

    void Update()
    {
        HandleSelection();
        HandleView();
    }

    private void HandleView()
    {
        if (Physics.Raycast(armTransform.position, armTransform.forward, out hit))
        {
            if (selectedObject != null)
            {
                return;
            }
            CursorController.SetTransparency(0.5f);
            selectedObject = hit.transform.gameObject;
            OnSelect(hit.transform.gameObject);
        }
        else
        {
            if (selectedObject != null)
            {
                CursorController.SetTransparency(1f);
                OnDeselect(selectedObject);
                selectedObject = null;
            }
        }
    }

    private void HandleSelection()
    {
        if (Physics.Raycast(viewReference.position, viewReference.forward, out hit))
        {
            if (lookedAtObject != null)
            {
                return;
            }
            lookedAtObject = hit.transform.gameObject;
            OnStartLookingAt(hit.transform.gameObject);
        }
        else
        {
            if (lookedAtObject != null)
            {
                OnStopLookingAt(lookedAtObject);
                lookedAtObject = null;
            }
        }
    }

    public void ResetSelectedObject()
    {
        selectedObject = null;
    }
}
