using UnityEngine;
using System.Collections;

using Pose = Thalmic.Myo.Pose;

public class CursorPositionScript : MonoBehaviour
{
    public float armLength;
    public GameObject handModel;

    GameObject myo;
    GameObject armReference;
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
