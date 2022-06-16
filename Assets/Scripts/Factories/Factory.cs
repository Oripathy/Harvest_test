using Base;
using UnityEngine;

namespace Factories
{
    public abstract class Factory<TModel, TView, TPresenter>
        where TModel : BaseModel, new()
        where TView : IBaseView
        where TPresenter: BasePresenter<TModel, TView>, new()
    {
        private protected UpdateHandler _updateHandler;
        private protected TView _viewPrefab;

        protected Factory(UpdateHandler updateHandler, TView viewPrefab)
        {
            _updateHandler = updateHandler;
            _viewPrefab = viewPrefab;
        }

        public abstract TModel CreateInstance(Vector3 position);
    }
}