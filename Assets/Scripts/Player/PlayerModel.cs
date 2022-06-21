using System;
using System.Collections.Generic;
using System.Threading;
using Base;
using Player.Scythe;
using UnityEngine;
using WheatField.WheatCube;

namespace Player
{
    public class PlayerModel : BaseModel
    {
        private readonly ScytheModel _scythe;
        private readonly List<List<Vector3>> _bagSlots; 
        private readonly Stack<ICollectable> _collectables = new Stack<ICollectable>();
        private readonly Vector3 _bagPosition = new Vector3(0f, -0.133f, -0.15f);
        private const int MaxCollectablesAmount = 40;

        public float MoveSpeed { get; }
        public float RotationSpeed { get; }
        public Vector3 MoveDirection { get; set; }
        public BaseState CurrentState { get; set; }

        public event Action WheatDetected;
        public event Action WheatNotDetected;
        public event Action<int, int> CollectablesAmountChanged;
        
        public PlayerModel(ScytheModel scythe)
        {
            MoveSpeed = 2f;
            RotationSpeed = 720f;
            _scythe = scythe;
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

        public override void Init()
        {
            CollectablesAmountChanged?.Invoke(_collectables.Count, MaxCollectablesAmount);
            OnWheatNotDetected();
        }

        public void OnWheatDetected()
        {
            WheatDetected?.Invoke();
            _scythe.IsActive = true;
        }

        public void OnWheatNotDetected()
        {
            WheatNotDetected?.Invoke();
            _scythe.IsActive = false;
        }

        public Vector3 AddCollectable(ICollectable collectable)
        {
            _collectables.Push(collectable);
            CollectablesAmountChanged?.Invoke(_collectables.Count, MaxCollectablesAmount);

            switch (_collectables.Count % 4)
            {
                case 0:
                    var prevSlot = _bagSlots[1][1];
                    
                    foreach (var slot in _bagSlots)
                    {
                        slot[0] += new Vector3(0f, 0.15f, 0f);
                        slot[1] += new Vector3(0f, 0.15f, 0f);
                    }

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

                var collectable =  _collectables.Pop();
                CollectablesAmountChanged?.Invoke(_collectables.Count, MaxCollectablesAmount);
                return collectable;
            }
            
            return null;
        }

        public bool IsStackFull() => _collectables.Count >= MaxCollectablesAmount;

        public bool IsStackEmpty() => _collectables.Count <= 0;
    }
}
