using System;
using Base;
using UnityEngine;

namespace WheatField.Wheat
{
    public interface IWheatView : IBaseView
    {
        public Transform Transform { get; }
        public Collider Collider { get; }

        public MeshRenderer MeshRenderer { get; }

        public event Action WheatHarvested;
    }
}