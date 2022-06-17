using System;
using Base;

namespace Player.WheatDetector
{
    public interface IWheatDetectorView : IBaseView
    {
        public event Action WheatDetected;
        public event Action WheatNotDetected;
    }
}