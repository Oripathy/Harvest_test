using System.Collections.Generic;
using Base;
using ObjectPools;
using UnityEngine;
using WheatField;
using WheatField.Wheat;
using WheatField.WheatCube;

namespace Factories
{
    public class WheatFieldFactory
    {
        private readonly WheatFieldView _viewPrefab;
        private readonly UpdateHandler _updateHandler;
        private readonly WheatFactory _wheatFactory;
        private readonly WheatCubeFactory _wheatCubeFactory;

        public WheatFieldFactory(UpdateHandler updateHandler, WheatFieldView viewPrefab1, WheatFactory wheatFactory,
            WheatCubeFactory wheatCubeFactory)
        {
            _updateHandler = updateHandler;
            _viewPrefab = viewPrefab1;
            _wheatFactory = wheatFactory;
            _wheatCubeFactory = wheatCubeFactory;
        }

        public WheatFieldModel CreateInstance(Vector3 position)
        {
            var view = Object.Instantiate(_viewPrefab, position, Quaternion.identity);
            var wheatCubePool = new ObjectPool<WheatCubeModel, WheatCubeView, IWheatCubeView, WheatCubePresenter>(80, _wheatCubeFactory).Init();
            var model = new WheatFieldModel(wheatCubePool);
            new WheatFieldPresenter().Init<WheatFieldPresenter>(model, view, _updateHandler);
            
            var wheat = new List<List<WheatModel>>();
            var wheatPosition = new Vector3(4f, 0f, 4f);
            const float step = 0.5f;

            for (var i = 0; i < model.FieldSize[0] / step; i++)
            {
                wheatPosition.z = 4f;
                wheat.Add(new List<WheatModel>());
                
                for (var j = 0; j < model.FieldSize[1] / step; j++)
                {
                    wheat[i].Add(_wheatFactory.CreateInstance(wheatPosition, view.transform));
                    wheatPosition.z -= step;
                }

                wheatPosition.x -= step;
            }
            
            model.SetWheat(wheat);
            return model;
        }
    }
}