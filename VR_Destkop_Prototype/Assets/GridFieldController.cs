using UnityEngine;

public class GridFieldController : MonoBehaviour
{
    public GameObject grid;
    private GridController gridController;

    void Start()
    {
        gridController = grid.GetComponent<GridController>();
    }

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Entered");
    }
}
