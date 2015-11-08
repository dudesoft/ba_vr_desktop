using UnityEngine;

public class PoseManager : MonoBehaviour {

    public GameObject myoHolder;
    private ThalmicMyo myo;

    private static PoseManager instance;

    public static PoseManager GetInstance()
    {
        if (!instance)
        {
            instance = (PoseManager)FindObjectOfType(typeof(PoseManager));
        }
        return instance;
    }

    void Start()
    {
        myo = myoHolder.GetComponent<ThalmicMyo>();
    }

    public Thalmic.Myo.Pose GetCurrentPose()
    {
        return myo.pose;
    }

    public Quaternion GetCurrentRotation()
    {
        return myoHolder.transform.rotation;
    }

    public Vector3 GetCurrentDirection()
    {
        return myoHolder.transform.forward;
    }
}