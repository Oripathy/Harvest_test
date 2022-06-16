using System;
using System.Collections.Generic;
using Base;
using UnityEngine;

namespace Player
{
    public class PlayerPresenter : BasePresenter<PlayerModel, IPlayerView>
    {
        private Dictionary<Type, BaseState> _statesToType;

        public void RotateAt(Vector3 point)
        {
            var destinationRotation = Quaternion.LookRotation(point, Vector3.up);
            _view.Transform.rotation = Quaternion.RotateTowards(_view.Transform.rotation, destinationRotation,
                _model.RotationSpeed * Time.deltaTime);
        }

        public T SwitchState<T>()
            where T: BaseState
        {
            var type = typeof(T);

            if (_statesToType.TryGetValue(type, out var state))
            {
                _model.CurrentState.OnExit();
                _model.CurrentState = state;
                _model.CurrentState.OnEnter();
                return _model.CurrentState as T;
            }

            return null;
        }
    }
}