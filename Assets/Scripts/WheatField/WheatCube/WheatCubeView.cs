using System;
using Barn;
using Barn.SellPoint;
using UnityEngine;

namespace WheatField.WheatCube
{
    public class WheatCubeView : MonoBehaviour, IWheatCubeView, ICollectable
    {
        [SerializeField] private Collider _collider;

        private bool _isSellable;

        public Transform Transform => transform;
        public Collider Collider => _collider;

        public event Action<Vector3, Transform> CubeCollected;
        public event Action<Vector3> EnteredSellZone;
        public event Action<SellPointView> Sold;

        public void Collect(Vector3 position, Transform bag)
        {
            CubeCollected?.Invoke(position, bag);
            _isSellable = true;
        }

        public void Sell(Vector3 position)
        {
            EnteredSellZone?.Invoke(position);
        }

        public void DestroyCube()
        {
            Destroy(gameObject);
        }

        public void OnTriggerEnter(Collider other)
        {
            if (_isSellable)
            {
                if (other.TryGetComponent<SellPointView>(out var sellPoint))
                {
                    Sold?.Invoke(sellPoint);
                    Debug.Log(sellPoint);
                }
            }
        }
    }
}