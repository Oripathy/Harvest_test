using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace WheatField.Wheat
{
    public class WheatView : MonoBehaviour, IWheatView, IHarvestable
    {
        [SerializeField] private Collider _collider;
        [SerializeField] private MeshRenderer _meshRenderer;

        public Transform Transform => transform;
        public Collider Collider => _collider;
        public MeshRenderer MeshRenderer => _meshRenderer;

        public event Action WheatHarvested;
        public event Action ObjectDestroyed;

        private void Start()
        {
            _meshRenderer.material = new Material(_meshRenderer.material);
            transform.localScale = new Vector3(1f, 1f, 1f);
            var randomAngle = Random.Range(0f, 90f);
            var randomRotation = new Quaternion(0f, (float) Math.Sin(randomAngle / 2), 0f, (float) Math.Cos(randomAngle / 2));
            transform.rotation *= randomRotation;
        }

        public void Harvest()
        {
            WheatHarvested?.Invoke();
        }

        private void OnDestroy()
        {
            ObjectDestroyed?.Invoke();
        }
    }
}