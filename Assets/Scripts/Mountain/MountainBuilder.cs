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
}