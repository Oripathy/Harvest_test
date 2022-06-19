﻿using System;
using Barn;
using Base;
using UnityEngine;
using WheatField.WheatCube;

namespace Player
{
    public interface IPlayerView : IBaseView
    {
        public Rigidbody Rigidbody { get; }
        public Transform Transform { get; }
        public Transform Bag { get; }
        public Animator Animator { get; }
        
        public event Action<ICollectable> CollidedWithCollectable;
        public event Action<BarnView> EnteredSellZone;
        public event Action ExitSellZone;
    }
}