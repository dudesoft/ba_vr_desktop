using UnityEngine;
using System.Collections;
using System;
using Pose = Thalmic.Myo.Pose;

public class MyoMapper : MonoBehaviour
{
	public GameObject myo;
	public MyoMapping leftHand;
	public MyoMapping rightHand;
	private static MyoMapper instance;

	public static MyoMapper GetInstance ()
	{
		if (!instance) {
			instance = (MyoMapper)GameObject.FindObjectOfType (typeof(MyoMapper));
		}
		return instance;
	}

	public MyoMapping GetMyoMapping ()
	{
		ThalmicMyo tMyo = myo.GetComponent<ThalmicMyo> ();
		if (tMyo.arm == Thalmic.Myo.Arm.Right) {
			Debug.Log ("R");
			return rightHand;
		} else if (tMyo.arm == Thalmic.Myo.Arm.Left) {
			Debug.Log ("L");
			return leftHand;
		} else {
			Debug.Log ("Could not determine which hand the myo is worn on!" +
				"Using right hand!");
			return rightHand;
		}
	}

	public void SpawnCursor()
	{
		// make sure only one cursor is instantiated at a time
		GameObject[] cursors = GameObject.FindGameObjectsWithTag (ApplicationConstants.Tags.CURSOR);
		if (cursors.Length > 0) {
			foreach(GameObject cursor in cursors) {
				Destroy(cursor);
			}
		}

		Instantiate<GameObject> (ApplicationConstants.HAND_MAPPING.cursorModel);
	}

	[System.Serializable]
	public class MyoMapping
	{
		public Sprite waveLeftIcon;
		public Sprite waveRightIcon;
		public Pose waveLeft;
		public Pose waveRight;
		public Pose fist;
		public Pose spread;
		public Pose doubleTap;
		public GameObject cursorModel;
	}
}