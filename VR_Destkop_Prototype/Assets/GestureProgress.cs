using UnityEngine;
using System.Collections;

public class GestureProgress : MonoBehaviour {

    public Sprite[] progressStates;
    private SpriteRenderer spriteRenderer;

	void Start () 
	{
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
	}

	public void SetGestureProgress(float progress)
    {
        int usedSprite = Mathf.RoundToInt(progressStates.Length * progress);
        spriteRenderer.sprite = progressStates[usedSprite];
    }
}
