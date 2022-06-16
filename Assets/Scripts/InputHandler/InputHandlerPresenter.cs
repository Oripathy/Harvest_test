using Base;
using Player;
using UnityEngine;

namespace InputHandler
{
    public class InputHandlerPresenter : BasePresenter<PlayerModel, IInputHandlerView>
    {
        public override void Init(PlayerModel model, IInputHandlerView view, UpdateHandler updateHandler)
        {
            base.Init(model, view, updateHandler);
            _view.DirectionReceived += OnDirectionReceived;
        }

        private void OnDirectionReceived(Vector3 direction)
        {
            
        }

        public override void Dispose()
        {
            base.Dispose();
            _view.DirectionReceived -= OnDirectionReceived;
        }
    }
}