using System.Threading.Tasks;
using Barn;
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
            _view.CubeCollected += StartMovement;
            _view.EnteredSellZone += StartMovingToSellZone;
            return this as TPresenter;
        }
        
        private async void StartRotation()
        {
            await Rotate();
        }

        private async Task Rotate()
        {
            _view.Transform.localScale = _model.InitialScale;
            
            while (!_model.IsCollected)
            {
                _view.Transform.rotation *= _model.Rotation;
                await Task.Yield();
            }
        }

        private async void StartMovement(Vector3 position, Transform bag)
        {
            _model.IsCollected = true;
            _view.Collider.enabled = false;
            await MoveToBag(position, bag);
        }

        private async Task MoveToBag(Vector3 position, Transform bag)
        {
            var startTime = Time.time;
            _view.Transform.parent = bag;
            var initialPosition = _view.Transform.localPosition;
            var initialRotation = _view.Transform.localRotation;

            while (Time.time <= startTime + _model.MovementTime)
            {
                _view.Transform.localPosition = Vector3.Lerp(initialPosition, position,
                    (Time.time - startTime) / _model.MovementTime);
                _view.Transform.localRotation = Quaternion.Lerp(initialRotation, bag.localRotation,
                (Time.time - startTime) / _model.MovementTime);
                await Task.Yield();
            }

            _view.Transform.localPosition = position;
            _view.Transform.localRotation = bag.transform.localRotation;
        }

        private async void StartMovingToSellZone(Vector3 position)
        {
            await MoveToSellPoint(position);
        }

        private async Task MoveToSellPoint(Vector3 position)
        {
            var initialPosition = _view.Transform.position;
            var startTime = Time.time;

            while (Time.time < startTime + _model.MovementTime)
            {
                _view.Transform.position = Vector3.Lerp(initialPosition, position,
                    (Time.time - startTime) / _model.MovementTime);
                await Task.Yield();
            }
            
            _view.DestroyCube();
        }

        private void OnSold(SellPointView sellPoint)
        {
            
        }
    }
}