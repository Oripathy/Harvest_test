using System;
using UnityEngine;
using UnityEngine.UI;

namespace Player.InputHandler
{
    public class InputHandlerView : MonoBehaviour, IInputHandlerView
    {
        [SerializeField] private GameObject _joystick;
        [SerializeField] private Image _outerJoystick;
        [SerializeField] private Image _innerJoystick;
        [SerializeField] private Canvas _canvas;
        
        private Camera _uiCamera;
        private Vector3 _moveDirection;
        private Vector2 _startPosition;
        private float _scale;
        private bool _isTouched;

        public event Action<Vector3> DirectionReceived;
        public event Action ObjectDestroyed;

        public InputHandlerView Init(Camera uiCamera)
        {
            _uiCamera = uiCamera;
            _canvas.worldCamera = _uiCamera;
            return this;
        }

        private void Awake()
        {
            _scale = _canvas.scaleFactor;
            _joystick.SetActive(false);
        }

        private void Update()
        {
            //HandleMobileInput();
            HandlePCInput();
        }

        private void HandlePCInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _isTouched = true;
                _joystick.SetActive(true);
                var sizeDelta = _outerJoystick.rectTransform.sizeDelta;
                _startPosition = new Vector2
                {
                    x = Input.mousePosition.x / sizeDelta.x / _scale,
                    y = Input.mousePosition.y / sizeDelta.y / _scale
                };
                _outerJoystick.rectTransform.anchoredPosition = Input.mousePosition / _scale;
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
                var sizeDelta = _outerJoystick.rectTransform.sizeDelta;
                var position = new Vector2
                {
                    x = Input.mousePosition.x / sizeDelta.x / _scale,
                    y = Input.mousePosition.y / sizeDelta.y / _scale
                };
                
                _moveDirection = new Vector3(position.x - _startPosition.x, 0f, position.y - _startPosition.y);
                
                if (_moveDirection.magnitude > 1f)
                    _moveDirection.Normalize();

                _innerJoystick.rectTransform.anchoredPosition = new Vector2(
                    _moveDirection.x * sizeDelta.x / 3,
                    _moveDirection.z * sizeDelta.y / 3);
                DirectionReceived?.Invoke(_moveDirection);
            }
        }

        private void HandleMobileInput()
        {
            var touch = Input.GetTouch(0);
            
            if (Input.touchCount > 0)
            {
                _isTouched = true;
                _joystick.SetActive(true);
                var sizeDelta = _outerJoystick.rectTransform.sizeDelta;
                _startPosition = new Vector2
                {
                    x = touch.position.x / sizeDelta.x / _scale,
                    y = touch.position.y / sizeDelta.y / _scale
                };
                _outerJoystick.rectTransform.anchoredPosition = touch.position / _scale;
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                _isTouched = false;
                _joystick.SetActive(false);
                _moveDirection = Vector3.zero;
                _startPosition = Vector2.zero;
                DirectionReceived?.Invoke(_moveDirection);
            }

            if (_isTouched)
            {
                var sizeDelta = _outerJoystick.rectTransform.sizeDelta;
                var position = new Vector2
                {
                    x = touch.position.x / sizeDelta.x / _scale,
                    y = touch.position.y / sizeDelta.y / _scale
                };
                
                _moveDirection = new Vector3(position.x - _startPosition.x, 0f, position.y - _startPosition.y);
                
                if (_moveDirection.magnitude > 1f)
                    _moveDirection.Normalize();

                _innerJoystick.rectTransform.anchoredPosition = new Vector2(
                    _moveDirection.x * sizeDelta.x / 3,
                    _moveDirection.z * sizeDelta.y / 3);
                DirectionReceived?.Invoke(_moveDirection);
            }
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            if (hasFocus)
                return;
            
            _isTouched = false;
            _joystick.SetActive(false);
            _moveDirection = Vector3.zero;
            _startPosition = Vector2.zero;
            DirectionReceived?.Invoke(_moveDirection);
        }

        private void OnDestroy()
        {
            ObjectDestroyed?.Invoke();
        }
    }
}