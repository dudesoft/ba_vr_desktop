using UnityEngine;
using System.Collections;

public class ApplicationConstants : MonoBehaviour
{
    // Distance of objects to camera
    public static readonly float ARM_LENGTH = 10;
    public static readonly float OBJECT_DISTANCE = 13;

    // Timers to trigger interaction
    public static readonly float ACTION_DELAY = 1f;

    // Different types of tangible objects
    public enum TangibleType { Icon, Box }

    // Class with tag names
    public class Tags
    {
        public static readonly string TANGIBLE = "Tangible";
		public static readonly string CURSOR = "Cursor";
    }

	// Save Myo Mapping
	public static MyoMapper.MyoMapping HAND_MAPPING;

	void Start() 
	{
		MyoMapper myoMapper = MyoMapper.GetInstance ();
		HAND_MAPPING = myoMapper.GetMyoMapping ();
		myoMapper.SpawnCursor ();
	}
}