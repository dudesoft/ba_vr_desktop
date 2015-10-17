using UnityEngine;
using System.Collections;

public class FindHolder : MonoBehaviour {

	private SpriteRenderer sRenderer;
	private GestureProgress gProgress;

	void Awake() 
	{
		EventManager.GetInstance ().SetProgress += FindTheHolder;
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

	void OnDestroy() {
		EventManager.GetInstance ().SetProgress -= FindTheHolder;
	}
}
