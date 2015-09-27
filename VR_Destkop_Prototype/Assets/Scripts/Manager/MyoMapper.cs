using UnityEngine;
using System.Collections;
using System;
using Pose = Thalmic.Myo.Pose;

public class MyoMapper : MonoBehaviour
{
    public GameObject myo;
    private ThalmicMyo tMyo;

    public MyoMapping leftHand;
    public MyoMapping rightHand;

    private static MyoMapper instance;

    // Save Myo Mapping
    public MyoMapping handMapping;

    public static MyoMapper GetInstance()
    {
        if (!instance)
        {
            instance = (MyoMapper)GameObject.FindObjectOfType(typeof(MyoMapper));
        }
        return instance;
    }

    void Awake()
    {
        tMyo = myo.GetComponent<ThalmicMyo>();
        tMyo.OnArmChanged += HandleOnArmChanged;
        tMyo.OnSyncedChanged += HandleOnSyncedChanged;

        // Right hand is used as default mapping
        handMapping = rightHand;
    }

    void HandleOnSyncedChanged(bool synced)
    {

    }

    void HandleOnArmChanged(Thalmic.Myo.Arm arm)
    {
        handMapping = GetMyoMapping(arm);
        if (handMapping != null)
        {
            SpawnCursor();
        }
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
            Debug.Log("Could not determine which hand the myo is worn on! " +
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
        Instantiate<GameObject>(handMapping.cursorModel);
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
        public GameObject cursorModel;
    }
}