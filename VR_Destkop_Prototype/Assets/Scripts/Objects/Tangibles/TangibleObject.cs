using UnityEngine;
using System.Collections;
using System;

abstract public class TangibleObject : MonoBehaviour
{
    public bool selectable = true;
    public bool tangible = true;

    public bool canContainObjects = false;
    public GameObject hoveringOverContainer;

    public PoseManager poseManager;
    public MyoMapper myoMapper;

    public bool selected;
    public bool grabbed;
    private string description;

    // Distance to origin
    private float objectDistance;

    // Components
    private Renderer objectRenderer;

    abstract public void OnSelect();
    abstract public void OnDeselect();
    abstract public void OnGrab();

    abstract public Renderer GetRenderer();

    void Awake()
    {
        // Start the event listener
        SelectionManager.GetInstance().OnSelect += TriggerSelected;
        SelectionManager.GetInstance().OnDeselect += TriggerDeselected;
    }

    public virtual void Start()
    {
        poseManager = PoseManager.GetInstance();
        myoMapper = MyoMapper.GetInstance();
        objectRenderer = GetRenderer();
        objectDistance = ApplicationConstants.DEFAULT_OBJECT_DISTANCE;

        if (canContainObjects)
        {
            gameObject.AddComponent<ObjectStorage>();
        }
    }

    void Update()
    {
        if (selected)
        {
            CheckGrabbed();
        }

        SetLocationAndRotation();
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

    // react to events
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
            this.grabbed = true;
            OnGrab();
        }
        else
        {
            this.grabbed = false;
            OnRelease();
        }
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
    }

    public void SetAlpha(float value)
    {
        if (objectRenderer == null)
        {
            Debug.Log("No renderer attached to GameObject");
            return;
        }
        Color color = objectRenderer.material.color;
        color = new Color(color.r, color.g, color.b, value);    // Just set the alpha value of the color
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
            Debug.Log("Container has no component ObjectStorage!");
            return;
        }

        storage.Store(gameObject);
        gameObject.SetActive(false);
        // Have to call OnTriggerExit() manually when object inside gets disabled
        container.GetComponent<TangibleObject>().OnTriggerExit(null);
    }
}