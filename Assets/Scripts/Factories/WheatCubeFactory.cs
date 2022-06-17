using Base;
using UnityEngine;
using WheatField.WheatCube;

namespace Factories
{
    public class WheatCubeFactory
    {
        private WheatCubeView _wheatCubePrefab;
        private UpdateHandler _updateHandler;

        public WheatCubeFactory(WheatCubeView wheatCubePrefab, UpdateHandler updateHandler)
        {
            _wheatCubePrefab = wheatCubePrefab;
            _updateHandler = updateHandler;
        }

        public WheatCubeModel CreateInstance(Vector3 position)
        {
            var model = new WheatCubeModel();
            var view = GameObject.Instantiate(_wheatCubePrefab, position, Quaternion.identity);
            var presenter = new WheatCubePresenter().Init<WheatCubePresenter>(model, view, _updateHandler);
            model.Init();
            return model;
        }
    }
}