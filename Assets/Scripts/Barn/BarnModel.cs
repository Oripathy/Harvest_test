using System;
using Base;
using UnityEngine;

namespace Barn
{
    public class BarnModel : BaseModel
    {
        public event Action<Vector3> CubeSold;
        
        public void OnCubeSold(Vector3 position)
        {
            CubeSold?.Invoke(position);
        }
    }
}