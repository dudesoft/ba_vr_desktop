using UnityEngine;

public class ActionIconHandler : MonoBehaviour {

    public GameObject iconHolder;

    private MyoMapper myoMapper;
    private SpriteRenderer actionSprite;
    private TextMesh textHolder;

    void Start()
    {
        myoMapper = MyoMapper.GetInstance();
        actionSprite = iconHolder.GetComponent<SpriteRenderer>();
        textHolder = gameObject.GetComponentInChildren<TextMesh>();
    }

    public void SetAction(ActionHolder actionHolder)
    {
        actionSprite.sprite = myoMapper.spriteMapping[actionHolder.action];
        textHolder.text = actionHolder.description;
    }
}
