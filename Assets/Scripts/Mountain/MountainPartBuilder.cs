using UnityEngine;
using System.Collections;

namespace SeafoodEditorHelper
{
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
                cdr.size = new Vector2(bounds.size.x, bounds.size.y * 0.7f);
                cdr.offset = new Vector2(0, -bounds.size.y * 0.15f);
            }
            else if (type == MountainPartType.LeftTop)
            {
                var cdr = gameObject.AddComponent<BoxCollider2D>();
                cdr.size = new Vector2(bounds.size.x * 0.8f, bounds.size.y * 0.7f);
                cdr.offset = new Vector2(bounds.size.x * 0.1f, -bounds.size.y * 0.15f);
            }
            else if (type == MountainPartType.RightTop)
            {
                var cdr = gameObject.AddComponent<BoxCollider2D>();
                cdr.size = new Vector2(bounds.size.x * 0.8f, bounds.size.y * 0.7f);
                cdr.offset = new Vector2(-bounds.size.x * 0.1f, -bounds.size.y * 0.15f);
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
}