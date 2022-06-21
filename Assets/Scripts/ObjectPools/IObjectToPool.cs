using System;
using UnityEngine;

namespace ObjectPools
{
    public interface IObjectToPool
    {
        public IObjectToPool SetActive(bool isActive);
        public void SetPosition(Vector3 position);

        public event Action<IObjectToPool> ObjectShouldBeReturned;
    }
}