using System;
using TMPro;
using UnityEngine;

namespace Player.PlayerUI
{
    public class PlayerUIView : MonoBehaviour, IPlayerUIView
    { 
        [SerializeField] private TMP_Text _collectablesAmountText;
        [SerializeField] private Canvas _canvas;

        private Camera _uiCamera;

        public event Action ObjectDestroyed;

        public PlayerUIView Init(Camera uiCamera)
        {
            _uiCamera = uiCamera;
            _canvas.worldCamera = _uiCamera;
            return this;
        }

        public void UpdateCollectableAmount(int collectablesAmount, int maxCollectablesAmount)
        {
            _collectablesAmountText.text = collectablesAmount + " / " + maxCollectablesAmount;
        }
        
        private void OnDestroy()
        {
            ObjectDestroyed?.Invoke();
        }
    }
}