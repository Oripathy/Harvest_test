using System;
using UnityEngine;
using UnityEngine.UI;

namespace InputHandler
{
    public class InputHandlerView : MonoBehaviour, IInputHandlerView
    {
        [SerializeField] private GameObject _joystick;
        [SerializeField] private GameObject _outerJoystickObj;
        [SerializeField] private GameObject _innerJoystickObj;
        [SerializeField] private Camera _camera;

        private Image _outerJoystick;
        private Image _innerJoystick;
        private bool _isTouched;
        private Vector3 _moveDirection;
        private Vector2 _startPosition;

        public event Action<Vector3> DirectionReceived;

        private void Awake()
        {
            _outerJoystick = _outerJoystickObj.GetComponent<Image>();
            _innerJoystick = _innerJoystickObj.GetComponent<Image>();
            _joystick.SetActive(false);
            _camera = Camera.main.transform.GetChild(0).GetComponent<Camera>();
            GetComponent<Canvas>().worldCamera = _camera;
            Debug.Log(GetComponent<Canvas>().worldCamera);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _isTouched = true;
                _joystick.SetActive(true);
                _startPosition = new Vector2
                {
                    x = Input.mousePosition.x / _outerJoystick.rectTransform.sizeDelta.x,
                    y = Input.mousePosition.y / _outerJoystick.rectTransform.sizeDelta.y
                };
                _outerJoystick.rectTransform.anchoredPosition = Input.mousePosition;
                Debug.Log(_outerJoystick.rectTransform.anchoredPosition + " " + Input.mousePosition);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                _isTouched = false;
                _joystick.SetActive(false);
                _moveDirection = Vector3.zero;
                _startPosition = Vector2.zero;
                DirectionReceived?.Invoke(_moveDirection);
            }

            if (_isTouched)
            {
                var position = new Vector2
                {
                    x = Input.mousePosition.x / _outerJoystick.rectTransform.sizeDelta.x,
                    y = Input.mousePosition.y / _outerJoystick.rectTransform.sizeDelta.y
                    // x = Input.mousePosition.x,
                    // y = Input.mousePosition.y
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