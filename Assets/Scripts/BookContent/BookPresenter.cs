using Enums;
using UI.Buttons;

namespace BookContent
{
    public class BookPresenter
    {
        private BookModel _bookModel;
        private BookViewer _bookViewer;

        public BookPresenter(BookModel bookModel, BookViewer bookViewer)
        {
            _bookModel = bookModel;
            _bookViewer = bookViewer;
            UpdateView();
        }

        public void ChangePage(ChangePageButton changePageButton)
        {
            switch (changePageButton.ButtonType)
            {
                case ButtonType.NextPageMake:
                    _bookModel.NextPage();
                    break;
                case ButtonType.PrevPageMake:
                    _bookModel.PrevPage();
                    break;
            }

            UpdateView();
        }
        
        private void UpdateView()
        {
            _bookViewer.ShowPage(_bookModel.CurrentPage);
            _bookViewer.SetButtons(_bookModel.CanGoPrev(), _bookModel.CanGoNext());
        }
    }
}