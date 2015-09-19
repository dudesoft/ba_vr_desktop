using UnityEngine;
using System.Collections;

public class IconController : MonoBehaviour
{
    public float objectDistance;

    private bool selected;
    private GameObject anker;
    private bool grabbed;
    private float maxEmission = 0.3f;
    private float minEmission = 0.01f;
    private float color, halo;
    private Light light;
    private Material material;

    void Update()
    {
        anker = GameObject.Find("Cursor Hand");
        if (GetComponent<MeshRenderer>() != null)
        {
            material = GetComponent<MeshRenderer>().material;
        }
        else
        {
            material = GetComponent<SkinnedMeshRenderer>().material;
        }
        color = minEmission;
        light = GetComponent<Light>();
        SetupLocationAndRotation();
    }

    private void SetupLocationAndRotation()
    {
        if (grabbed)
        {
            transform.position = anker.transform.position;
        }
        transform.position = (transform.position - Camera.main.transform.position).normalized * objectDistance + Camera.main.transform.position;
        transform.LookAt(Camera.main.transform);

        if (selected)
        {
            color = Mathf.Lerp(material.GetColor("_EmissionColor").r, maxEmission, 0.1f);
            halo = Mathf.Lerp(light.intensity, maxEmission, 0.1f);
            light.intensity = halo;
        }
        else
        {
            color = Mathf.Lerp(material.GetColor("_EmissionColor").r, minEmission, 0.1f);
            halo = Mathf.Lerp(light.intensity, 0, 0.1f);
            light.intensity = halo;
        }
        material.SetColor("_EmissionColor", new Color(color, color, color));
    }

    public void Grab()
    {
        grabbed = true;
    }

    private void Release()
    {
        grabbed = false;
    }

    public void Select()
    {
        selected = true;
    }

    public void Deselect()
    {
        selected = false;
    }
}
