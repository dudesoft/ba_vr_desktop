using UnityEngine;
using System.Collections.Generic;

using Pose = Thalmic.Myo.Pose;

public class MyoMapper : MonoBehaviour
{
	public GameObject myo;
	private ThalmicMyo tMyo;

	public Sprite moveIcon;
	public Sprite rotateIcon;

	public MyoMapping leftHand;
	public MyoMapping rightHand;

	private static MyoMapper instance;

	// Save Myo Mapping
	public MyoMapping handMapping;
	public Dictionary<Pose, Sprite> spriteMapping;

	public Pose rest = Pose.Rest;

	public static MyoMapper GetInstance()
	{
		if (!instance)
		{
			instance = (MyoMapper)FindObjectOfType(typeof(MyoMapper));
		}
		return instance;
	}

	void Awake()
	{
		tMyo = myo.GetComponent<ThalmicMyo>();
		tMyo.OnArmChanged += HandleOnArmChanged;

		// Right hand is used as default mapping
		handMapping = rightHand;
		buildSpriteMapping();
	}

	void HandleOnArmChanged(Thalmic.Myo.Arm arm)
	{
		handMapping = GetMyoMapping(arm);
		if (handMapping != null)
		{
			SpawnCursor();
			buildSpriteMapping();
		}
	}

	// Map poses to icons
	private void buildSpriteMapping()
	{
		spriteMapping = new Dictionary<Pose, Sprite>();
		spriteMapping.Add(handMapping.fist, handMapping.fistIcon);
		spriteMapping.Add(handMapping.doubleTap, handMapping.doubleTapIcon);
		spriteMapping.Add(handMapping.waveLeft, handMapping.waveLeftIcon);
		spriteMapping.Add(handMapping.waveRight, handMapping.waveRightIcon);
		spriteMapping.Add(handMapping.spread, handMapping.spreadIcon);
		spriteMapping.Add(handMapping.move, moveIcon);
		spriteMapping.Add(handMapping.rotate, rotateIcon);
	}

	public MyoMapping GetMyoMapping(Thalmic.Myo.Arm arm)
	{
		if (arm == Thalmic.Myo.Arm.Right)
		{
			return rightHand;
		}
		else if (arm == Thalmic.Myo.Arm.Left)
		{
			return leftHand;
		}
		else
		{
			Debug.Log("Could not determine on which arm the myo is worn! " +
				"Returning right hand.");
			return rightHand;
		}
	}

	public void SpawnCursor()
	{
		// Make sure only one cursor is instantiated at a time
		GameObject[] cursors = GameObject.FindGameObjectsWithTag(ApplicationConstants.Tags.CURSOR);
		if (cursors.Length > 0)
		{
			foreach (GameObject cursor in cursors)
			{
				Destroy(cursor);
			}
		}
		Instantiate(handMapping.cursorModel);
	}

	[System.Serializable]
	public class MyoMapping
	{
		public Sprite waveLeftIcon;
		public Sprite waveRightIcon;
		public Sprite fistIcon;
		public Sprite spreadIcon;
		public Sprite doubleTapIcon;
		public Pose waveLeft;
		public Pose waveRight;
		public Pose fist;
		public Pose spread;
		public Pose doubleTap;
		public Pose move = Pose.Move;
		public Pose rotate = Pose.Rotate;
		public GameObject cursorModel;
	}
}