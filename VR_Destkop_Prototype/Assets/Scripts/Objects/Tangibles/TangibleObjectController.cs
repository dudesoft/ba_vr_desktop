using UnityEngine;
using System.Collections;

using TangibleType = ApplicationConstants.TangibleType;

public class TangibleObjectController : MonoBehaviour {

    public TangibleType tangibleType;
    private TangibleObject tangibleObject;

    void Start()
    {
        // Get correct instance of the tangible object
        switch (tangibleType)
        {
            case TangibleType.Icon:
                tangibleObject = gameObject.AddComponent<Icon>();
                break;
            case TangibleType.Box:
                tangibleObject = gameObject.AddComponent<Box>();
                break;
        }
    }
}
