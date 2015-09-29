using UnityEngine;
using System.Collections;

using Pose = Thalmic.Myo.Pose;

public class ActionIconHandler : MonoBehaviour {

    public GameObject iconHolder;

    private MyoMapper myoMapper;
    private SpriteRenderer actionSprite;

    void Start()
    {
        myoMapper = MyoMapper.GetInstance();
        actionSprite = iconHolder.GetComponent<SpriteRenderer>();
    }

    public void SetAction(ActionHolder actionHolder)
    {
        actionSprite.sprite = myoMapper.spriteMapping[actionHolder.action];
    }
}
