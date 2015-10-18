﻿using UnityEngine;
using System.Collections.Generic;
using System;

abstract public class TangibleObject : MonoBehaviour
{
    public bool selectable = true;
    public bool tangible = true;
    public bool deletable = true;

    public bool blockGesture;
    public bool canContainObjects = false;
    public GameObject hoveringOverContainer;

    public PoseManager poseManager;
    public MyoMapper myoMapper;
    public EventManager eventManager;

    public bool selected;
    public bool grabbed;
    public bool isStored;
    public bool isOpen;
    private string description;

    // Provide a counter for Tangibles
    public bool counterActive;
    public float counterValue;

    // Distance to origin
    public float objectDistance;

    // Components
    private Renderer objectRenderer;
    private GestureIconHolder iconHolder;

    abstract public void OnSelect();
    abstract public void OnDeselect();
    abstract public void OnGrab();

    abstract public Renderer GetRenderer();

    void Awake()
    {
        // Subscribe to events
        SelectionManager.GetInstance().OnSelect += TriggerSelected;
        SelectionManager.GetInstance().OnDeselect += TriggerDeselected;
    }

    public virtual void Start()
    {
        eventManager = EventManager.GetInstance();
        poseManager = PoseManager.GetInstance();
        myoMapper = MyoMapper.GetInstance();
        objectRenderer = GetRenderer();
        objectDistance = ApplicationConstants.DEFAULT_OBJECT_DISTANCE;
        iconHolder = GetComponentInChildren<GestureIconHolder>();

        if (canContainObjects)
        {
            gameObject.AddComponent<ObjectStorage>();
        }
    }

    public virtual void Update()
    {
        if (selected)
        {
            CheckGrabbed();
            if (deletable)
            {
                CheckDeleteGesture();
            }
        }

        if (!isStored)
        {
            SetLocationAndRotation();
        }

        if (counterActive)
        {
            counterValue += Time.deltaTime;
        }
        else
        {
            counterValue = 0;
        }
    }

    void CheckDeleteGesture()
    {
        if (poseManager.GetCurrentPose() == myoMapper.rest)
        {
            eventManager.SetTheProgress(myoMapper.spriteMapping[myoMapper.handMapping.waveRight], 0);
            counterActive = false;
            blockGesture = false;
        }

        if (poseManager.GetCurrentPose() == myoMapper.handMapping.waveRight && !blockGesture)
        {
            counterActive = true;
            float progress = counterValue / 2;
            if (progress >= 1)
            {
                eventManager.MoveToTheTrash(this.gameObject);
                gameObject.SetActive(false);
                return;
            }
            eventManager.SetTheProgress(myoMapper.spriteMapping[myoMapper.handMapping.waveRight], progress);
        }
    }

    public void ShowActionIcons(List<ActionHolder> actions)
    {
        iconHolder.ShowGestureIcons(actions);
    }

    public void HandleContainerActionIcons(GestureIconBuilder.ActionHolderType defaultAction)
    {
        if (isOpen)
        {
            ShowActionIcons(GestureIconBuilder.BuildActionHolderSet(GestureIconBuilder.ActionHolderType.OPEN_BOX));
        }
        else
        {
            ShowActionIcons(GestureIconBuilder.BuildActionHolderSet(defaultAction));
        }
    }

    public void HideActionIcons()
    {
        iconHolder.HideGestureIcons();
    }

    void SetLocationAndRotation()
    {
        transform.position = (transform.position - Camera.main.transform.position).normalized *
            objectDistance + Camera.main.transform.position;
        transform.LookAt(Camera.main.transform);
    }

    void CheckGrabbed()
    {
        if (!grabbed && poseManager.GetCurrentPose() == myoMapper.handMapping.fist)
        {
            SetGrabbed(true);
        }
        else if (grabbed && poseManager.GetCurrentPose() != myoMapper.handMapping.fist)
        {
            SetGrabbed(false);
        }

        if (grabbed)
        {
            transform.position = CursorController.GetCursorPosition();
        }
    }

    // React to events
    void TriggerSelected(GameObject go)
    {
        if (go != gameObject)
        {
            return;
        }
        SetSelected(true);
    }

    void TriggerDeselected(GameObject go)
    {
        if (go != gameObject)
        {
            return;
        }
        SetSelected(false);
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
            if (isStored)
            {
                FreeFromThemShackles();
            }
            this.grabbed = true;
            OnGrab();
        }
        else
        {
            this.grabbed = false;
            OnRelease();
        }
    }

    private void FreeFromThemShackles()
    {
        isStored = false;
        transform.parent = null;
        hoveringOverContainer.GetComponentInChildren<GridController>().Remove(gameObject);
        hoveringOverContainer = null;
    }

    public virtual void OnRelease()
    {
        if (hoveringOverContainer != null)
        {
            selected = false;
            grabbed = false;
            SelectionManager.GetInstance().ResetSelectedObject();
            StoreInto(hoveringOverContainer);
        }
        else if (isStored)
        {
            isStored = false;
        }
    }

    public void SetEmission(Color value)
    {
        if (objectRenderer == null)
        {
            Debug.Log("No renderer attached to GameObject");
            return;
        }
        objectRenderer.material.SetColor("_EmissionColor", value);
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (selected)
        {
            TangibleObject otherTangibleObject = other.gameObject.GetComponent<TangibleObject>();
            if (otherTangibleObject == null)
            {
                return;
            }

            if (otherTangibleObject.canContainObjects == true)
            {
                hoveringOverContainer = otherTangibleObject.gameObject;
            }
        }
    }

    public virtual void OnTriggerExit(Collider other)
    {
        // might be called manually from already disabled gameObject
        if (other == null)
        {
            return;
        }

        if (selected)
        {
            TangibleObject otherTangibleObject = other.gameObject.GetComponent<TangibleObject>();
            if (otherTangibleObject == null)
            {
                return;
            }
            if (otherTangibleObject.canContainObjects == true)
            {
                hoveringOverContainer = null;
            }
        }
    }

    public virtual void StoreInto(GameObject container)
    {
        ObjectStorage storage = container.GetComponent<ObjectStorage>();
        if (storage == null)
        {
            Debug.Log("Container has no Component ObjectStorage!");
            return;
        }

        isStored = true;
        SetSelected(false);
        container.GetComponentInChildren<GridController>().Store(gameObject);
        // Have to call OnTriggerExit() manually when object inside gets disabled
        container.GetComponent<TangibleObject>().OnTriggerExit(null);
    }
}