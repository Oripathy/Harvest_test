using Base;
using Player;
using Player.Scythe;
using Player.WheatDetector;
using UnityEngine;

namespace Factories
{
    public class PlayerFactory
    {
        private PlayerView _playerPrefab;
        private UpdateHandler _updateHandler;
        private ScytheView _scytheView;
        private WheatDetectorView _detectorView;

        public PlayerFactory(PlayerView playerPrefab, UpdateHandler updateHandler)
        {
            _playerPrefab = playerPrefab;
            _updateHandler = updateHandler;
        }

        public PlayerModel CreateInstance(Vector3 initialPosition)
        {
            var view = GameObject.Instantiate(_playerPrefab, initialPosition, Quaternion.identity);
            _scytheView = view.gameObject.GetComponentInChildren<ScytheView>();
            _detectorView = view.gameObject.GetComponentInChildren<WheatDetectorView>();
            
            var scytheModel = new ScytheModel();
            var scythePresenter = new ScythePresenter().Init<ScythePresenter>(scytheModel, _scytheView, _updateHandler);
            var model = new PlayerModel(scytheModel);
            var presenter = new PlayerPresenter().Init<PlayerPresenter>(model, view, _updateHandler);
            var detectorPresenter = new WheatDetectorPresenter().Init<WheatDetectorPresenter>(model, _detectorView, _updateHandler);
            return model;
        }
    }
}