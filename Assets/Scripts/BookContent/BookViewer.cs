using System;
using Enums;
using UI.Buttons;
using UnityEngine;
using UnityEngine.UI;

namespace BookContent
{
    public class BookViewer : MonoBehaviour
    {
        [SerializeField] private GameObject[] _pages;

        public event Action OnNextClicked;
        public event Action OnPrevClicked;

        [field: SerializeField] public ChangePageButton[] ChangePageButtons { get; private set; }
        public int PageCount => _pages.Length;

        public void SetButtons(bool canGoPrev, bool canGoNext)
        {
            foreach (var btn in ChangePageButtons)
            {
                var unityButton = btn.GetComponent<Button>();

                if (btn.ButtonType == ButtonType.PrevPageMake)
                    unityButton.interactable = canGoPrev;

                if (btn.ButtonType == ButtonType.NextPageMake)
                    unityButton.interactable = canGoNext;
            }
        }

        public void ShowPage(int index)
        {
            for (int i = 0; i < _pages.Length; i++)
                _pages[i].SetActive(i == index);
        }
    }
}