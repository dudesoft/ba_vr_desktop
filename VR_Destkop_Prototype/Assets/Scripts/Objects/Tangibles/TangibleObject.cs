using UnityEngine;
using System.Collections;
using System;

abstract public class TangibleObject : MonoBehaviour
{
    public bool selectable;
    public bool tangible;

    private bool selected;
    private bool grabbed;
    private string description;

    // Components
    private Renderer objectRenderer;

    abstract public void OnSelect();
    abstract public void OnDeselect();
    abstract public void OnGrab();
    abstract public void OnRelease();

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
    }

    public void SetSelected(bool selected)
    {
        if (!selectable)
        {
            return;
        }
        if (selected)
        {
            this.selected = true;
            OnSelect();
        }
        else
        {
            this.selected = false;
            OnDeselect();
        }
    }

    public void SetGrabbed(bool grabbed)
    {
        if (!tangible)
        {
            return;
        }
        if (grabbed)
        {
            this.grabbed = true;
            OnGrab();
        }
        else
        {
            this.grabbed = false;
            OnRelease();
        }
    }

    public void SetAlpha(float value, float speed)
    {
        if (objectRenderer == null)
        {
            Debug.Log("No renderer attached to GameObject");
            return;
        }
        Color color = objectRenderer.material.color;
        color = new Color(color.r, color.g, color.b, value);         // Just set the alpha value of the color
    }

    public void SetEmission(Color value, float speed)
    {
        if (objectRenderer == null)
        {
            Debug.Log("No renderer attached to GameObject");
            return;
        }
        objectRenderer.material.SetColor("_EmissionColor", value);
    }
}