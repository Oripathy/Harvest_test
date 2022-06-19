using Base;
using UnityEngine;

namespace Coins
{
    public interface ICoinView : IBaseView
    {
        public RectTransform RectTransform { get; }

        public void DestroyCoin();
    }
}