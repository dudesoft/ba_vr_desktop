using UnityEngine;
using System.Collections;
using System;

public class SelectionManager : MonoBehaviour
{
	public GameObject armReference;
	private Transform armTransform;

	// rotation-offset from myo transform
	private Vector3 myoDirection;

	// remember last selected object
	private GameObject selectedObject;
	private RaycastHit hit;

	// event handler for selection
	public delegate void OnSelectEvent (GameObject g);

	public event OnSelectEvent OnSelect;
	public event OnSelectEvent OnDeselect;

	private static SelectionManager instance;
	
	public static SelectionManager GetInstance ()
	{
		if (!instance) {
			instance = (SelectionManager)FindObjectOfType (typeof(SelectionManager));
		}
		return instance;
	}

	void Start ()
	{
		armTransform = armReference.transform;
	}

	void Update ()
	{
		HandleSelection ();
	}

	private void HandleSelection ()
	{
		if (Physics.Raycast (armTransform.position, armTransform.forward, out hit)) {
			if (selectedObject != null) {
				return;
			}
            CursorController.SetTransparency(0.5f);
			selectedObject = hit.transform.gameObject;
			OnSelect (hit.transform.gameObject);
		} else {
			if (selectedObject != null) {
                CursorController.SetTransparency(1f);
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
