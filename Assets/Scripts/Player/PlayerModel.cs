using System.Collections.Generic;
using Base;
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

        public PlayerModel()
        {
            MoveSpeed = 2f;
            RotationSpeed = 540f;
        }
    }
}
