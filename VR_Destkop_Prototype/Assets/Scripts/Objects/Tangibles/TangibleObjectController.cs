using UnityEngine;
using System.Collections;

using TangibleType = ApplicationConstants.TangibleType;

public class TangibleObjectController : MonoBehaviour {

    public TangibleType tangibleType;

    void Start()
    {
        // Get correct instance of the tangible object
        switch (tangibleType)
        {
            case TangibleType.Icon:
                gameObject.AddComponent<Icon>();
                break;
            case TangibleType.Box:
                gameObject.AddComponent<Box>();
                break;
        }
    }
}
