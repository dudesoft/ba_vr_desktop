using System;
using UnityEngine;

public class Box : TangibleObject
{
	private Animator animator;

	public override void Start()
	{
		canContainObjects = true;
		base.Start();
		animator = GetComponent<Animator>();
	}

	public override void Update()
	{
		base.Update();
		if (selected) {
			CheckGestures();
		}
	}

	private void CheckGestures()
	{
		if (poseManager.GetCurrentPose() == myoMapper.handMapping.waveLeft && !isOpen && !isStored)
		{
			isOpen = true;
			deletable = false;
			animator.SetBool("IsOpen", true);
			GetComponentInChildren<GridController>().Open();
			ShowActionIcons(GestureIconBuilder.BuildActionHolderSet(GestureIconBuilder.ActionHolderType.OPEN_BOX));
		}

		if (poseManager.GetCurrentPose() == myoMapper.handMapping.waveRight)
		{
			if (isOpen) 
			{
				isOpen = false;
				deletable = true;
				animator.SetBool("IsOpen", false);
				GetComponentInChildren<GridController>().Close();
				ShowActionIcons(GestureIconBuilder.BuildActionHolderSet(GestureIconBuilder.ActionHolderType.BASIC_BOX));

				// avoid starting delete process instantly
				blockGesture = true;
			}
		}
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
			// Trash bin should not be put into Boxes
			if (other.gameObject.GetComponent<TangibleObject>().selected && other.gameObject.name != "Trash")
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
		if (!isOpen)
		{
			animator.SetBool("IsOpen", false);
		}
	}

	public override void OnGrab()
	{
		ShowActionIcons(GestureIconBuilder.BuildActionHolderSet(GestureIconBuilder.ActionHolderType.MOVE_ICON));
		GetComponent<Collider>().isTrigger = false;
	}

	public override void OnRelease()
	{
		base.OnRelease ();
		GetComponent<Collider>().isTrigger = true;
		HandleContainerActionIcons (GestureIconBuilder.ActionHolderType.BASIC_BOX);
	}

	public override void OnSelect()
	{
		SetEmission(ApplicationConstants.HIGHLIGHTED);
		if (isStored)
		{
			HandleContainerActionIcons(GestureIconBuilder.ActionHolderType.STORED_BOX);
		}
		else
		{
			HandleContainerActionIcons(GestureIconBuilder.ActionHolderType.BASIC_BOX);
		}
	}

	public override void OnDeselect()
	{
		SetEmission(Color.black);
		HideActionIcons();
	}
}