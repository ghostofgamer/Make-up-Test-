using UnityEngine;

namespace BookContent
{
    public class BookModel
    {
        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        
        public BookModel(int totalPages, int startPage = 0)
        {
            TotalPages = totalPages;
            CurrentPage = Mathf.Clamp(startPage, 0, totalPages - 1);
        }

        public void NextPage()
        {
            if (CurrentPage < TotalPages - 1)
                CurrentPage++;
        }

        public void PrevPage()
        {
            if (CurrentPage > 0)
                CurrentPage--;
        }

        public bool CanGoNext() => CurrentPage < TotalPages - 1;
        public bool CanGoPrev() => CurrentPage > 0;
    }
}