using System;
using Barn;
using UnityEngine;
using WheatField.WheatCube;
using Vector3 = System.Numerics.Vector3;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerView : MonoBehaviour, IPlayerView
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Animator _animator;
        [SerializeField] private Transform _bag;

        public Rigidbody Rigidbody => _rigidbody;
        public Transform Transform => transform;
        public Transform Bag => _bag;
        public Animator Animator => _animator;

        public event Action<ICollectable> CollidedWithCollectable;
        public event Action<BarnView> EnteredSellZone;
        public event Action ExitSellZone;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<ICollectable>(out var collectable))
            {
                CollidedWithCollectable?.Invoke(collectable);
            }

            if (other.TryGetComponent<BarnView>(out var barn))
            {
                EnteredSellZone?.Invoke(barn);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<BarnView>(out var bar))
            {
                ExitSellZone?.Invoke();
            }
        }
    }
}