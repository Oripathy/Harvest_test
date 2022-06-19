using Base;
using UnityEngine;

namespace CoinUI
{
    public interface ICoinUIView : IBaseView
    {
        public RectTransform CoinsUIPosition { get; }
        public Transform Transform { get; }
        public Canvas Canvas { get; }

        public void UpdateCoinsAmount(int coinsAmount);
    }
}