using UnityEngine;
using System.Collections;

using Pose = Thalmic.Myo.Pose;

public class ApplicationConstants
{
    // Distance of objects to camera
    public static readonly float ARM_LENGTH = 10;
    public static readonly float OBJECT_DISTANCE = 13;

    // Defining all default poses
    public class DefaultPose
    {
        public static readonly Pose GRAB = Pose.Fist;
        public static readonly Pose LAUNCH = Pose.WaveIn;
        public static readonly Pose EDIT = Pose.WaveOut;
        public static readonly Pose RESET = Pose.DoubleTap;
    }

    // Timers to trigger interaction
    public static readonly float ACTION_DELAY = 1f;

    // Different types of tangible objects
    public enum TangibleType { Icon, Box }

    // Class with tag names
    public class Tags
    {
        public static readonly string TANGIBLE = "Tangible";
    }
}