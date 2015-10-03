using UnityEngine;

public class OpenBox : MonoBehaviour
{
    public GameObject grid;
    private Animation gridAnimation;

    void Start()
    {
        gridAnimation = grid.GetComponent<Animation>();
    }

    public void ShowGrid()
    {
        gridAnimation.Play();
    }
}