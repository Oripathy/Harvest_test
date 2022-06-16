using Base;
using InputHandler;
using Player;
using UnityEngine;

namespace Factories
{
    public class InputHandlerFactory
    {
        private InputHandlerView _viewPrefab;
        private UpdateHandler _updateHandler;

        public InputHandlerFactory(InputHandlerView viewPrefab, UpdateHandler updateHandler)
        {
            _viewPrefab = viewPrefab;
            _updateHandler = updateHandler;
        }

        public void CreateInstance(PlayerModel model)
        {
            var view = GameObject.Instantiate(_viewPrefab);
            var presenter = new InputHandlerPresenter().Init<InputHandlerPresenter>(model, view, _updateHandler);
        }
    }
}