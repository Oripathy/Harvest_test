using Base;

namespace Player.States
{
    public class HarvestingState : BaseState
    {
        public HarvestingState(PlayerPresenter presenter, IPlayerView view, PlayerModel model, string animBoolName) :
            base(presenter, view, model, animBoolName)
        {
        }

        private protected override void SwitchState()
        {
            throw new System.NotImplementedException();
        }
    }
}