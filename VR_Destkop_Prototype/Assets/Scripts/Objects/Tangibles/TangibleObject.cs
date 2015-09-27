using UnityEngine;
using System.Collections;
using System;

abstract public class TangibleObject : MonoBehaviour
{
	public bool selectable = true;
	public bool tangible = true;
	private bool selected;
	private bool grabbed;
	private string description;

	// Components
	private Renderer objectRenderer;

	abstract public void OnSelect ();

	abstract public void OnDeselect ();

	abstract public void OnGrab ();

	abstract public void OnRelease ();

	abstract public Renderer GetRenderer();

	void Awake ()
	{
		// start the event listener
		SelectionManager.GetInstance ().OnSelect += TriggerSelected;
		SelectionManager.GetInstance ().OnDeselect += TriggerDeselected;
	}

	void Start ()
	{
		objectRenderer = GetRenderer();
	}

	// react to events
	void TriggerSelected (GameObject go)
	{
		if (go != gameObject) {
			return;
		}
		SetSelected (true);
	}

	void TriggerDeselected (GameObject go)
	{
		if (go != gameObject) {
			return;
		}
		SetSelected (false);
	}

	public void SetSelected (bool selected)
	{
		if (!selectable) {
			return;
		}
		if (selected) {
			this.selected = true;
			OnSelect ();
		} else {
			this.selected = false;
			OnDeselect ();
		}
	}

	public void SetGrabbed (bool grabbed)
	{
		if (!tangible) {
			return;
		}
		if (grabbed) {
			this.grabbed = true;
			OnGrab ();
		} else {
			this.grabbed = false;
			OnRelease ();
		}
	}

	public void SetAlpha (float value)
	{
		if (objectRenderer == null) {
			Debug.Log ("No renderer attached to GameObject");
			return;
		}
		Color color = objectRenderer.material.color;
		color = new Color (color.r, color.g, color.b, value);	// Just set the alpha value of the color
	}

	public void SetEmission (Color value)
	{
		if (objectRenderer == null) {
			Debug.Log ("No renderer attached to GameObject");
			return;
		}
		objectRenderer.material.SetColor ("_EmissionColor", value);
	}
}