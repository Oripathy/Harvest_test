using System;
using Base;
using UnityEngine;

namespace InputHandler
{
    public interface IInputHandlerView : IBaseView
    {
        public event Action<Vector3> DirectionReceived;
    }
}