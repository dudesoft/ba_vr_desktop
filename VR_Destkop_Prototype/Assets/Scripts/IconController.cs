using UnityEngine;
using System.Collections;

using Pose = Thalmic.Myo.Pose;

public class IconController : MonoBehaviour
{
    public float objectDistance;
    public float gestureTime;
    public GameObject instructionHolder;

    private bool selected;
    private bool grabbed;
    private float maxEmission = 0.3f;
    private float minEmission = 0.01f;
    private float color, halo, gestureTimer;
    private Animation instructionAnimation;
    private Light lighting;
    private Material material;
    private GameObject anker;
    private GameObject myo;
    private GameObject waveLeft, waveRight, fist;
    private Transform self;
    private ThalmicMyo thalmicMyo;

    void Start()
    {
        AssignChildren();
        AssignRendererAndTransform();
        myo = GameObject.Find("Myo");
        anker = GameObject.Find("Cursor Hand");
        thalmicMyo = myo.GetComponent<ThalmicMyo>();
        color = minEmission;
        lighting = GetComponent<Light>();
        instructionAnimation = instructionHolder.GetComponent<Animation>();
    }

    void Update()
    {
        SetupLocationAndRotation();
        CheckGestures();
    }

    private void AssignChildren()
    {
        Transform child = transform.GetChild(0);

        int childCount = child.childCount;
        for (int i = 0; i < childCount - 1; i++)
        {
            Transform childChild = child.GetChild(i);

            switch (childChild.name)
            {
                case "Make Fist":
                    fist = childChild.gameObject;
                    break;
                case "Wave Left":
                    waveLeft = childChild.gameObject;
                    break;
                case "Wave Right":
                    waveRight = childChild.gameObject;
                    break;
            }
        }
    }

    private void AssignRendererAndTransform()
    {
        if (GetComponent<MeshRenderer>() != null)
        {
            material = GetComponent<MeshRenderer>().material;
        }
        else
        {
            material = GetComponent<SkinnedMeshRenderer>().material;
        }

        if (transform.parent != null)
        {
            self = transform.parent;
        }
        else
        {
            self = transform;
        }
    }

    private void SetupLocationAndRotation()
    {
        if (grabbed)
        {
            self.position = anker.transform.position;
        }
        self.position = (self.position - Camera.main.transform.position).normalized * objectDistance + Camera.main.transform.position;
        self.LookAt(Camera.main.transform);

        if (selected)
        {
            color = Mathf.Lerp(material.GetColor("_EmissionColor").r, maxEmission, 0.1f);
            halo = Mathf.Lerp(lighting.intensity, maxEmission, 0.1f);
            if (thalmicMyo.pose == Pose.Fist)
            {
                if (!grabbed)
                {
                    instructionAnimation.PlayQueued("HideGestureHolder", QueueMode.CompleteOthers);
                }
                grabbed = true;
            }
            else
            {
                if (grabbed)
                {
                    instructionAnimation.PlayQueued("ShowGestureHolder", QueueMode.CompleteOthers);
                }
                grabbed = false;
            }
        }
        else
        {
            color = Mathf.Lerp(material.GetColor("_EmissionColor").r, minEmission, 0.1f);
            halo = Mathf.Lerp(lighting.intensity, 0, 0.1f);
            grabbed = false;

            if (instructionHolder.activeSelf && !instructionAnimation.isPlaying)
            {
                instructionHolder.SetActive(false);
            }
        }
        lighting.intensity = halo;
        material.SetColor("_EmissionColor", new Color(color, color, color));
    }

    private void CheckGestures()
    {
        if(!instructionHolder.activeSelf)
        {
            return;
        }

        if (thalmicMyo.pose == Pose.WaveIn)
        {
            gestureTimer += Time.deltaTime;
            float progress = gestureTimer / gestureTime;
            if (!(progress > 1))
            {
                waveLeft.SendMessage("SetGestureProgress", progress);
            }
        }
        else if (thalmicMyo.pose == Pose.Rest)
        {
            gestureTimer = 0;
            waveLeft.SendMessage("SetGestureProgress", 0f);
        }

    }

    public void Select()
    {
        selected = true;
        instructionHolder.SetActive(true);
        instructionAnimation.PlayQueued("ShowGestureHolder", QueueMode.CompleteOthers);
    }

    public void Deselect()
    {
        selected = false;
        instructionAnimation.PlayQueued("HideGestureHolder", QueueMode.CompleteOthers);
    }
}
