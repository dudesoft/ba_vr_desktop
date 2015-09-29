using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ObjectStorage : MonoBehaviour {

    private List<GameObject> storedObjects = new List<GameObject>();

    public void Store(GameObject gameObject)
    {
        storedObjects.Add(gameObject);
    }
}
