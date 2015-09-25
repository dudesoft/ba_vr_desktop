using UnityEngine;
using System.Collections;

public class CursorController : MonoBehaviour {

    public GameObject armReference;
    private PoseManager poseManager;

    void Start()
    {
        poseManager = PoseManager.GetInstance();
    }
}