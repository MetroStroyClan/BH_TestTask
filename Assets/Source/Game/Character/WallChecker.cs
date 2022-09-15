using System;
using UnityEngine;

namespace Source.Game.Character
{
    public class WallChecker : MonoBehaviour
    {
        [SerializeField] private float minDistanceToWall;
        [SerializeField] private string wallTag;
        
        private bool _isWallOnFront = false;

        public bool IsWallOnFront
        {
            get { return _isWallOnFront; }
        }

        private void Update()
        {
            CheckWall();
        }

        public bool IsWallOnLocalDirection(Vector3 dir)
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(dir), out var hit))
            {
                if ((bool)hit.collider?.gameObject.CompareTag(wallTag) && hit.distance < minDistanceToWall)
                    return true;
            }

            return false;
        } 
        
        private void CheckWall()
        {
            if (Physics.Raycast(transform.position, transform.forward, out var hit))
            {
                if ((bool)hit.collider?.gameObject.CompareTag(wallTag) && hit.distance < minDistanceToWall)
                {
                    _isWallOnFront = true;
                }
                else
                {
                    _isWallOnFront = false;
                }
            }
            else
            {
                _isWallOnFront = false;
            }
        }
    }
}
