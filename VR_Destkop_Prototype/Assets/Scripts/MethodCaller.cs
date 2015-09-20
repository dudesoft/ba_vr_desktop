using UnityEngine;
using System.Collections;

public class MethodCaller : MonoBehaviour {

    public GameObject receiver;

    public void Grab()
    {
        receiver.SendMessage("Grab");
    }

    private void Release()
    {
        receiver.SendMessage("Release");
    }

    public void Select()
    {
        receiver.SendMessage("Select");
    }

    public void Deselect()
    {
        receiver.SendMessage("Deselect");
    }
}
