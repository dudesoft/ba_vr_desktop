using UnityEngine;
using System.Collections;

public class ResetArmPosition : MonoBehaviour
{
    Quaternion antiYaw = Quaternion.identity;
    GameObject myo;

    void Start()
    {
        myo = GameObject.Find("Myo");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            antiYaw = Quaternion.FromToRotation(
                new Vector3(myo.transform.forward.x, myo.transform.forward.y, myo.transform.forward.z),
                Camera.main.transform.forward);
        }

        transform.rotation = antiYaw * Quaternion.LookRotation(myo.transform.forward);
    }
}
