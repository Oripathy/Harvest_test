using Base;
using UnityEngine;

namespace Factories
{
    public abstract class Factory<TModel, TView, TIView, TPresenter>
        where TModel : BaseModel, new()
        where TIView : IBaseView
        where TView : MonoBehaviour, TIView
        where TPresenter: BasePresenter<TModel, TIView>, new()
    {
        private protected UpdateHandler _updateHandler;
        private protected TView _viewPrefab;

        protected Factory(UpdateHandler updateHandler, TView viewPrefab)
        {
            _updateHandler = updateHandler;
            _viewPrefab = viewPrefab;
        }

        public virtual TModel CreateInstance(Vector3 position)
        {
            var view = GameObject.Instantiate(_viewPrefab, position, Quaternion.identity);
            var model = new TModel();
            var presenter = new TPresenter().Init<TPresenter>(model, view, _updateHandler);
            model.Init();
            return model;
        }
    }
}