using System.Threading.Tasks;
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
            _view.Collider.isTrigger = false;
            await MoveToBag(position, bag);
        }

        private async Task MoveToBag(Vector3 position, Transform bag)
        {
            var startTime = Time.time;
            _view.Transform.parent = bag;
            var initialPosition = _view.Transform.position;
            var initialRotation = _view.Transform.rotation;

            while (Time.time <= startTime + _model.MovementTime)
            {
                _view.Transform.localPosition = Vector3.Lerp(initialPosition, bag.position,
                    (Time.time - startTime) / _model.MovementTime);
                _view.Transform.rotation = Quaternion.Lerp(initialRotation, bag.rotation,
                    (Time.time - startTime) / _model.MovementTime);
                await Task.Yield();
            }
        }
    }
}