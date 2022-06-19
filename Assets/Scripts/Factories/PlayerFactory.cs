using Base;
using Player;
using Player.PlayerUI;
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
        private PlayerUIView _playerUIView;

        public PlayerFactory(PlayerView playerPrefab, UpdateHandler updateHandler, PlayerUIView playerUIView)
        {
            _playerPrefab = playerPrefab;
            _updateHandler = updateHandler;
            _playerUIView = playerUIView;
        }

        public PlayerModel CreateInstance(Vector3 initialPosition)
        {
            var view = GameObject.Instantiate(_playerPrefab, initialPosition, Quaternion.identity);
            _scytheView = view.gameObject.GetComponentInChildren<ScytheView>();
            _detectorView = view.gameObject.GetComponentInChildren<WheatDetectorView>();
            var playerUIView = GameObject.Instantiate(_playerUIView);
            
            var scytheModel = new ScytheModel();
            var model = new PlayerModel(scytheModel);

            var scythePresenter = new ScythePresenter().Init<ScythePresenter>(scytheModel, _scytheView, _updateHandler);
            var presenter = new PlayerPresenter().Init<PlayerPresenter>(model, view, _updateHandler);
            var detectorPresenter = new WheatDetectorPresenter().Init<WheatDetectorPresenter>(model, _detectorView, _updateHandler);
            var playerUIPresenter = new PlayerUIPresenter().Init<PlayerUIPresenter>(model, playerUIView, _updateHandler);
            
            model.Init();
            return model;
        }
    }
}