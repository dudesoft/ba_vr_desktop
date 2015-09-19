using UnityEngine;
using System.Collections;

using LockingPolicy = Thalmic.Myo.LockingPolicy;
using Pose = Thalmic.Myo.Pose;
using UnlockType = Thalmic.Myo.UnlockType;
using VibrationType = Thalmic.Myo.VibrationType;

public class ResetArmPosition : MonoBehaviour
{
    Quaternion antiYaw = Quaternion.identity;
    GameObject myo;
    ThalmicMyo tMyo;

    void Start()
    {
        myo = GameObject.Find("Myo");
        tMyo = myo.GetComponent<ThalmicMyo>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || tMyo.pose == Pose.DoubleTap)
        {
            antiYaw = Quaternion.FromToRotation(
                new Vector3(myo.transform.forward.x, myo.transform.forward.y, myo.transform.forward.z),
                Camera.main.transform.forward);
        }

        transform.rotation = antiYaw * Quaternion.LookRotation(myo.transform.forward);
    }
}
