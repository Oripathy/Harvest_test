using System;
using Player;
using UnityEngine;

namespace MainCamera
{
    public class CameraMover : MonoBehaviour
    {
        private PlayerView _player;
        private readonly Vector3 _offset = new Vector3(0f, 5f, -4f);
        private readonly double _rotationAngle = 50 * Math.PI / 180;
        private Quaternion _rotationOffset;

        private void Start()
        {
            _player = FindObjectOfType<PlayerView>();
            _rotationOffset = new Quaternion((float)Math.Sin(_rotationAngle / 2), 0f, 0f,
                (float)Math.Cos(_rotationAngle / 2));
            transform.rotation = _rotationOffset * transform.rotation;
        }

        private void LateUpdate()
        {
            transform.position = _player.transform.position + _offset;
        }
    }
}