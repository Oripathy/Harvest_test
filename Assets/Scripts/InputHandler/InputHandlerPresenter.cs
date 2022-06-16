using Base;
using Player;
using UnityEngine;

namespace InputHandler
{
    public class InputHandlerPresenter : BasePresenter<PlayerModel, IInputHandlerView>
    {
        public override TPresenter Init<TPresenter>(PlayerModel model, IInputHandlerView view, UpdateHandler updateHandler)
        {
            base.Init<TPresenter>(model, view, updateHandler);
            _view.DirectionReceived += OnDirectionReceived;
            return this as TPresenter;
        }

        private void OnDirectionReceived(Vector3 direction)
        {
            _model.MoveDirection = direction;
        }

        public override void Dispose()
        {
            base.Dispose();
            _view.DirectionReceived -= OnDirectionReceived;
        }
    }
}