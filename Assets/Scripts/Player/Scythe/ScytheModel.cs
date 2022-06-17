using System;
using Base;

namespace Player.Scythe
{
    public class ScytheModel : BaseModel
    {
        private bool _isActive;
        
        public bool IsActive
        {
            get => _isActive;
            set
            {
                _isActive = value;
                IsActiveChanged?.Invoke(_isActive);
            }
        }

        public event Action<bool> IsActiveChanged;
    }
}