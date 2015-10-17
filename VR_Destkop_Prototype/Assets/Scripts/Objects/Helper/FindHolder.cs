using UnityEngine;
using System.Collections;

public class FindHolder : MonoBehaviour {

	private SpriteRenderer sRenderer;
	private GestureProgress gProgress;

	void OnEnable() {
		EventManager.GetInstance ().SetProgress += FindTheHolder;
	}
	
	void OnDisable() {
		EventManager.GetInstance ().SetProgress -= FindTheHolder;
	}

	void Start () {
		sRenderer = GetComponent<SpriteRenderer> ();
		gProgress = transform.parent.gameObject.GetComponent<GestureProgress> ();
	}

	private void FindTheHolder(Sprite sprite, float progress) {
		if (sprite == sRenderer.sprite) 
		{
			gProgress.SetGestureProgress(progress);
		}
	}
}
