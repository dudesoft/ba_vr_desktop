using UnityEngine;
using System.Collections;
using System;

public class CursorController : MonoBehaviour
{
    private GameObject armReference;
    private PoseManager poseManager;
    private Quaternion antiYaw;
    private MyoMapper myoMapper;
    private Animator animator;

    private static Renderer handRenderer;
    private static Transform self;

    // public to make changes in inspector
    public float armLength;
    public GameObject handModel;

    void Start()
    {
        self = transform;
        myoMapper = MyoMapper.GetInstance();
        poseManager = PoseManager.GetInstance();
        armReference = GameObject.FindWithTag(ApplicationConstants.Tags.ARM_REFERENCE);
        armLength = ApplicationConstants.DEFAULT_ARM_LENGTH;

        handRenderer = handModel.GetComponent<Renderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        UpdatePosition();
        HandleAnimations();
    }

    private void HandleAnimations()
    {
        if (poseManager.GetCurrentPose() == myoMapper.handMapping.fist)
        {
            animator.SetBool("IsClosed", true);
        }
        else
        {
            animator.SetBool("IsClosed", false);
        }
    }

    private void UpdatePosition()
    {
        Vector3 myoPosition = armReference.transform.position +
            armReference.transform.forward * armLength;
        Vector3 handFocusPoint = myoPosition - armReference.transform.forward;

        transform.position = myoPosition;
        transform.LookAt(handFocusPoint);
    }

    public static Vector3 GetCursorPosition()
    {
        return self.position;
    }

    public static void SetTransparency(float transparency)
    {
        foreach (Material material in handRenderer.materials)
        {
            material.color = new Color(material.color.r, material.color.g, material.color.b, transparency);
        }
    }
}