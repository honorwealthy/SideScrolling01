using UnityEngine;
using System.Collections;

namespace SeafoodEditorHelper
{
    public enum MountainPartType
    {
        LeftTop,
        LeftMiddle,
        LeftBottom,
        RightTop,
        RightMiddle,
        RightBottom,
        CenterTop,
        CenterMiddle,
        CenterBottom,
    }

    public enum MountainNeighborType
    {
        Up,
        Down,
        Left,
        Right
    }

    public static class MountainPartBuilderHelper
    {
        public static GameObject BuildMountainPartByType(this GameObject parent, MountainPartType type)
        {
            return MountainPartBuilder.BuildPartByType(parent, type);
        }

        public static GameObject BuildMountainNeighbor(this GameObject parent, MountainNeighborType neighbor, MountainPartType part)
        {
            var partBuilder = parent.GetComponent<MountainPartBuilder>();
            return partBuilder.BuildNeighbor(neighbor, part);
        }
    }
}
