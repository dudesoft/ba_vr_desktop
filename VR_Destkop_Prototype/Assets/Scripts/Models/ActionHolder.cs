using Pose = Thalmic.Myo.Pose;

public class ActionHolder
{
	public Pose action { get; set; }
	public string description { get; set; }

	// Is a critical action performed, p.e. deleting an object?
	public bool critical { get; set; }

	public ActionHolder(Pose action, string description)
	{
		this.action = action;
		this.description = description;
	}
}