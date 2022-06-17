using Base;
using UnityEngine;
using WheatField.Wheat;

namespace Factories
{
    public class WheatFactory
    {
        private WheatView _viewPrefab;
        private UpdateHandler _updateHandler;
        private WheatCubeFactory _wheatCubeFactory;
        
        public WheatFactory(UpdateHandler updateHandler, WheatView viewPrefab, WheatCubeFactory wheatCubeFactory)
        {
            _updateHandler = updateHandler;
            _viewPrefab = viewPrefab;
            _wheatCubeFactory = wheatCubeFactory;
        }

        public WheatModel CreateInstance(Vector3 position, Transform parent)
        {
            var model = new WheatModel().Init(_wheatCubeFactory);
            var view = GameObject.Instantiate(_viewPrefab, parent, false);
            view.transform.localPosition = position;
            var presenter = new WheatPresenter().Init<WheatPresenter>(model, view, _updateHandler);
            return model;
        }
    }
}