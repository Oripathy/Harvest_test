using UnityEngine;
using UnityEngine.UI;

namespace CoinUI.Coin
{
    public class CoinView : MonoBehaviour, ICoinView
    {
        [SerializeField] private Image _coinImage;
        public RectTransform RectTransform => _coinImage.rectTransform;
        public void DestroyCoin()
        {
            Destroy(gameObject);
        }
    }
}