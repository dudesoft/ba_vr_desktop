using UnityEngine;
using System.Collections;

public class ApplicationConstants : MonoBehaviour
{
	// Switched Layers
	public static readonly int MASKED_OBJECTS_LAYER = 8;
	public static readonly int GUI_LAYER = 5;
	public static readonly int DEFAULT_LAYER = 0;

	// Distance of objects to camera
	public static readonly float DEFAULT_ARM_LENGTH = 15;
	public static readonly float DEFAULT_OBJECT_DISTANCE = 20;
	public static readonly float DISABLED_OBJECT_DISTANCE = 30;
	public static readonly float STORED_OBJECT_DISTANCE = 18;

	// Alpha values
	public static readonly float ALPHA_DEFAULT = 1f;
	public static readonly float ALPHA_HALF_TRANSPARENT = 0.5f;
	public static readonly float ALPHA_FULL_TRANSPARENT = 0f;

	// Timers to trigger interaction
	public static readonly float ACTION_DELAY = 1f;

	// Different types of tangible objects
	public enum TangibleType
	{
		Icon,
		Box,
		Trash
	}

	// Class with tag names
	public class Tags
	{
		public static readonly string TANGIBLE = "Tangible";
		public static readonly string CURSOR = "Cursor";
		public static readonly string ARM_REFERENCE = "ArmReference";
	}

	// Emission color for highlighting
	public static Color HIGHLIGHTED = new Color (0.2f, 0.2f, 0.2f);
}