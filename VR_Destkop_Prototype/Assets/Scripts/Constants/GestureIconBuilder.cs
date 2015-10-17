using UnityEngine;
using System.Collections.Generic;

public class GestureIconBuilder
{
    public enum ActionHolderType
    {
        BASIC_ICON, MOVE_ICON, BASIC_BOX, TRASH
    }

    public static List<ActionHolder> BuildActionHolderSet(ActionHolderType type)
    {
        MyoMapper mapping = MyoMapper.GetInstance();

        switch (type)
        {
            case ActionHolderType.BASIC_ICON:
			return new List<ActionHolder> {new ActionHolder(mapping.handMapping.waveRight, "Delete", true),
				new ActionHolder(mapping.handMapping.fist, "Move")};
            case ActionHolderType.BASIC_BOX:
			return new List<ActionHolder> { new ActionHolder(mapping.handMapping.waveRight, "Delete", true),
				new ActionHolder(mapping.handMapping.fist, "Move"),new ActionHolder(mapping.handMapping.waveLeft, "Open")};
            case ActionHolderType.MOVE_ICON:
                return new List<ActionHolder> { new ActionHolder(mapping.handMapping.spread, "Release") };
			case ActionHolderType.TRASH:
			return new List<ActionHolder> { new ActionHolder(mapping.handMapping.waveRight, "Clear", true),
				new ActionHolder(mapping.handMapping.fist, "Move"), new ActionHolder(mapping.handMapping.waveLeft, "Open")};
            default:
                Debug.Log("Unknown ActionHolderType.");
                return null;
        }
    }
}
