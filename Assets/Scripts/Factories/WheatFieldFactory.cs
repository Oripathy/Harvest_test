using System.Collections.Generic;
using Base;
using UnityEngine;
using WheatField;
using WheatField.Wheat;

namespace Factories
{
    public class WheatFieldFactory
    {
        private WheatFieldView _viewPrefab;
        private UpdateHandler _updateHandler;
        private WheatFactory _wheatFactory;
        private WheatCubeFactory _wheatCubeFactory;

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
            var model = new WheatFieldModel(_wheatCubeFactory);
            var view = GameObject.Instantiate(_viewPrefab, position, Quaternion.identity);
            var presenter = new WheatFieldPresenter().Init<WheatFieldPresenter>(model, view, _updateHandler);
            
            var wheat = new List<List<WheatModel>>();
            var wheatPosition = new Vector3(4f, 0f, 4f);
            var step = 0.5f;

            for (var i = 0; i < model._fieldSize[0] / step - 1; i++)
            {
                wheatPosition.z = 4f;
                wheat.Add(new List<WheatModel>());
                
                for (var j = 0; j < model._fieldSize[1] / step - 1; j++)
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