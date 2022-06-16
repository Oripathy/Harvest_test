using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerView : MonoBehaviour, IPlayerView
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Animator _animator;

        public Rigidbody Rigidbody => _rigidbody;
        public Transform Transform => transform;
        public Animator Animator => _animator;
    }
}