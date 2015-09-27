using UnityEngine;
using System.Collections;

public class ArmReferenceController : MonoBehaviour {

	private PoseManager poseManager;
	private Quaternion antiYaw;
	private MyoMapper myoMapper;

	void Start ()
	{
		poseManager = PoseManager.GetInstance ();
		myoMapper = MyoMapper.GetInstance ();

		antiYaw = poseManager.GetCurrentRotation();
	}
	
	void Update ()
	{
		CheckArmReset ();
	}

	void CheckArmReset ()
	{
		// Reset position of cursor
		if (Input.GetKeyDown(KeyCode.Space) || poseManager.GetCurrentPose()
		    == myoMapper.handMapping.doubleTap)
		{
			antiYaw = Quaternion.FromToRotation(poseManager.GetCurrentDirection(),
			                                    Camera.main.transform.forward);
		}
		
		transform.rotation = antiYaw * Quaternion.LookRotation(poseManager.GetCurrentDirection());
	}
}
