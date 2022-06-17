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
        
        public WheatFieldFactory(UpdateHandler updateHandler, WheatFieldView viewPrefab1, WheatFactory wheatFactory)
        {
            _updateHandler = updateHandler;
            _viewPrefab = viewPrefab1;
            _wheatFactory = wheatFactory;
        }

        public WheatFieldModel CreateInstance(Vector3 position)
        {
            var model = new WheatFieldModel();
            var view = GameObject.Instantiate(_viewPrefab, position, Quaternion.identity);
            var presenter = new WheatFieldPresenter().Init<WheatFieldPresenter>(model, view, _updateHandler);
            
            var wheat = new List<List<WheatModel>>();
            // var wheatPosition = new Vector3(4f, 0.5f, 4f);
            var wheatPosition = new Vector3(4f, 0f, 4f);

            for (var i = 0; i < 5; i++)
            {
                wheatPosition.z = 4f;
                wheat.Add(new List<WheatModel>());
                
                for (var j = 0; j < 5; j++)
                {
                    wheat[i].Add(_wheatFactory.CreateInstance(wheatPosition, view.transform));
                    wheatPosition.z -= 2f;
                }

                wheatPosition.x -= 2f;
            }
            
            return model;
        }
    }
}