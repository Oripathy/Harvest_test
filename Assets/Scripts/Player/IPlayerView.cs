using Base;
using UnityEngine;

namespace Player
{
    public interface IPlayerView : IBaseView
    {
        public Rigidbody Rigidbody { get; }
        public Transform Transform { get; }
    }
}