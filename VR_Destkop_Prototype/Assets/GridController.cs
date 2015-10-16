using UnityEngine;

public class GridController : MonoBehaviour
{
    // Get a Reference to the ParentObject
    private GameObject boxParent;

    void Start()
    {
        Transform parent = transform;

        while (boxParent == null)
        {
            if (parent.parent != null)
            {
                parent = parent.parent;
            }
            else
            {
                boxParent = parent.gameObject;
            }
        }
    }

    public void SpawnIcons()
    {
        Box box = boxParent.GetComponent<Box>();

        if (box == null)
        {
            return;
        }


    }

    public void DisableGrid()
    {
        gameObject.SetActive(false);
    }
}
