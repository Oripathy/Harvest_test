using Base;
using UnityEngine;

namespace CoinUI
{
    public interface ICoinUIView : IBaseView
    {
        public RectTransform CoinUIPanel { get;}
        public Canvas Canvas { get; }
        public Camera Camera { get; }

        public void UpdateCoinsAmount(int coinsAmount);
    }
}