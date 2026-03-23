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
        
        protected override void Click()
        {
            if (_bookPresenter != null)
                _bookPresenter.ChangePage(this);
            else
                Debug.LogWarning($"{name}: BookPresenter не назначен!");
        }
    }
}