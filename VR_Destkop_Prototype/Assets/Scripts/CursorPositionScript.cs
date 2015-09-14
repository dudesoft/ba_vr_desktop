using UnityEngine;
using System.Collections;

using LockingPolicy = Thalmic.Myo.LockingPolicy;
using Pose = Thalmic.Myo.Pose;
using UnlockType = Thalmic.Myo.UnlockType;
using VibrationType = Thalmic.Myo.VibrationType;

public class CursorPositionScript : MonoBehaviour
{

    public float armLength = VRConstants.ARM_LENGTH;

    GameObject armReference;
    GameObject myo;
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
            animator.SetBool("IsClosed", true);
        }
        else
        {
            animator.SetBool("IsClosed", false);
        }
    }
}
