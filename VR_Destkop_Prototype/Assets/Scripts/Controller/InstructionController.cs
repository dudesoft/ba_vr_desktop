using UnityEngine;
using System.Collections;
using System;

public class InstructionController : MonoBehaviour
{
    private Color transparent = new Color(1, 1, 1, 0);
    private Color visible = new Color(1, 1, 1, 1);
    private Color targetColor;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        targetColor = transparent;
        spriteRenderer = GetComponent<SpriteRenderer>();
        SelectionManager.GetInstance().OnStartLookingAt += OnStartLookingAt;
        SelectionManager.GetInstance().OnStopLookingAt += OnStopLookingAt;
    }

    private void OnStartLookingAt(GameObject g)
    {
        if (g == gameObject)
        {
            targetColor = visible;
        }
    }

    private void OnStopLookingAt(GameObject g)
    {
        if (g == gameObject)
        {
            targetColor = transparent;
        }
    }

    void Update()
    {
        spriteRenderer.material.color = Color.Lerp(spriteRenderer.material.color, targetColor, 0.05f);
    }
}
