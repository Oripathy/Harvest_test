using Base;
using Player;
using UnityEngine;

namespace Factories
{
    public class PlayerFactory
    {
        private PlayerView _playerPrefab;
        private UpdateHandler _updateHandler;

        public PlayerFactory(PlayerView playerPrefab, UpdateHandler updateHandler)
        {
            _playerPrefab = playerPrefab;
            _updateHandler = updateHandler;
        }

        public PlayerModel CreateInstance(Vector3 initialPosition)
        {
            var view = GameObject.Instantiate(_playerPrefab, initialPosition, Quaternion.identity);
            var model = new PlayerModel();
            var presenter = new PlayerPresenter().Init<PlayerPresenter>(model, view, _updateHandler);
            return model;
        }
    }
}