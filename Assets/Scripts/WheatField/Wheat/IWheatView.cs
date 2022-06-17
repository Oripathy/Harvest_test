using System;
using Base;
using UnityEngine;

namespace WheatField.Wheat
{
    public interface IWheatView : IBaseView
    {
        public Transform Transform { get; }
        public Collider Collider { get; }
        public GameObject GrownUpWheat { get; }
        public GameObject WheatLowerPart { get; }
        public GameObject WheatUpperPart { get; }

        public event Action WheatHarvested;
    }
}