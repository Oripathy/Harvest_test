using Base;
using UnityEngine;

namespace Player.States
{
    public class IdleState : BaseState
    {
        public IdleState(PlayerPresenter presenter, IPlayerView view, PlayerModel model, string animBoolName) : base(
            presenter, view, model, animBoolName)
        {
        }

        private protected override void SwitchState()
        {
            if (_model.MoveDirection != Vector3.zero)
                _presenter.SwitchState<MoveState>();
        }
    }
}