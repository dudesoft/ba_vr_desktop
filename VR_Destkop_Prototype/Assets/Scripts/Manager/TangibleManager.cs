using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class TangibleManager : MonoBehaviour
{
    private List<GameObject> tangibles = new List<GameObject>();

    private static TangibleManager instance;

    public static TangibleManager GetInstance()
    {
        if (!instance)
        {
            instance = (TangibleManager)GameObject.FindObjectOfType(typeof(TangibleManager));
        }
        return instance;
    }

    void Start()
    {
        FindAllTangibles();
    }

    private void FindAllTangibles()
    {
        tangibles = GameObject.FindGameObjectsWithTag(ApplicationConstants.Tags.TANGIBLE).ToList();
    }

    public void AddTangible(GameObject tangible)
    {
        tangibles.Add(tangible);
    }

    public void RemoveTangible(GameObject tangible)
    {
        tangibles.Remove(tangible);
    }

    public List<GameObject> GetTangibles()
    {
        return tangibles;
    }
}