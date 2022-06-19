using Base;

namespace Player.PlayerUI
{
    public class PlayerUIPresenter : BasePresenter<PlayerModel, IPlayerUIView>
    {
        public override TPresenter Init<TPresenter>(PlayerModel model, IPlayerUIView view, UpdateHandler updateHandler)
        {
            base.Init<TPresenter>(model, view, updateHandler);
            _model.CollectablesAmountChanged += OnCollectablesAmountChanged;
            return this as TPresenter;
        }

        private void OnCollectablesAmountChanged(int collectablesAmount, int maxCollectablesAmount)
        {
            _view.UpdateCollectableAmount(collectablesAmount, maxCollectablesAmount);
        }
    }
}