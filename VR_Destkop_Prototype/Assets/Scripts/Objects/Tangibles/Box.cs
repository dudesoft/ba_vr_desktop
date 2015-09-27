using UnityEngine;
using System.Collections;
using System;

public class Box : TangibleObject
{
	public override Renderer GetRenderer()
	{
		GameObject meshHolder = transform.FindChild ("Cardboard").gameObject;
		return meshHolder.GetComponent<Renderer> ();
	}

    public override void OnDeselect()
    {
		SetEmission (new Color (0, 0, 0));
	}

    public override void OnGrab()
    {
        throw new NotImplementedException();
    }

    public override void OnRelease()
    {
        throw new NotImplementedException();
    }

    public override void OnSelect()
    {
		SetEmission (new Color (100, 100, 100));
	}
}