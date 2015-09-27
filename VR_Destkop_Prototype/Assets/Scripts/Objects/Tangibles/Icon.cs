using UnityEngine;
using System.Collections;
using System;

public class Icon : TangibleObject
{
	public override Renderer GetRenderer()
	{
		return GetComponent<Renderer> ();
	}

	public override void OnDeselect ()
	{
		SetEmission (Color.black);
	}

	public override void OnGrab ()
	{
	}

	public override void OnRelease ()
	{
	}

	public override void OnSelect ()
	{
		SetEmission (ApplicationConstants.HIGHLIGHTED);
	}
}


