using System;
using Base;
using ObjectPools;
using UnityEngine;

namespace WheatField.WheatCube
{
    public class WheatCubeModel : BaseModel, IObjectToPool
    {
        private const float RotationAngle = 0.01f;

        public Vector3 InitialScale { get; }
        public Vector3 InBagScale { get; }
        public Quaternion Rotation { get; }
        public bool IsCollected { get; set; }
        public float ActiveTime { get; }
        public float MovementTime { get; }

        public event Action IsAppeared;
        public event Action<bool> ActiveStateChanged;
        public event Action<IObjectToPool> ObjectShouldBeReturned;
        public event Action<Vector3> CubePlaced; 

        public WheatCubeModel()
        {
            ActiveTime = 10f;
            MovementTime = 0.5f;
            InitialScale = new Vector3(0.6f, 0.3f, 0.3f);
            InBagScale = new Vector3(0.3f, 0.15f, 0.15f);
            Rotation = new Quaternion(0f, (float) Math.Sin(RotationAngle / 2), 0f,
                (float) Math.Cos(RotationAngle / 2));
        }

        public IObjectToPool SetActive(bool isActive)
        {
            ActiveStateChanged?.Invoke(isActive);
            return this;
        }

        public void SetPosition(Vector3 position)
        {
            CubePlaced?.Invoke(position);
            IsAppeared?.Invoke();
        }

        public void OnObjectShouldBeReturned()
        {
            ObjectShouldBeReturned?.Invoke(this);
        }
    }
}