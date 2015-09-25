using UnityEngine;
using System.Collections;

public class HandAppearanceController : MonoBehaviour {

    float transparency;
    SkinnedMeshRenderer MeshRenderer;

	void Start () {
        MeshRenderer = GetComponent<SkinnedMeshRenderer>();
        transparency = 1f;
	}
	
	void Update () {
        if (MeshRenderer.material.color.a != transparency)
        {
            foreach (Material material in MeshRenderer.materials)
            {
                material.color = new Color(material.color.r, material.color.g, material.color.b, Mathf.Lerp(material.color.a, transparency, 0.05f));
            }
        }
	}

    void SetTransparency(float value)
    {
        transparency = value;
    }
}