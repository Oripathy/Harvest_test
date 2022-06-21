using System;
using System.Threading;
using Base;
using UnityEngine;

namespace WheatField.Wheat
{
    public class WheatModel : BaseModel
    {
        public float GrowUpTime { get; private set; }
        public Vector3 InitialScale { get; private set; }
        public Vector3 GrownUpScale { get; private set; }
        
        public event Action<Vector3> WheatHarvested; 

        public new WheatModel Init()
        {
            InitialScale = new Vector3(1f, 0.1f, 1f);
            GrownUpScale = new Vector3(1f, 1f, 1f);
            GrowUpTime = 10f;
            return this;
        }

        public void OnHarvested(Vector3 position)
        {
            WheatHarvested?.Invoke(position);
        }
    }
}