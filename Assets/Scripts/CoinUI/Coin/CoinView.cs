using System;
using UnityEngine;
using UnityEngine.UI;

namespace CoinUI.Coin
{
    public class CoinView : MonoBehaviour, ICoinView
    {
        [SerializeField] private Image _coinImage;
        public RectTransform RectTransform => _coinImage.rectTransform;

        public event Action ObjectDestroyed;
        
        public void SetCoinActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
        
        private void OnDestroy()
        {
            ObjectDestroyed?.Invoke();
        }
    }
}