using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CoinUI
{
    public class CoinUIView : MonoBehaviour, ICoinUIView
    {
        [SerializeField] private Image _coinsUIImage;
        [SerializeField] private Image _coinUIPanel;
        [SerializeField] private TMP_Text _coinsAmountText;
        [SerializeField] private Canvas _canvas;

        private Camera _uiCamera;
        
        public RectTransform CoinsUIPosition => _coinsUIImage.rectTransform;
        public RectTransform CoinUIPanel => _coinUIPanel.rectTransform;
        public Transform Transform => transform;
        public Canvas Canvas => _canvas;
        public Camera Camera => _uiCamera;

        public event Action ObjectDestroyed;

        public CoinUIView Init(Camera uiCamera)
        {
            _uiCamera = uiCamera;
            _canvas.worldCamera = _uiCamera;
            return this;
        }
        
        public void UpdateCoinsAmount(int coinsAmount)
        {
            _coinsAmountText.text = coinsAmount.ToString();
        }
        
        private void OnDestroy()
        {
            ObjectDestroyed?.Invoke();
        }
    }
}