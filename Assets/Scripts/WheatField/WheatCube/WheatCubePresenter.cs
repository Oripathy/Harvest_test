using System.Threading;
using System.Threading.Tasks;
using Barn.SellPoint;
using Base;
using UnityEngine;

namespace WheatField.WheatCube
{
    public class WheatCubePresenter : BasePresenter<WheatCubeModel, IWheatCubeView>
    {
        public override TPresenter Init<TPresenter>(WheatCubeModel model, IWheatCubeView view, UpdateHandler updateHandler)
        {
            base.Init<TPresenter>(model, view, updateHandler);
            _model.IsAppeared += StartRotation;
            _model.ActiveStateChanged += OnActiveStateChanged;
            _model.CubePlaced += OnCubePlaced;
            _view.CubeCollected += StartMovingToBag;
            _view.EnteredSellZone += StartMovingToSellPoint;
            _view.Sold += OnSold;
            return this as TPresenter;
        }

        private void OnActiveStateChanged(bool isActive)
        {
            _view.SetCubeActive(isActive);
        }

        private void OnCubePlaced(Vector3 position)
        {
            _view.Transform.position = position;
        }
                
        private async void StartRotation()
        {
            var token = _model.Source?.Token ?? _model.CreateCancellationTokenSource().Token;
            await Rotate(token);
        }

        private async Task Rotate(CancellationToken token)
        {
            _model.IsCollected = false;
            _view.Transform.localScale = _model.InitialScale;
            var startTime = Time.time;

            while (!_model.IsCollected)
            {
                if (token.IsCancellationRequested)
                {
                    return;
                }
                
                if (Time.time >= startTime + _model.ActiveTime)
                {
                    _model.OnObjectShouldBeReturned();
                    _view.Transform.localScale = _model.InitialScale;
                    break;
                }
                
                _view.Transform.rotation *= _model.Rotation;
                await Task.Yield();
            }
        }

        private async void StartMovingToBag(Vector3 position, Transform bag)
        {
            var token = _model.Source?.Token ?? _model.CreateCancellationTokenSource().Token;
            _model.IsCollected = true;
            _view.Collider.enabled = false;
            await MoveToBag(position, bag, token);
        }

        private async Task MoveToBag(Vector3 position, Transform bag, CancellationToken token)
        {
            var startTime = Time.time;
            _view.Transform.parent = bag;
            var initialPosition = _view.Transform.localPosition;
            var initialRotation = _view.Transform.localRotation;

            while (Time.time <= startTime + _model.MovementTime)
            {
                if (token.IsCancellationRequested)
                {
                    return;
                }
                
                _view.Transform.localPosition = Vector3.Lerp(initialPosition, position,
                    (Time.time - startTime) / _model.MovementTime);
                _view.Transform.localRotation = Quaternion.Lerp(initialRotation, bag.localRotation,
                (Time.time - startTime) / _model.MovementTime);
                _view.Transform.localScale = Vector3.Lerp(_model.InitialScale, _model.InBagScale,
                    (Time.time - startTime) / _model.MovementTime);
                await Task.Yield();
            }

            _view.Transform.localPosition = position;
            _view.Transform.localRotation = bag.transform.localRotation;
            _view.Transform.localScale = _model.InBagScale;
        }

        private async void StartMovingToSellPoint(Vector3 position)
        {
            var token = _model.Source?.Token ?? _model.CreateCancellationTokenSource().Token;
            await MoveToSellPoint(position, token);
            _model.OnObjectShouldBeReturned();
        }

        private async Task MoveToSellPoint(Vector3 position, CancellationToken token)
        {
            _view.Transform.parent = null;
            var initialPosition = _view.Transform.position;
            var startTime = Time.time;

            while (Time.time < startTime + _model?.MovementTime)
            {
                if (token.IsCancellationRequested)
                {
                    return;
                }
                
                _view.Transform.position = Vector3.Lerp(initialPosition, position,
                    (Time.time - startTime) / _model.MovementTime);
                await Task.Yield();
            }

            _view.Transform.position = position;
            _view.Collider.enabled = true;
            await Task.Delay(50, token);
        }

        private void OnSold(SellPointView sellPoint)
        {
            sellPoint.OnCubeSold();
        }

        public override void Dispose()
        {
            _model.IsAppeared -= StartRotation;
            _model.ActiveStateChanged -= OnActiveStateChanged;
            _model.CubePlaced -= OnCubePlaced;
            _view.CubeCollected -= StartMovingToBag;
            _view.EnteredSellZone -= StartMovingToSellPoint;
            _view.Sold -= OnSold;
            base.Dispose();
        }
    }
}