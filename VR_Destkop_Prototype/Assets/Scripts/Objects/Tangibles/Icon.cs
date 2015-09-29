using UnityEngine;
using System.Collections;
using System;

public class Icon : TangibleObject
{

    public override void Start()
    {
        canContainObjects = false;
        base.Start();
    }

    public override Renderer GetRenderer()
	{
        GameObject meshHolder = transform.FindChild("MeshHolder").gameObject;
        return meshHolder.GetComponent<Renderer>();
    }

	public override void OnDeselect ()
	{
		SetEmission (Color.black);
	}

	public override void OnGrab ()
	{
	}

	public override void OnSelect ()
	{
		SetEmission (ApplicationConstants.HIGHLIGHTED);
	}

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}