using UnityEngine;
using System.Collections;

using Pose = Thalmic.Myo.Pose;



public class OpenBox : MonoBehaviour {

    GameObject myo;
    ThalmicMyo tMyo;

    void Start()
    {
        myo = GameObject.Find("Myo");
        tMyo = myo.GetComponent<ThalmicMyo>();
    }

    void OnTriggerEnter(Collider other)
    {
        GetComponent<Animator>().SetBool("IsOpen", true);
    }

    void OnTriggerExit(Collider other)
    {
        GetComponent<Animator>().SetBool("IsOpen", false);
    }

    void OnTriggerStay(Collider other)
    {
        if(tMyo.pose != Pose.Fist)
        {
            Destroy(other.gameObject);
            GetComponent<Animator>().SetBool("IsOpen", false);
        }
    }
}
