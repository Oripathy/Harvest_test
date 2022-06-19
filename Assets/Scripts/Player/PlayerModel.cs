using System;
using System.Collections.Generic;
using System.Threading;
using Base;
using Player.Scythe;
using UnityEngine;
using WheatField.WheatCube;
//151
//127
namespace Player
{
    public class PlayerModel : BaseModel
    {
        private List<List<Vector3>> _bagSlots; 
        private Stack<ICollectable> _collectables = new Stack<ICollectable>();
        private Vector3 _bagPosition = new Vector3(0f, -0.133f, -0.15f);
        private int _stackHeigh;
        private readonly int _maxCollectablesAmount = 40;

        public CancellationTokenSource Source { get; private set; }
        public float MoveSpeed { get; private set; }
        public float RotationSpeed { get; private set; }
        public Vector3 MoveDirection { get; set; }
        public BaseState CurrentState { get; set; }
        public ScytheModel Scythe { get; private set; }

        public event Action WheatDetected;
        public event Action WheatNotDetected;
        public event Action<int, int> CollectablesAmountChanged;
        
        public PlayerModel(ScytheModel scythe)
        {
            MoveSpeed = 2f;
            RotationSpeed = 540f;
            Scythe = scythe;
            _bagSlots = new List<List<Vector3>>
            {
                new List<Vector3>
                {
                    _bagPosition - new Vector3(0.15f, 0f, 0f),
                    _bagPosition + new Vector3(0.15f, 0f, 0f)
                },
                new List<Vector3>
                {
                    _bagPosition - new Vector3(0.15f, 0f, 0.15f),
                    _bagPosition + new Vector3(0.15f, 0f, -0.15f),
                }
            };
        }

        public void Init()
        {
            CollectablesAmountChanged?.Invoke(_collectables.Count, _maxCollectablesAmount);
            OnWheatNotDetected();
        }

        public void OnWheatDetected()
        {
            WheatDetected?.Invoke();
            Scythe.IsActive = true;
        }

        public void OnWheatNotDetected()
        {
            WheatNotDetected?.Invoke();
            Scythe.IsActive = false;
        }

        public Vector3 AddCollectable(ICollectable collectable)
        {
            _collectables.Push(collectable);
            CollectablesAmountChanged?.Invoke(_collectables.Count, _maxCollectablesAmount);

            switch (_collectables.Count % 4)
            {
                case 0:
                    var prevSlot = _bagSlots[1][1];
                    
                    foreach (var slot in _bagSlots)
                    {
                        // var y0 = slot[0].y - 0.15f * _stackHeigh;
                        // var y1 = slot[1].y - 0.15f * _stackHeigh;
                        // slot[0] -= new Vector3(0f, y0, 0f);
                        // slot[1] -= new Vector3(0f, y1, 0f);
                        slot[0] += new Vector3(0f, 0.15f, 0f);
                        slot[1] += new Vector3(0f, 0.15f, 0f);
                    }

                    if (_collectables.Count != 0)
                        _stackHeigh++;

                    return prevSlot;

                case 1:
                    return _bagSlots[0][0];

                case 2:
                    return _bagSlots[0][1];

                case 3:
                    return _bagSlots[1][0];

                default:
                    return Vector3.zero;
            }
        }

        public ICollectable ReleaseCollectable()
        {
            if (_collectables.Count > 0)
            {
                if (_collectables.Count % 4 == 0)
                {
                    foreach (var slot in _bagSlots)
                    {
                        slot[0] -= new Vector3(0f, 0.15f, 0f);
                        slot[1] -= new Vector3(0f, 0.15f, 0f);
                    }
                }
                _stackHeigh = _collectables.Count / 4;
                var collectable =  _collectables.Pop();
                CollectablesAmountChanged?.Invoke(_collectables.Count, _maxCollectablesAmount);
                return collectable;
            }
            
            return null;
        }

        public bool IsStackFull() => _collectables.Count >= _maxCollectablesAmount;

        public bool IsStackEmpty() => _collectables.Count <= 0;

        public CancellationTokenSource CreateCancellationTokenSource()
        {
            return Source = new CancellationTokenSource();
        }
    }
}
