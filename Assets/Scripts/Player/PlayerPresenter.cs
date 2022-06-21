using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Barn;
using Base;
using Player.States;
using UnityEngine;
using WheatField.WheatCube;

namespace Player
{
    public class PlayerPresenter : BasePresenter<PlayerModel, IPlayerView>
    {
        private Dictionary<Type, BaseState> _statesToType;
        private static readonly int Harvest = Animator.StringToHash("Harvest");

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
            _view.EnteredSellZone += OnEnteredSellZone;
            _view.ExitSellZone += OnExitSellZone;
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
            _view.Animator.SetBool(Harvest, true);
        }

        private void OnWheatNotDetected()
        {
            _view.Animator.SetBool(Harvest, false);
        }

        private void OnCollidedWithCollectable(ICollectable collectable)
        {
            if (!_model.IsStackFull())
            {
                var position = _view.Bag.localRotation * _model.AddCollectable(collectable);
                collectable.Collect(position, _view.Bag);
            }
        }

        private async void OnEnteredSellZone(BarnView barn)
        {
            if (_model.IsStackEmpty()) 
                return;
            
            var position = barn.SellPoint;
            var token = _model.CreateCancellationTokenSource().Token;
            await SellCollectable(position, token);
        }

        private async Task SellCollectable(Vector3 position, CancellationToken token)
        {
            while (!_model.IsStackEmpty())
            {
                if (token.IsCancellationRequested)
                {
                    return;
                }
                
                var collectable = _model.ReleaseCollectable();
                collectable.Sell(position);
                await Task.Delay(50, token);
            }
        }
        
        private void OnExitSellZone()
        {
            if (_model.IsStackEmpty())
                return;
            
            _model.DisposeSource();
        }

        public override void Dispose()
        {
            _model.WheatDetected -= OnWheatDetected;
            _model.WheatNotDetected -= OnWheatNotDetected;
            _view.CollidedWithCollectable -= OnCollidedWithCollectable;
            _view.EnteredSellZone -= OnEnteredSellZone;
            _view.ExitSellZone -= OnExitSellZone;
            base.Dispose();
        }
    }
}