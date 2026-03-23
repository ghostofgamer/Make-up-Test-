using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Buttons
{
    public abstract class AbstractButton : MonoBehaviour
    {
        private Button _button;
        public event Action Clicked;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            Click();
            Clicked?.Invoke();
        }

        protected virtual void Click()
        {
        }
    }
}