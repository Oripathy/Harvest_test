using UnityEngine;

namespace WheatField.WheatCube
{
    public interface ICollectable
    {
        public void Collect(Vector3 position, Transform bag);
        public void Sell(Vector3 position);
    }
}