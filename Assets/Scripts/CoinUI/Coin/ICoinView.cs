using Base;
using UnityEngine;

namespace CoinUI.Coin
{
    public interface ICoinView : IBaseView
    {
        public RectTransform RectTransform { get; }

        public void DestroyCoin();
    }
}