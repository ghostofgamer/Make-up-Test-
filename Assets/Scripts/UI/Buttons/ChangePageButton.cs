using BookContent;
using Enums;
using UnityEngine;

namespace UI.Buttons
{
    public class ChangePageButton : AbstractButton
    {
        private BookPresenter _bookPresenter;
        
        [field: SerializeField] public ButtonType ButtonType { get; private set; }

        public void Init(BookPresenter bookPresenter)
        {
            _bookPresenter = bookPresenter;
        }

        public void SetValue(bool value)
        {
            Button.interactable = value;
        }

        protected override void OnClick()
        {
            if (_bookPresenter != null)
                _bookPresenter.ChangePage(this);
            else
                Debug.LogWarning($"{name}: BookPresenter не назначен!");
        }
    }
}