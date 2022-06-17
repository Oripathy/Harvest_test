using System;
using UnityEngine;

namespace WheatField.Wheat
{
    public class WheatView : MonoBehaviour, IWheatView, IHarvestable
    {
        [SerializeField] private Collider _collider;
        [SerializeField] private GameObject _grownUpWheat;
        [SerializeField] private GameObject _wheatLowerPart;
        [SerializeField] private GameObject _wheatUpperPart;
        
        public Transform Transform => transform;
        public Collider Collider => _collider;
        public GameObject GrownUpWheat => _grownUpWheat;
        public GameObject WheatLowerPart => _wheatLowerPart;
        public GameObject WheatUpperPart => _wheatUpperPart;

        public event Action WheatHarvested;

        private void Start()
        {
            // _wheatLowerPart.SetActive(false);
            // _wheatUpperPart.SetActive(false);
            WheatHarvested?.Invoke();
        }

        public void Harvest()
        {
            WheatHarvested?.Invoke();
            Debug.Log("preparing");
        }
    }
}