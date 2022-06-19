using TMPro;
using UnityEngine;

namespace Player.PlayerUI
{
    public class PlayerUIView : MonoBehaviour, IPlayerUIView
    { 
        [SerializeField] private TMP_Text _collectablesAmountText;

        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main.transform.GetChild(0).GetComponent<Camera>();
            GetComponent<Canvas>().worldCamera = _camera;
        }

        public void UpdateCollectableAmount(int collectablesAmount, int maxCollectablesAmount)
        {
            _collectablesAmountText.text = collectablesAmount + " / " + maxCollectablesAmount;
        }
    }
}