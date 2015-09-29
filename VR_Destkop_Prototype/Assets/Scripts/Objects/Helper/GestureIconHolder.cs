using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Pose = Thalmic.Myo.Pose;

public class GestureIconHolder : MonoBehaviour
{
    public GameObject[] iconHolder;

    private List<GameObject> filledIconHolder;

    void Start()
    {
        ShowGestureIcons(new List<ActionHolder> { new ActionHolder(Pose.Fist, "yolo"), new ActionHolder(Pose.Fist, "yolo") });
    }

    public void ShowGestureIcons(List<ActionHolder> actions)
    {
        MyoMapper myoMapper = MyoMapper.GetInstance();
        filledIconHolder = new List<GameObject>();

        // build up the right views for given amount of actions
        switch (actions.Count)
        {
            case 1:
                filledIconHolder.Add(iconHolder[2]);
                break;
            case 2:
                filledIconHolder.Add(iconHolder[1]);
                filledIconHolder.Add(iconHolder[3]);
                break;
            case 3:
                filledIconHolder.Add(iconHolder[0]);
                filledIconHolder.Add(iconHolder[2]);
                filledIconHolder.Add(iconHolder[4]);
                break;
            case 4:
                filledIconHolder.Add(iconHolder[0]);
                filledIconHolder.Add(iconHolder[1]);
                filledIconHolder.Add(iconHolder[3]);
                filledIconHolder.Add(iconHolder[4]);
                break;
            default:
                Debug.LogError("GestureIconHolder can only handle up to 4 actions.");
                break;
        }

        // fill the viewholder with content
        for (int i = 0; i < filledIconHolder.Count; i++)
        {
            filledIconHolder[i].GetComponent<ActionIconHandler>().SetAction(actions[i]);
        }
    }
}
