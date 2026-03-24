using Cysharp.Threading.Tasks;
using HandContent;
using Tools;
using UI.Buttons;
using UnityEngine;
using UnityEngine.UI;

namespace MakeupContent.BlushContent
{
    public class BlushHandler : MakeupHandler
    {
        [SerializeField] private GameObject[] _blushes;
        [SerializeField] private BlushColorButton[] _blushButtons;
        [SerializeField] private Transform _brushTransform;
        [SerializeField] private Image _brushTipRenderer;
        [SerializeField] private Transform brushDefaultPosition;
        [SerializeField] private BlushTool _tool;
        [SerializeField] private Vector3 _offset;
        [SerializeField] private Transform _faceBushPos;
        [SerializeField] private Transform _posWiting;
        [SerializeField] private DraggableHandler _draggableHandler;

        private Color _currentColor;
        private Color _defaultColor = Color.white;
        private bool _isWorking = false;

        private void Awake()
        {
            Init();
        }

        public void OnColorSelected(Color color, Vector3 colorButtonPosition, int index)
        {
            if (!TryStartAction())
                return;

            _isWorking = true;
            _currentColor = color;
            _tool.SetIndex(index);
            HandSequence(colorButtonPosition).Forget();
        }

        public async UniTask ApplyBlush(int index)
        {
            _draggableHandler.SetValue(false);
            Debug.Log("ApplyBlush" + index);

            await HandController.PlayApplyAnimation(_faceBushPos.position - new Vector3(0, 100f, 0));

            Cleaning();
            _blushes[index].SetActive(true);

            await ReturnDefault();
        }

        protected override void Cleaning()
        {
            foreach (var blush in _blushes)
                blush.SetActive(false);
        }

        private void Init()
        {
            for (int i = 0; i < _blushButtons.Length; i++)
                _blushButtons[i].SetIndex(i);
        }

        private async UniTask HandSequence(Vector3 colorButtonPosition)
        {
            await MoveHandPickApply(
                brushDefaultPosition, // позиция инструмента
                _brushTransform, // трансформ кисти
                _tool, // инструмент
                _offset,
                _posWiting, // позиция ожидания
                _draggableHandler,
                colorButtonPosition, // позиция применения
                _brushTipRenderer,
                _currentColor
            );
        }

        private async UniTask ReturnDefault()
        {
            await ReturnTool(
                brushDefaultPosition, // позиция по умолчанию
                brushDefaultPosition,
                (() => _brushTipRenderer.color = _defaultColor) // родитель для возврата инструмента
            );
        }
    }
}