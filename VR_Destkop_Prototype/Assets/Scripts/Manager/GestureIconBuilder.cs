using UnityEngine;
using System.Collections.Generic;

public class GestureIconBuilder
{
    public enum ActionHolderType
    {
        BASIC_ICON, MOVE_ICON, BASIC_BOX
    }

    public static List<ActionHolder> BuildActionHolderSet(ActionHolderType type)
    {
        MyoMapper mapping = MyoMapper.GetInstance();

        switch (type)
        {
            case ActionHolderType.BASIC_ICON:
                return new List<ActionHolder> { new ActionHolder(mapping.handMapping.waveLeft, "Start"),
                new ActionHolder(mapping.handMapping.fist, "Move"),new ActionHolder(mapping.handMapping.waveRight, "Edit")};
            case ActionHolderType.BASIC_BOX:
                return new List<ActionHolder> { new ActionHolder(mapping.handMapping.waveLeft, "Open"),
                new ActionHolder(mapping.handMapping.fist, "Move"),new ActionHolder(mapping.handMapping.waveRight, "Edit")};
            case ActionHolderType.MOVE_ICON:
                return new List<ActionHolder> { new ActionHolder(mapping.handMapping.move, "Release") };
            default:
                Debug.Log("Unknown ActionHolderType.");
                return null;
        }
    }
}
