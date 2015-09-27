using UnityEngine;
using System.Collections;

public class CursorController : MonoBehaviour {
	
    private GameObject armReference;
    private PoseManager poseManager;
	private Quaternion antiYaw;

	// public to make changes in inspector
	public float armLength;

    void Start()
    {
        poseManager = PoseManager.GetInstance();
		armReference = GameObject.FindWithTag (ApplicationConstants.Tags.ARM_REFERENCE);

		armLength = ApplicationConstants.DEFAULT_ARM_LENGTH;
    }

	void Update()
	{
		UpdatePosition ();
	}

	void UpdatePosition ()
	{		
		Vector3 myoPosition = armReference.transform.position +
			armReference.transform.forward * armLength;
		Vector3 handFocusPoint = myoPosition - armReference.transform.forward;
		
		transform.position = myoPosition;
		transform.LookAt(handFocusPoint);
	}
}