using System;
using UnityEngine;
using WheatField.WheatCube;

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

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<ICollectable>(out var collectable))
            {
                CollidedWithCollectable?.Invoke(collectable);
            }
        }
    }
}