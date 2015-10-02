using UnityEngine;

public class Icon : TangibleObject
{
    public override void Start()
    {
        canContainObjects = false;
        base.Start();
    }

    public override Renderer GetRenderer()
	{
        GameObject meshHolder = transform.FindChild("MeshHolder").gameObject;
        return meshHolder.GetComponent<Renderer>();
    }

	public override void OnDeselect ()
	{
		SetEmission (Color.black);
        HideActionIcons();
    }

	public override void OnGrab ()
	{
        ShowActionIcons(GestureIconBuilder.BuildActionHolderSet(GestureIconBuilder.ActionHolderType.MOVE_ICON));
    }

    public override void OnRelease()
    {
        ShowActionIcons(GestureIconBuilder.BuildActionHolderSet(GestureIconBuilder.ActionHolderType.BASIC_ICON));
    }

    public override void OnSelect ()
	{
		SetEmission (ApplicationConstants.HIGHLIGHTED);
        ShowActionIcons(GestureIconBuilder.BuildActionHolderSet(GestureIconBuilder.ActionHolderType.BASIC_ICON));
    }

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}