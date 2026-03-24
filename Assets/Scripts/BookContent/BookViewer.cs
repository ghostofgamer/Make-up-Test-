using Enums;
using UI.Buttons;
using UnityEngine;

namespace BookContent
{
    public class BookViewer : MonoBehaviour
    {
        [SerializeField] private GameObject[] _pages;

        [field: SerializeField] public ChangePageButton[] ChangePageButtons { get; private set; }
        public int PageCount => _pages.Length;

        public void SetButtons(bool canGoPrev, bool canGoNext)
        {
            foreach (var btn in ChangePageButtons)
            {
                if (btn.ButtonType == ButtonType.PrevPageMake)
                    btn.SetValue(canGoPrev);

                if (btn.ButtonType == ButtonType.NextPageMake)
                    btn.SetValue(canGoNext);
            }
        }

        public void ShowPage(int index)
        {
            for (int i = 0; i < _pages.Length; i++)
                _pages[i].SetActive(i == index);
        }
    }
}