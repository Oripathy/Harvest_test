using System;
using UnityEngine;
using WheatField.Wheat;

namespace Player.WheatDetector
{
    public class WheatDetectorView : MonoBehaviour, IWheatDetectorView
    {
        [SerializeField] private Transform _detectionPoint;
        [SerializeField] private LayerMask _wheatLayer;
        
        private readonly float _detectionRadius = 0.3f;
        private Collider[] _colliderHit = new Collider[2];
        private bool _isWheatDetected;
        
        public event Action WheatDetected;
        public event Action WheatNotDetected;

        private void Update()
        {
            //Physics.OverlapSphereNonAlloc(_detectionPoint.position, _detectionRadius, _colliderHit, _wheatLayer);

            var colliders = Physics.OverlapSphere(_detectionPoint.position, _detectionRadius, _wheatLayer);
            
            if (colliders.Length != 0 && !_isWheatDetected)
            {
                WheatDetected?.Invoke();
                _isWheatDetected = true;
            }
            else if (colliders.Length == 0 && _isWheatDetected)
            {
                WheatNotDetected?.Invoke();
                _isWheatDetected = false;
            }
        }
    }
}