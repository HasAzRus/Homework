using UnityEngine;

namespace Homework
{
    public class PlayerStart : Behaviour
    {
        private void OnDrawGizmos()
        {
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawWireCube(Vector3.zero, new Vector3(1, 2, 1));
        }
    }
}