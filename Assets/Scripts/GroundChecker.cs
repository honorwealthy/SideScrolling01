using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SeafoodStudio
{
    public enum CheckDirection { Down, Forward }

    public class GroundChecker : MonoBehaviour
    {
        public float Radius = 1f;
        public float Distance = 0f;
        public CheckDirection Direction = CheckDirection.Down;

		public RaycastHit2D CheckGround()
        {
            Vector2 dir = GetDirection(Direction);
            return Physics2D.CircleCast(transform.position, Radius, dir, Distance, 1 << LayerMask.NameToLayer("Ground"));
        }

        private Vector3 GetDirection(CheckDirection direction)
        {
            Vector3 ret = Vector3.down;
            if (direction == CheckDirection.Forward)
            {
                int forward = transform.lossyScale.x > 0 ? 1: -1;
                ret = Vector3.right * forward;
            }

            return ret;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, Radius);

            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position + GetDirection(Direction) * Distance, Radius);
        }
    }
}