using System;
using Base;
using UnityEngine;

namespace WheatField.WheatCube
{
    public class WheatCubeModel : BaseModel
    {
        private readonly float _rotationAngle = 0.01f;
        
        public Vector3 InitialScale { get; private set; }
        public Vector3 InBagScale { get; private set; }
        public Quaternion Rotation { get; private set; }
        public bool IsCollected { get; set; }
        public float MovementTime { get; private set; }

        public event Action IsAppeared;

        public void Init()
        {
            IsAppeared?.Invoke();
        }

        public WheatCubeModel()
        {
            MovementTime = 0.5f;
            InitialScale = new Vector3(0.3f, 0.15f, 0.15f);
            InBagScale = new Vector3(0.1f, 0.05f, 0.05f);
            Rotation = new Quaternion(0f, (float) Math.Sin(_rotationAngle / 2), 0f,
                (float) Math.Cos(_rotationAngle / 2));
        }
    }
}