using System;

namespace Base
{
    public interface IBaseView
    {
        public event Action ObjectDestroyed;
    }
}