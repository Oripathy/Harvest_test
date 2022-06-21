using Base;
using WheatField.Wheat;

namespace Player.Scythe
{
    public class ScythePresenter : BasePresenter<ScytheModel, IScytheView>
    {
        public override TPresenter Init<TPresenter>(ScytheModel model, IScytheView view, UpdateHandler updateHandler)
        {
            base.Init<TPresenter>(model, view, updateHandler);
            _view.WheatHarvested += OnWheatHarvested;
            _model.IsActiveChanged += OnIsActiveChanged;
            return this as TPresenter;
        }

        private void OnWheatHarvested(IHarvestable harvestable)
        {
            harvestable.Harvest();
        }

        private void OnIsActiveChanged(bool isActive)
        {
            _view.SetScytheActive(isActive);
        }

        public override void Dispose()
        {
            _view.WheatHarvested -= OnWheatHarvested;
            _model.IsActiveChanged -= OnIsActiveChanged;
            base.Dispose();
        }
    }
}