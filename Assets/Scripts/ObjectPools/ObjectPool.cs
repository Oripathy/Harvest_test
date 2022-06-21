using System.Collections.Generic;
using Base;
using Factories;
using UnityEngine;

namespace ObjectPools
{
    public class ObjectPool<TModel, TView, TIView, TPresenter>
        where TModel : BaseModel, IObjectToPool, new()
        where TIView : IBaseView
        where TView : MonoBehaviour, TIView
        where TPresenter : BasePresenter<TModel, TIView>, new()
    {
        private Queue<IObjectToPool> _pool;
        private readonly int _poolCapacity;
        private readonly Factory<TModel, TView, TIView, TPresenter> _factory;

        public ObjectPool(int poolCapacity, Factory<TModel, TView, TIView, TPresenter> factory)
        {
            _poolCapacity = poolCapacity;
            _factory = factory;
        }

        public ObjectPool<TModel, TView, TIView, TPresenter> Init()
        {
            _pool = new Queue<IObjectToPool>();

            for (var i = 0; i < _poolCapacity; i++)
            {
                _pool.Enqueue(_factory.CreateInstance());
            }

            return this;
        }

        public ObjectPool<TModel, TView, TIView, TPresenter> Init(Transform parent, RectTransform destinationPosition)
        {
            _pool = new Queue<IObjectToPool>();

            for (var i = 0; i < _poolCapacity; i++)
            {
                _pool.Enqueue(_factory.CreateInstance(parent, destinationPosition));
            }

            return this;
        }

        public bool TryReleaseObject(Vector3 position, out IObjectToPool pooledObject)
        {
            if (_pool.Count > 0)
            {
                pooledObject = _pool.Dequeue();
                pooledObject.SetActive(true).SetPosition(position);
                return true;
            }

            pooledObject = null;
            return false;
        }

        public void ReturnToPool(IObjectToPool pooledObject)
        {
            _pool.Enqueue(pooledObject);
            pooledObject.SetActive(false);
        }
    }
}