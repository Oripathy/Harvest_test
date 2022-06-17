using System;
using UnityEngine;
using Random = UnityEngine.Random;

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
            transform.localScale = new Vector3(1f, 1f, 1f);
            Quaternion randomRotation;
            var randomAngle = Random.Range(0f, 90f);
            randomRotation =
                new Quaternion(0f, (float) Math.Sin(randomAngle / 2), 0f, (float) Math.Cos(randomAngle / 2));
            transform.rotation *= randomRotation;
        }

        public void Harvest()
        {
            WheatHarvested?.Invoke();
        }
    }
}