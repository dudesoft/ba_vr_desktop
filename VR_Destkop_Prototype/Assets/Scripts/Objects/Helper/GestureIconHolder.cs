using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GestureIconHolder : MonoBehaviour
{
    public GameObject[] iconHolder;

    private List<GameObject> filledIconHolder;
    private bool shouldAnimateIn;
    private float animationTimer = 1;

    void Update()
    {
        if (animationTimer < 1)
        {
            if (shouldAnimateIn)
            {
                AnimateIn();
            }
            else
            {
                AnimateOut();
            }
        }
    }

    private void AnimateIn()
    {
        animationTimer += Time.deltaTime * 2;
        for (int i = 0; i < iconHolder.Length; i++)
        {
            iconHolder[i].transform.localScale = Vector3.Lerp(new Vector3(0, 0, 0), new Vector3(1, 1, 1), animationTimer);
        }
    }

    private void AnimateOut()
    {
        animationTimer += Time.deltaTime * 2;
        for (int i = 0; i < iconHolder.Length; i++)
        {
            iconHolder[i].transform.localScale = Vector3.Lerp(new Vector3(1, 1, 1), new Vector3(0, 0, 0), animationTimer);
        }
    }

    public void HideGestureIcons()
    {
        animationTimer = 0;
        shouldAnimateIn = false;
    }

    public void ShowGestureIcons(List<ActionHolder> actions)
    {
        ResetAnimation();
        shouldAnimateIn = true;

        // build up the right views for given amount of actions
        switch (actions.Count)
        {
            case 1:
                filledIconHolder.Add(iconHolder[2]);
                break;
            case 2:
                filledIconHolder.Add(iconHolder[1]);
                filledIconHolder.Add(iconHolder[3]);
                break;
            case 3:
                filledIconHolder.Add(iconHolder[0]);
                filledIconHolder.Add(iconHolder[2]);
                filledIconHolder.Add(iconHolder[4]);
                break;
            case 4:
                filledIconHolder.Add(iconHolder[0]);
                filledIconHolder.Add(iconHolder[1]);
                filledIconHolder.Add(iconHolder[3]);
                filledIconHolder.Add(iconHolder[4]);
                break;
            default:
                Debug.LogError("GestureIconHolder can only handle up to 4 actions.");
                break;
        }

        // fill the viewholder with content
        for (int i = 0; i < filledIconHolder.Count; i++)
        {
            filledIconHolder[i].GetComponent<ActionIconHandler>().SetAction(actions[i]);
            filledIconHolder[i].SetActive(true);
        }
    }

    private void ResetAnimation()
    {
        for (int i = 0; i < iconHolder.Length; i++)
        {
            iconHolder[i].SetActive(false);
        }
        animationTimer = 0;
        filledIconHolder = new List<GameObject>();
    }
}
