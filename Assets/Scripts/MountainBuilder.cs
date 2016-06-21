using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
            gameObject.layer = LayerMask.NameToLayer("Ground");

            var obj = gameObject.BuildMountainPartByType(MountainPartType.LeftTop);
            obj.BuildMountainNeighbor(MountainNeighborType.Right, MountainPartType.RightTop);
            var obj2 = obj.BuildMountainNeighbor(MountainNeighborType.Down, MountainPartType.LeftBottom);
            obj2.BuildMountainNeighbor(MountainNeighborType.Right, MountainPartType.RightBottom);
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

        public void BuildCollider()
        {
            if (transform.childCount > 0)
            {
                foreach (Transform child in transform)
                {
                    var partBuilder = child.GetComponent<MountainPartBuilder>();
                    partBuilder.BuildCollider();
                }
            }
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
        public MountainPartType type;

        //private float ColliderHeight { get { return 0.3f; } }
        //private float ColliderWidth { get { return 0.8f; } }

        public static GameObject BuildPartByType(GameObject parent, MountainPartType type)
        {
            MountainBuilder builder = parent.GetComponent<MountainBuilder>();

            var obj = new GameObject();
            obj.layer = LayerMask.NameToLayer("Ground");
            obj.AddComponent<SpriteRenderer>().sprite = builder.GetSpriteByType(type);
            obj.transform.SetParent(parent.transform);
            obj.transform.localPosition = new Vector3();
            obj.transform.localScale = Vector3.one;

            obj.AddComponent<MountainPartBuilder>().type = type;
            return obj;
        }

        public GameObject BuildNeighbor(MountainNeighborType neighbor, MountainPartType part)
        {
            MountainBuilder builder = gameObject.transform.parent.GetComponent<MountainBuilder>();
            GameObject obj = new GameObject();
            obj.layer = LayerMask.NameToLayer("Ground");

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
            obj.transform.localScale = Vector3.one;
            obj.AddComponent<MountainPartBuilder>().type = part;
            return obj;
        }

        public void BuildCollider()
        {
            var collider = gameObject.GetComponent<Collider2D>();

            if (collider != null)
                DestroyImmediate(collider);

            var bounds = gameObject.GetComponent<SpriteRenderer>().sprite.bounds;
            //if (type == MountainPartType.CenterTop)
            //{
            //    var cdr = gameObject.AddComponent<BoxCollider2D>();
            //    cdr.size = new Vector2(bounds.size.x, bounds.size.y * ColliderHeight);
            //    cdr.offset = new Vector2(0, 0);
            //}
            //else if (type == MountainPartType.LeftTop)
            //{
            //    var cdr = gameObject.AddComponent<BoxCollider2D>();
            //    cdr.size = new Vector2(bounds.size.x * ColliderWidth, bounds.size.y * ColliderHeight);
            //    cdr.offset = new Vector2(bounds.size.x * (1 - ColliderWidth) / 2f, 0);
            //}
            //else if (type == MountainPartType.RightTop)
            //{
            //    var cdr = gameObject.AddComponent<BoxCollider2D>();
            //    cdr.size = new Vector2(bounds.size.x * ColliderWidth, bounds.size.y * ColliderHeight);
            //    cdr.offset = new Vector2(-bounds.size.x * (1 - ColliderWidth) / 2f, 0);
            //}
            if (type == MountainPartType.CenterTop)
            {
                var cdr = gameObject.AddComponent<BoxCollider2D>();
                cdr.size = new Vector2(bounds.size.x, bounds.size.y * 0.6f);
                cdr.offset = new Vector2(0, -bounds.size.y * 0.2f);
            }
            else if (type == MountainPartType.LeftTop)
            {
                var cdr = gameObject.AddComponent<BoxCollider2D>();
                cdr.size = new Vector2(bounds.size.x * 0.8f, bounds.size.y * 0.6f);
                cdr.offset = new Vector2(bounds.size.x * 0.1f, -bounds.size.y * 0.2f);
            }
            else if (type == MountainPartType.RightTop)
            {
                var cdr = gameObject.AddComponent<BoxCollider2D>();
                cdr.size = new Vector2(bounds.size.x * 0.8f, bounds.size.y * 0.6f);
                cdr.offset = new Vector2(-bounds.size.x * 0.1f, -bounds.size.y * 0.2f);
            }
            else if (type == MountainPartType.LeftMiddle)
            {
                var cdr = gameObject.AddComponent<BoxCollider2D>();
                cdr.size = new Vector2(bounds.size.x * 0.8f, bounds.size.y);
                cdr.offset = new Vector2(bounds.size.x * 0.1f, 0);
            }
            else if (type == MountainPartType.LeftBottom)
            {
                var cdr = gameObject.AddComponent<PolygonCollider2D>();
                cdr.SetPath(0, new Vector2[] {
                    new Vector2(bounds.min.x * 0.6f, bounds.max.y),
                    new Vector2(bounds.max.x, bounds.max.y),
                    new Vector2(bounds.max.x, bounds.min.y)
                });
            }
            else if (type == MountainPartType.RightMiddle)
            {
                var cdr = gameObject.AddComponent<BoxCollider2D>();
                cdr.size = new Vector2(bounds.size.x * 0.8f, bounds.size.y);
                cdr.offset = new Vector2(-bounds.size.x * 0.1f, 0);
            }
            else if (type == MountainPartType.RightBottom)
            {
                var cdr = gameObject.AddComponent<PolygonCollider2D>();
                cdr.SetPath(0, new Vector2[] {
                    new Vector2(bounds.max.x * 0.6f, bounds.max.y),
                    new Vector2(bounds.min.x, bounds.max.y),
                    new Vector2(bounds.min.x, bounds.min.y)
                });
            }
            else if (type == MountainPartType.CenterMiddle)
            {
                var cdr = gameObject.AddComponent<BoxCollider2D>();
                cdr.size = new Vector2(bounds.size.x, bounds.size.y);
                cdr.offset = new Vector2(0, 0);
            }
            else if (type == MountainPartType.CenterBottom)
            {
                var cdr = gameObject.AddComponent<BoxCollider2D>();
                cdr.size = new Vector2(bounds.size.x, bounds.size.y);
                cdr.offset = new Vector2(0, 0);
            }
        }
    }

    //public class MountainPartColliderHelper
    //{
    //    public void GenerateCollider(GameObject gameObject, Bounds bounds)
    //    {
    //        var cdr = gameObject.AddComponent<BoxCollider2D>();
    //        cdr.size = new Vector2(bounds.size.x, bounds.size.y * 0.3f);
    //        cdr.offset = new Vector2(0, 0);
    //    }
    //}

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