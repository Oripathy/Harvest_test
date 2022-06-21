using Base;
using ObjectPools;
using UnityEngine;

namespace Factories
{
    public abstract class Factory<TModel, TView, TIView, TPresenter>
        where TModel : BaseModel, IObjectToPool, new()
        where TIView : IBaseView
        where TView : MonoBehaviour, TIView
        where TPresenter: BasePresenter<TModel, TIView>, new()
    {
        private protected readonly UpdateHandler _updateHandler;
        private protected readonly TView _viewPrefab;

        protected Factory(UpdateHandler updateHandler, TView viewPrefab)
        {
            _updateHandler = updateHandler;
            _viewPrefab = viewPrefab;
        }

        public virtual TModel CreateInstance()
        {
            var view = Object.Instantiate(_viewPrefab);
            view.gameObject.SetActive(false);
            var model = new TModel();
            new TPresenter().Init<TPresenter>(model, view, _updateHandler);
            model.Init();
            return model;
        }

        public virtual TModel CreateInstance(Transform parent, RectTransform destinationPosition)
        {
            var view = Object.Instantiate(_viewPrefab, parent, false);
            var model = new TModel();
            new TPresenter().Init<TPresenter>(model, view, _updateHandler);
            model.Init();
            return model;
        }
    }
}