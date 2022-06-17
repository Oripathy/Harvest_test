using System;
using Base;
using WheatField.Wheat;

namespace Player.Scythe
{
    public interface IScytheView : IBaseView
    {
        public event Action<IHarvestable> WheatHarvested;
        public void SetScytheActive(bool isActive);
    }
}