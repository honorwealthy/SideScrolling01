using UnityEngine;
using System.Collections;

namespace SeafoodEditorHelper
{
    public class MountainBuilder : MonoBehaviour
    {
        public Sprite LeftTop;
        public Sprite LeftMiddle;
        public Sprite LeftBottom;
        public Sprite RightTop;
        public Sprite RightMiddle;
        public Sprite RightBottom;
        public Sprite[] CenterTop;
        public Sprite[] CenterMiddle;
        public Sprite CenterBottom;

        public void BuildBasic()
        {
            var obj = MountainPartBuilder.BuildPartByType(gameObject, MountainPartType.LeftTop);
            obj.BuildNeighbor(MountainNeighborType.Right, MountainPartType.RightTop);
            var obj2 = obj.BuildNeighbor(MountainNeighborType.Down, MountainPartType.LeftBottom);
            obj2.BuildNeighbor(MountainNeighborType.Right, MountainPartType.RightBottom);
        }

        public Sprite GetSpriteByType(MountainPartType type)
        {
            Sprite sprite = null;
            switch (type)
            {
                case MountainPartType.LeftTop:
                    sprite = LeftTop;
                    break;
                case MountainPartType.LeftMiddle:
                    sprite = LeftMiddle;
                    break;
                case MountainPartType.LeftBottom:
                    sprite = LeftBottom;
                    break;
                case MountainPartType.RightTop:
                    sprite = RightTop;
                    break;
                case MountainPartType.RightMiddle:
                    sprite = RightMiddle;
                    break;
                case MountainPartType.RightBottom:
                    sprite = RightBottom;
                    break;
                case MountainPartType.CenterTop:
                    sprite = CenterTop[Random.Range(0, 2)];
                    break;
                case MountainPartType.CenterMiddle:
                    sprite = CenterMiddle[Random.Range(0, 2)];
                    break;
                case MountainPartType.CenterBottom:
                    sprite = CenterBottom;
                    break;
            }
            return sprite;
        }
    }

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

    public class MountainPartBuilder : MonoBehaviour
    {
        [SerializeField]
        public MountainPartType type { get; set; }

        public static GameObject BuildPartByType(GameObject parent, MountainPartType type)
        {
            MountainBuilder builder = parent.GetComponent<MountainBuilder>();

            var obj = new GameObject("Mountain");
            obj.AddComponent<SpriteRenderer>().sprite = builder.GetSpriteByType(type);
            obj.transform.SetParent(parent.transform);
            obj.transform.localPosition = new Vector3();

            obj.AddComponent<MountainPartBuilder>().type = type;
            return obj;
        }

        public GameObject BuildNeighbor(MountainNeighborType neighbor, MountainPartType part)
        {
            MountainBuilder builder = gameObject.transform.parent.GetComponent<MountainBuilder>();
            GameObject obj = new GameObject("Mountain");

            Sprite otherSprite = builder.GetSpriteByType(part);
            obj.AddComponent<SpriteRenderer>().sprite = otherSprite;
            obj.transform.SetParent(builder.transform);

            Sprite selfSprite = GetComponent<SpriteRenderer>().sprite;
            Vector3 selfLocalPosition = transform.localPosition;
            Vector3 otherLocalPosition = selfLocalPosition;

            switch (neighbor)
            {
                case MountainNeighborType.Up:
                    otherLocalPosition = selfLocalPosition + new Vector3(0, selfSprite.bounds.max.y + otherSprite.bounds.extents.y, 0);
                    break;
                case MountainNeighborType.Down:
                    otherLocalPosition = selfLocalPosition + new Vector3(0, selfSprite.bounds.min.y - otherSprite.bounds.extents.y, 0);
                    break;
                case MountainNeighborType.Left:
                    otherLocalPosition = selfLocalPosition + new Vector3(selfSprite.bounds.min.x - otherSprite.bounds.extents.x, 0, 0);
                    break;
                case MountainNeighborType.Right:
                    otherLocalPosition = selfLocalPosition + new Vector3(selfSprite.bounds.max.x + otherSprite.bounds.extents.x, 0, 0);
                    break;
            }

            obj.transform.localPosition = otherLocalPosition;
            var partBuilder = obj.AddComponent<MountainPartBuilder>().type = part;
            return obj;
        }
    }

    public static class MountainPartBuilderHelper
    {
        public static GameObject BuildNeighbor(this GameObject parent, MountainNeighborType neighbor, MountainPartType part)
        {
            var partBuilder = parent.GetComponent<MountainPartBuilder>();
            return partBuilder.BuildNeighbor(neighbor, part);
        }
    }
}