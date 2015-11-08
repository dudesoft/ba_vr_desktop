using UnityEngine;
using System.Collections.Generic;

public class GestureIconBuilder
{
	public enum ActionHolderType
	{
		BASIC_ICON, MOVE_ICON, BASIC_BOX, OPEN_BOX, TRASH, STORED_BOX
	}

	public static List<ActionHolder> BuildActionHolderSet(ActionHolderType type)
	{
		MyoMapper mapping = MyoMapper.GetInstance();

		switch (type)
		{
			case ActionHolderType.BASIC_ICON:
			return new List<ActionHolder> {new ActionHolder(mapping.handMapping.waveRight, "Delete"),
				new ActionHolder(mapping.handMapping.fist, "Grab")};
			case ActionHolderType.BASIC_BOX:
			return new List<ActionHolder> { new ActionHolder(mapping.handMapping.waveLeft, "Open"),
				new ActionHolder(mapping.handMapping.fist, "Grab"),new ActionHolder(mapping.handMapping.waveRight, "Delete")};
			case ActionHolderType.STORED_BOX:
				return new List<ActionHolder> {new ActionHolder(mapping.handMapping.fist, "Grab"),
					new ActionHolder(mapping.handMapping.waveRight, "Delete")};
			case ActionHolderType.OPEN_BOX:
			return new List<ActionHolder> {new ActionHolder(mapping.handMapping.waveRight, "Close"),
				new ActionHolder(mapping.handMapping.fist, "Grab")};
			case ActionHolderType.MOVE_ICON:
			return new List<ActionHolder> { new ActionHolder(mapping.handMapping.spread, "Release"), 
				new ActionHolder(mapping.handMapping.move, "Move")};
			case ActionHolderType.TRASH:
			return new List<ActionHolder> { new ActionHolder(mapping.handMapping.waveLeft, "Open"),
				new ActionHolder(mapping.handMapping.fist, "Grab"), new ActionHolder(mapping.handMapping.waveRight, "Clear")};
			default:
				Debug.Log("Unknown ActionHolderType.");
				return null;
		}
	}
}