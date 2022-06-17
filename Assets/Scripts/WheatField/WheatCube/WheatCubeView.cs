using System;
using UnityEngine;

namespace WheatField.WheatCube
{
    public class WheatCubeView : MonoBehaviour, IWheatCubeView, ICollectable
    {
        [SerializeField] private Collider _collider;

        public Transform Transform => transform;
        public Collider Collider => _collider;

        public event Action<Vector3, Transform> CubeCollected;

        public void Collect(Vector3 position, Transform bag)
        {
            CubeCollected?.Invoke(position, bag);
        }
    }
}