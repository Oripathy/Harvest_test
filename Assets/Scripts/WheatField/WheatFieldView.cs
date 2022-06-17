using UnityEngine;

namespace WheatField
{
    public class WheatFieldView : MonoBehaviour, IWheatFieldView
    {
        public Transform Transform => transform;
    }
}