using System.Collections.Generic;
using System.Linq;
using Base;
using ObjectPools;
using UnityEngine;
using WheatField.Wheat;
using WheatField.WheatCube;

namespace WheatField
{
    public class WheatFieldModel : BaseModel
    {
        private List<List<WheatModel>> _wheat;
        private readonly ObjectPool<WheatCubeModel, WheatCubeView, IWheatCubeView, WheatCubePresenter> _wheatCubePool;
        private int _wheatHarvestedAmount;
        
        public readonly float[] FieldSize = { 8.5f, 8.5f };

        public WheatFieldModel(ObjectPool<WheatCubeModel, WheatCubeView, IWheatCubeView, WheatCubePresenter> wheatCubePool)
        {
            _wheatCubePool = wheatCubePool;
        }

        private void CreateWheatCube(Vector3 position)
        {
            if (_wheatCubePool.TryReleaseObject(position, out var cube))
            {
                cube.ObjectShouldBeReturned += ReturnWheatCubeToPool;
            }
        }

        private void OnWheatHarvested(Vector3 position)
        {
            _wheatHarvestedAmount++;

            if (_wheatHarvestedAmount < 10) 
                return;
            
            CreateWheatCube(position + new Vector3(0f, 0.3f, 0f));
            _wheatHarvestedAmount = 0;
        }

        public void SetWheat(List<List<WheatModel>> wheat)
        {
            _wheat = wheat;

            foreach (var obj in _wheat.SelectMany(list => list))
            {
                obj.WheatHarvested += OnWheatHarvested;
            }
        }

        private void ReturnWheatCubeToPool(IObjectToPool wheatCube)
        {
            _wheatCubePool.ReturnToPool(wheatCube);
            wheatCube.ObjectShouldBeReturned -= ReturnWheatCubeToPool;
        }
    }
}