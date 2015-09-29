using UnityEngine;
using System.Collections;

using Pose = Thalmic.Myo.Pose;

public class ActionHolder
{
    public Pose action { get; set; }
    public string description { get; set; }

    public ActionHolder(Pose action, string description)
    {
        this.action = action;
        this.description = description;
    }
}