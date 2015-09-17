using UnityEngine;
using System.Collections;

public class IconController : MonoBehaviour
{
    public float objectDistance;
    private GameObject anker;
    private bool grabbed;

    void Update()
    {
        anker = GameObject.Find("Cursor Hand");
        SetupLocationAndRotation();
    }

    private void SetupLocationAndRotation()
    {
        if (grabbed)
        {
            transform.position = anker.transform.position;
        }
        transform.position = (transform.position - Camera.main.transform.position).normalized * objectDistance + Camera.main.transform.position;
        transform.LookAt(Camera.main.transform);
    }

    public void Grab()
    {
        grabbed = true;
    }

    private void Release()
    {
        grabbed = false;
    }
}
