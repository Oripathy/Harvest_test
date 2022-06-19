using Base;
using Coins;
using UnityEngine;

namespace Factories
{
    public class CoinFactory
    {
        private CoinView _coinPrefab;
        private CoinUIModel _coinsModel;
        private UpdateHandler _updateHandler;

        public CoinFactory(CoinView coinPrefab, UpdateHandler updateHandler, CoinUIModel coinsModel)
        {
            _coinPrefab = coinPrefab;
            _updateHandler = updateHandler;
            _coinsModel = coinsModel;
        }

        public void CreateInstance(Vector3 position)
        {
            var view = GameObject.Instantiate(_coinPrefab, position, Quaternion.identity);
            var presenter = new CoinPresenter().Init<CoinPresenter>(_coinsModel, view, _updateHandler);
        }
    }
}