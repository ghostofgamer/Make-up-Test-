using Cysharp.Threading.Tasks;
using HandContent;
using Tools;
using UI.Buttons;
using UnityEngine;

namespace MakeupContent.PomadeContent
{
    public class PomadeHandler : MakeupHandler
    {
        [SerializeField] private GameObject[] _pomades;
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
            if (!TryStartAction())
                return;

            _currentPomadeTool = pomadeTool;
            _currentIndex = index;
            _defaultPomadePosition = pomadeDefaultPosition;
            _isWorking = true;
             HandSequence(colorButtonPosition).Forget();
        }

        public async UniTask ApplyPomade()
        {
            _draggableHandler.SetValue(false);
            
            await HandController.PlayApplyAnimation(_facePomadePosition.position - new Vector3(0, 100f, 0));
            
            Cleaning();
            _pomades[_currentIndex].SetActive(true);
            
            await ReturnDefault();
        }

        private void Init()
        {
            for (int i = 0; i < _pomadeButtons.Length; i++)
                _pomadeButtons[i].SetIndex(i);
        }

        private async UniTask HandSequence(Vector3 colorButtonPosition)
        {
            await MoveHandPickApply(
                _defaultPomadePosition,    // позиция инструмента
                _currentPomadeTool.transform,
                _currentPomadeTool,
                _offset,
                _waitingPosition,          // позиция ожидания
                _draggableHandler
            );
        }
        
        private async UniTask ReturnDefault()
        {
            await ReturnTool(
                _defaultPomadePosition, // позиция по умолчанию
                _defaultPomadePosition  // родитель для возврата инструмента
            );
        }

        protected override void Cleaning()
        {
            foreach (var pomade in _pomades)
                pomade.SetActive(false);
        }
    }
}