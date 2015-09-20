using UnityEngine;
using System.Collections;

using Pose = Thalmic.Myo.Pose;

public class IconController : MonoBehaviour
{
    public float objectDistance;
    public GameObject instructionHolder;

    private bool selected;
    private bool grabbed;
    private float maxEmission = 0.3f;
    private float minEmission = 0.01f;
    private float color, halo;
    private Animation instructionAnimation;
    private Light lighting;
    private Material material;
    private GameObject anker;
    private GameObject myo;
    private Transform self;
    private ThalmicMyo thalmicMyo;

    void Update()
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

        myo = GameObject.Find("Myo");
        thalmicMyo = myo.GetComponent<ThalmicMyo>();
        color = minEmission;
        anker = GameObject.Find("Cursor Hand");
        lighting = GetComponent<Light>();
        SetupLocationAndRotation();
        instructionAnimation = instructionHolder.GetComponent<Animation>();
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
                grabbed = true;
            } else
            {
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
