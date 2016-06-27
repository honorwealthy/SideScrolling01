using UnityEngine;
using System.Collections;

namespace SeafoodStudio
{
    public class GroundChecker : MonoBehaviour
    {
        public float Radius = 1f;
        public float Distance = 0f;
//        public RaycastHit2D Hit;

        [SerializeField]
        private bool isHit;

//        private void Awake()
//        {
//            CheckGround();
//        }

//        private void FixedUpdate()
//        {
//            CheckGround();
//        }

		public RaycastHit2D CheckGround()
        {
			RaycastHit2D Hit = Physics2D.CircleCast(transform.position, Radius, Vector2.down, Distance, 1 << LayerMask.NameToLayer("Ground"));
            isHit = (bool)Hit;
			return Hit;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, Radius);

            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position + Vector3.down * Distance, Radius);
        }
    }
}