using System;
using Barn;
using Barn.SellPoint;
using Base;
using UnityEngine;

namespace WheatField.WheatCube
{
    public interface IWheatCubeView : IBaseView
    {
        public Transform Transform { get; }
        public Collider Collider { get; }
        
        public event Action<Vector3, Transform> CubeCollected;
        public event Action<Vector3> EnteredSellZone;
        public event Action<SellPointView> Sold;

        public void DestroyCube();
    }
}