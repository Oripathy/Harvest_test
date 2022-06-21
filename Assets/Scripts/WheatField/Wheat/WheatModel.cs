using System;
using Base;
using UnityEngine;

namespace WheatField.Wheat
{
    public class WheatModel : BaseModel
    {
        public float GrowUpTime { get; private set; }
        public Vector4 InitialColor { get; private set; }
        public Vector4 GrownUpColor { get; private set; }

        public event Action<Vector3> WheatHarvested; 

        public new WheatModel Init()
        {
            InitialColor = new Vector4(0f, 1f, 0f, 1f);
            GrownUpColor = new Vector4(1f, 0.78f, 0f, 1f);
            GrowUpTime = 10f;
            return this;
        }

        public void OnHarvested(Vector3 position)
        {
            WheatHarvested?.Invoke(position);
        }
    }
}