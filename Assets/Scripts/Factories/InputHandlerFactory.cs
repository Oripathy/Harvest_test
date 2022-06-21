using Base;
using Player;
using Player.InputHandler;
using UnityEngine;

namespace Factories
{
    public class InputHandlerFactory
    {
        private readonly InputHandlerView _viewPrefab;
        private readonly UpdateHandler _updateHandler;
        private readonly Camera _uiCamera;

        public InputHandlerFactory(InputHandlerView viewPrefab, UpdateHandler updateHandler, Camera uiCamera)
        {
            _viewPrefab = viewPrefab;
            _updateHandler = updateHandler;
            _uiCamera = uiCamera;
        }

        public void CreateInstance(PlayerModel model)
        {
            var view = Object.Instantiate(_viewPrefab).Init(_uiCamera);
            new InputHandlerPresenter().Init<InputHandlerPresenter>(model, view, _updateHandler);
        }
    }
}