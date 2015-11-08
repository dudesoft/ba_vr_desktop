using UnityEngine;

public class DrawLine : MonoBehaviour
{
    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (transform.childCount == 0)
        {
            lineRenderer.enabled = false;
            return;
        }
        else if (!transform.GetChild(0).gameObject.activeSelf)
        {
            lineRenderer.enabled = false;
            return;
        }
        else
        {
            lineRenderer.enabled = true;
        }
        lineRenderer.SetPosition(0, transform.parent.parent.position);
        lineRenderer.SetPosition(1, transform.position);
    }
}
