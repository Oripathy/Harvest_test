using System;
using UnityEngine;
using UnityEngine.UI;

namespace InputHandler
{
    public class InputHandlerView : MonoBehaviour, IInputHandlerView
    {
        [SerializeField] private GameObject _joystick;
        [SerializeField] private Image _outerJoystick;
        [SerializeField] private Image _innerJoystick;
        [SerializeField] private Camera _camera;

        private bool _isTouched;
        private Vector3 _moveDirection;
        private Vector2 _startPosition;
        
        public event Action<Vector3> DirectionReceived;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _isTouched = true;
                _joystick.SetActive(true);
                _outerJoystick.rectTransform.anchoredPosition = Input.mousePosition;
                _startPosition = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                _isTouched = false;
                _joystick.SetActive(false);
                _moveDirection = Vector3.zero;
                DirectionReceived?.Invoke(_moveDirection);
            }

            if (_isTouched)
            {
                var position = new Vector2
                {
                    x = Input.mousePosition.x / _outerJoystick.rectTransform.sizeDelta.x,
                    y = Input.mousePosition.y / _outerJoystick.rectTransform.sizeDelta.y
                };
                
                _moveDirection = new Vector3(position.x - _startPosition.x, 0f, position.y - _startPosition.y);
                
                if (_moveDirection.magnitude > 1f)
                    _moveDirection.Normalize();

                _innerJoystick.rectTransform.anchoredPosition = new Vector2(
                    _moveDirection.x * _outerJoystick.rectTransform.sizeDelta.x / 2,
                    _moveDirection.z * _outerJoystick.rectTransform.sizeDelta.y / 2);
                
                DirectionReceived?.Invoke(_moveDirection);
            }
        }
    }
}