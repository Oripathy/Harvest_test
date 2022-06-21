using System;
using Player;
using UnityEngine;

namespace MainCamera
{
    public class CameraMover : MonoBehaviour
    {
        private const double RotationAngle = 50 * Math.PI / 180;
        private readonly Vector3 _offset = new Vector3(0f, 7f, -7f);
        private PlayerView _player;
        private Quaternion _rotationOffset;

        private void Start()
        {
            _player = FindObjectOfType<PlayerView>();
            _rotationOffset = new Quaternion((float)Math.Sin(RotationAngle / 2), 0f, 0f,
                (float)Math.Cos(RotationAngle / 2));
            transform.rotation = _rotationOffset * transform.rotation;
        }

        private void LateUpdate()
        {
            transform.position = _player.transform.position + _offset;
        }
    }
}