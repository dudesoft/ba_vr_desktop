using UnityEngine;
using System.Collections;

public class ApplicationConstants : MonoBehaviour
{
    // Distance of objects to camera
    public static readonly float DEFAULT_ARM_LENGTH = 10;
    public static readonly float DEFAULT_OBJECT_DISTANCE = 13;

    // Timers to trigger interaction
    public static readonly float ACTION_DELAY = 1f;

    // Different types of tangible objects
    public enum TangibleType { Icon, Box }

    // Class with tag names
    public class Tags
    {
        public static readonly string TANGIBLE = "Tangible";
		public static readonly string CURSOR = "Cursor";
		public static readonly string ARM_REFERENCE = "ArmReference";
    }
}