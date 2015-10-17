using UnityEngine;
using System;

public class Trash : TangibleObject
{
	private OpenBox openBox;
	private bool isOpen;

	public override void Start()
	{
		EventManager.GetInstance ().MoveToTrash += ReceiveObject;
		deletable = false;
		canContainObjects = true;
		base.Start();
		openBox = GetComponent<OpenBox>();
	}
	
	public override void Update()
	{
		base.Update ();
		if (selected) {
			CheckOpenGesture ();
		}
	}
	
	private void CheckOpenGesture()
	{
		if (poseManager.GetCurrentPose() == myoMapper.handMapping.waveLeft && !isOpen)
		{
			isOpen = true;
			openBox.ShowGrid();
		}
		
		if (poseManager.GetCurrentPose() == myoMapper.handMapping.waveRight && isOpen)
		{
			isOpen = false;
			openBox.HideGrid();
		}
	}
	
	public override Renderer GetRenderer()
	{
		GameObject meshHolder = transform.FindChild("Trashbin").gameObject;
		return meshHolder.GetComponent<Renderer>();
	}
	
	public override void OnTriggerEnter(Collider other)
	{
		base.OnTriggerEnter(other);
		if (!selected && other.gameObject.tag == ApplicationConstants.Tags.TANGIBLE)
		{
			if (!other.gameObject.GetComponent<TangibleObject>().selected)
				GetComponent<Collider>().isTrigger = false;
		}
	}
	
	void OnCollisionExit(Collision collision)
	{
		// Avoid other objects to be pushed into trigger indirectly
		if (!selected)
		{
			GetComponent<Collider>().isTrigger = true;
		}
	}

	void ReceiveObject(GameObject go)
	{
		ObjectStorage storage = GetComponent<ObjectStorage> ();
		if (storage == null) 
		{
			Debug.Log ("No Storage attached to Object!");
			return;
		}

		storage.Store (go);
	}
	
	public void SpawnIcons()
	{
		
	}
	
	public override void OnTriggerExit(Collider other)
	{
		base.OnTriggerExit(other);
	}
	
	public override void OnGrab()
	{
		ShowActionIcons(GestureIconBuilder.BuildActionHolderSet(GestureIconBuilder.ActionHolderType.MOVE_ICON));
		GetComponent<Collider>().isTrigger = false;
	}
	
	public override void OnRelease()
	{
		GetComponent<Collider>().isTrigger = true;
		ShowActionIcons(GestureIconBuilder.BuildActionHolderSet(GestureIconBuilder.ActionHolderType.BASIC_BOX));
	}
	
	public override void OnSelect()
	{
		SetEmission(ApplicationConstants.HIGHLIGHTED);
		ShowActionIcons(GestureIconBuilder.BuildActionHolderSet(GestureIconBuilder.ActionHolderType.BASIC_BOX));
	}
	
	public override void OnDeselect()
	{
		SetEmission(Color.black);
		HideActionIcons();
	}
}