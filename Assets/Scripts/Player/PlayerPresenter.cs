using System;
using System.Collections.Generic;
using Base;
using Player.States;
using UnityEngine;
using WheatField.WheatCube;

namespace Player
{
    public class PlayerPresenter : BasePresenter<PlayerModel, IPlayerView>
    {
        private Dictionary<Type, BaseState> _statesToType;

        public override TPresenter Init<TPresenter>(PlayerModel model, IPlayerView view, UpdateHandler updateHandler)
        {
            base.Init<TPresenter>(model, view, updateHandler);
            _statesToType = new Dictionary<Type, BaseState>
            {
                { typeof(IdleState), new IdleState(this, _view, _model, "Idle") },
                { typeof(MoveState), new MoveState(this, _view, _model, "Move") }
            };

            _model.CurrentState = _statesToType[typeof(IdleState)];
            _model.CurrentState.OnEnter();
            _model.WheatDetected += OnWheatDetected;
            _model.WheatNotDetected += OnWheatNotDetected;
            _view.CollidedWithCollectable += OnCollidedWithCollectable;
            return this as TPresenter;
        }

        public void RotateAt(Vector3 point)
        {
            var destinationRotation = Quaternion.LookRotation(point, Vector3.up);
            _view.Transform.rotation = Quaternion.RotateTowards(_view.Transform.rotation, destinationRotation,
                _model.RotationSpeed * Time.deltaTime);
        }

        private protected override void Update()
        {
            base.Update();
            _model.CurrentState.Update();
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

        private void OnWheatDetected()
        {
            _view.Animator.SetBool("Harvest", true);
        }

        private void OnWheatNotDetected()
        {
            _view.Animator.SetBool("Harvest", false);
        }

        private void OnCollidedWithCollectable(ICollectable collectable)
        {
            collectable.Collect(_view.Bag.position, _view.Bag);
        }
    }
}