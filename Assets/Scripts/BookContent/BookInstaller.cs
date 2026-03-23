using UnityEngine;

namespace BookContent
{
    public class BookInstaller : MonoBehaviour
    {
        [SerializeField] private BookViewer _bookViewer;

        private BookPresenter _bookPresenter;
        private BookModel _bookModel;

        public void Start()
        {
            _bookModel = new BookModel(_bookViewer.PageCount);
            _bookPresenter = new BookPresenter(_bookModel, _bookViewer);

            foreach (var changePageButton in _bookViewer.ChangePageButtons)
                changePageButton.Init(_bookPresenter);
        }
    }
}