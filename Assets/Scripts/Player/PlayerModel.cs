using System;
using System.Collections.Generic;
using Base;
using Player.Scythe;
using UnityEngine;
using WheatField.Wheat;

namespace Player
{
    public class PlayerModel : BaseModel
    {
        private List<List<WheatModel>> _wheatInStack;

        public float MoveSpeed { get; private set; }
        public float RotationSpeed { get; private set; }
        public Vector3 MoveDirection { get; set; }
        public BaseState CurrentState { get; set; }
        public ScytheModel Scythe { get; private set; }

        public event Action WheatDetected;
        
        public event Action WheatNotDetected; 
        public PlayerModel(ScytheModel scythe)
        {
            MoveSpeed = 2f;
            RotationSpeed = 540f;
            Scythe = scythe;
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
    }
}
