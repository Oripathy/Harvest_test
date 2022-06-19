using Base;

namespace Coins
{
    public class CoinUIPresenter : BasePresenter<CoinUIModel, ICoinUIView>
    {
        public override TPresenter Init<TPresenter>(CoinUIModel model, ICoinUIView view, UpdateHandler updateHandler)
        {
            base.Init<TPresenter>(model, view, updateHandler);

            return this as TPresenter;
        }
    }
}