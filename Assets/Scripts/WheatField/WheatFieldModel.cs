using System.Collections.Generic;
using System.Linq;
using Base;
using Factories;
using UnityEngine;
using WheatField.Wheat;


namespace WheatField
{
    public class WheatFieldModel : BaseModel
    {
        private List<List<WheatModel>> _wheat;
        private WheatCubeFactory _wheatCubeFactory;
        private int _wheatHarvestedAmount;
        
        public readonly float[] _fieldSize = { 10f, 10f };

        public WheatFieldModel(WheatCubeFactory wheatCubeFactory)
        {
            _wheatCubeFactory = wheatCubeFactory;
        }

        public void CreateWheatCube(Vector3 position)
        {
            _wheatCubeFactory.CreateInstance(position);
        }

        private void OnWheatHarvested(Vector3 position)
        {
            _wheatHarvestedAmount++;

            if (_wheatHarvestedAmount >= 10)
            {
                CreateWheatCube(position);
                _wheatHarvestedAmount = 0;
            }
        }

        public void SetWheat(List<List<WheatModel>> wheat)
        {
            _wheat = wheat;

            foreach (var obj in _wheat.SelectMany(list => list))
            {
                obj.WheatHarvested += OnWheatHarvested;
            }
        }
    }
}