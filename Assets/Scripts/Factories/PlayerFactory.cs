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
        private readonly PlayerView _playerPrefab;
        private readonly UpdateHandler _updateHandler;
        private readonly PlayerUIView _playerUIView;
        private readonly Camera _uiCamera;
        private ScytheView _scytheView;
        private WheatDetectorView _detectorView;

        public PlayerFactory(PlayerView playerPrefab, UpdateHandler updateHandler, PlayerUIView playerUIView, Camera uiCamera)
        {
            _playerPrefab = playerPrefab;
            _updateHandler = updateHandler;
            _playerUIView = playerUIView;
            _uiCamera = uiCamera;
        }

        public PlayerModel CreateInstance(Vector3 initialPosition)
        {
            var view = Object.Instantiate(_playerPrefab, initialPosition, Quaternion.identity);
            _scytheView = view.gameObject.GetComponentInChildren<ScytheView>();
            _detectorView = view.gameObject.GetComponentInChildren<WheatDetectorView>();
            var playerUIView = Object.Instantiate(_playerUIView).Init(_uiCamera);
            
            var scytheModel = new ScytheModel();
            var model = new PlayerModel(scytheModel);

            new ScythePresenter().Init<ScythePresenter>(scytheModel, _scytheView, _updateHandler);
            new PlayerPresenter().Init<PlayerPresenter>(model, view, _updateHandler);
            new WheatDetectorPresenter().Init<WheatDetectorPresenter>(model, _detectorView, _updateHandler);
            new PlayerUIPresenter().Init<PlayerUIPresenter>(model, playerUIView, _updateHandler);
            
            model.Init();
            return model;
        }
    }
}