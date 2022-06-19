﻿using System;
using System.Runtime.CompilerServices;
using Base;
using UnityEngine;

namespace Barn.SellPoint
{
    public interface ISellPointView : IBaseView
    {
        public Transform Transform { get; }

        public event Action CubeSold;
    }
}