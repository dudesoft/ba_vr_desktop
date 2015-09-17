using UnityEngine;
using System.Collections;

using LockingPolicy = Thalmic.Myo.LockingPolicy;
using Pose = Thalmic.Myo.Pose;
using UnlockType = Thalmic.Myo.UnlockType;
using VibrationType = Thalmic.Myo.VibrationType;

public class CursorPositionScript : MonoBehaviour
{
    public float armLength;
    public GameObject handModel;

    GameObject myo;
    GameObject armReference;
    GameObject selectedObject;
    ThalmicMyo thalmicMyo;
    Animator animator;

    void Start()
    {
        myo = GameObject.Find("Myo");
        armReference = GameObject.Find("Arm Reference");
        thalmicMyo = myo.GetComponent<ThalmicMyo>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Vector3 myoPosition = armReference.transform.position +
            armReference.transform.forward * armLength;
        Vector3 handFocusPoint = myoPosition - armReference.transform.forward;

        transform.position = myoPosition;
        transform.LookAt(handFocusPoint);

        if (thalmicMyo.pose == Pose.Fist)
        {
            if (selectedObject != null)
            {
                animator.SetBool("IsClosed", true);
                selectedObject.SendMessage("Grab");
            }
        }
        else
        {
            if (selectedObject != null)
            {
                animator.SetBool("IsClosed", false);
                selectedObject.SendMessage("Release");
            }
        }
    }

    void SelectObject(GameObject gameObject)
    {
        selectedObject = gameObject;
        handModel.SendMessage("SetTransparency", 0.5f);
    }

    void DeselectObject()
    {
        selectedObject = null;
        handModel.SendMessage("SetTransparency", 1f);
    }
}
