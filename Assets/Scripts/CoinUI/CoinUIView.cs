using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CoinUI
{
    public class CoinUIView : MonoBehaviour, ICoinUIView
    {
        [SerializeField] private Transform _coinsUIPosition;
        [SerializeField] private TMP_Text _coinsAmountText;
        [SerializeField] private Canvas _canvas;

        private Image _coinsUIImage; 
        private Camera _camera;
        
        public RectTransform CoinsUIPosition => _coinsUIImage.rectTransform;
        public Transform Transform => transform;
        public Canvas Canvas => _canvas;

        private void Awake()
        {
            _coinsUIImage = _coinsUIPosition.GetComponent<Image>();
            _camera = Camera.main.transform.GetChild(0).GetComponent<Camera>();
            GetComponent<Canvas>().worldCamera = _camera;
        }

        public void UpdateCoinsAmount(int coinsAmount)
        {
            _coinsAmountText.text = coinsAmount.ToString();
        }
    }
}