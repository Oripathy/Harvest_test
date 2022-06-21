using System;
using UnityEngine;
using WheatField.Wheat;

namespace Player.Scythe
{
    public class ScytheView : MonoBehaviour, IScytheView
    {
        public event Action<IHarvestable> WheatHarvested;
        public event Action ObjectDestroyed;
     
        public void SetScytheActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IHarvestable>(out var harvestable))
            {
                WheatHarvested?.Invoke(harvestable);
            }
        }
        
        private void OnDestroy()
        {
            ObjectDestroyed?.Invoke();
        }
    }
}