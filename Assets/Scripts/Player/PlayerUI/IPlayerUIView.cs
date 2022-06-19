using Base;
using TMPro;
using UnityEngine;

namespace Player.PlayerUI
{
    public interface IPlayerUIView : IBaseView
    {
        public void UpdateCollectableAmount(int collectablesAmount, int maxCollectablesAmount);
    }
}