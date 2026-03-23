using System.Collections;
using HandContent;
using Tools;
using UI.Buttons;
using UnityEngine;

namespace MakeupContent.PomadeContent
{
    public class PomadeHandler : MakeupHandler
    {
        [SerializeField] private GameObject[] _pomades;
        [SerializeField] private HandController _handController;
        [SerializeField] private PomadeButton[] _pomadeButtons;
        [SerializeField] private Transform _waitingPosition;
        [SerializeField] private Vector3 _offset;
        [SerializeField] private Transform _facePomadePosition;
        [SerializeField] private DraggableHandler _draggableHandler;
        
        private Transform _defaultPomadePosition;
        private bool _isWorking = false;
        private int _currentIndex;
        private PomadeTool _currentPomadeTool;

        private void Awake()
        {
            Init();
        }

        public void OnColorSelected(int index, Vector3 colorButtonPosition, Transform pomadeDefaultPosition,
            PomadeTool pomadeTool)
        {
            if (_isWorking || _handController.IsWorking)
                return;

            _currentPomadeTool = pomadeTool;
            _currentIndex = index;
            _defaultPomadePosition = pomadeDefaultPosition;
            _isWorking = true;
            StartCoroutine(HandSequence(colorButtonPosition));
        }

        public void ApplyPomade()
        {
            _draggableHandler.SetValue(false);
            Debug.Log("ApplyPomade");

            _handController.PlayApplyAnimation(_facePomadePosition.position - new Vector3(0, 100f, 0),
                () =>
                {
                    Cleaning();

                    _pomades[_currentIndex].SetActive(true);
                    StartCoroutine(ReturnDefault());
                });
        }

        private void Init()
        {
            for (int i = 0; i < _pomadeButtons.Length; i++)
                _pomadeButtons[i].SetIndex(i);
        }

        private IEnumerator HandSequence(Vector3 colorButtonPosition)
        {
            yield return _handController.MoveHandTo(_defaultPomadePosition.position,
                () => { _handController.PickUpObject(_currentPomadeTool.transform, _currentPomadeTool, _offset); });

            yield return _handController.MoveHandTo(_waitingPosition.position);
            _draggableHandler.SetValue(true);
        }

        private IEnumerator ReturnDefault()
        {
            yield return _handController.MoveHandTo(_defaultPomadePosition.position,
                () => { _handController.DropItem(_defaultPomadePosition); });

            yield return _handController.ReturnHand(() => { _isWorking = false; });
        }

        protected override void Cleaning()
        {
            foreach (var pomade in _pomades)
                pomade.SetActive(false);
        }
    }
}