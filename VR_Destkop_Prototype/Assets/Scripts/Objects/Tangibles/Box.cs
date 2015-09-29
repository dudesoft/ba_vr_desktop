using UnityEngine;
using System.Collections;
using System;

public class Box : TangibleObject
{
    private Animator animator;

    public override void Start()
    {
        canContainObjects = true;
        base.Start();
        animator = GetComponent<Animator>();
    }

    public override Renderer GetRenderer()
    {
        GameObject meshHolder = transform.FindChild("Cardboard").gameObject;
        return meshHolder.GetComponent<Renderer>();
    }

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (!selected && other.gameObject.tag == ApplicationConstants.Tags.TANGIBLE)
        {
            if (other.gameObject.GetComponent<TangibleObject>().selected)
            {
                animator.SetBool("IsOpen", true);
            }
            else
            {
                GetComponent<Collider>().isTrigger = false;
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // avoid other objects to be pushed into trigger indirectly
        if (!selected)
        {
            GetComponent<Collider>().isTrigger = true;
        }
    }

    public override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        animator.SetBool("IsOpen", false);
    }

    public override void OnDeselect()
    {
        SetEmission(Color.black);
    }

    public override void OnGrab()
    {
        GetComponent<Collider>().isTrigger = false;
    }

    public override void OnRelease()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    public override void OnSelect()
    {
        SetEmission(ApplicationConstants.HIGHLIGHTED);
    }
}