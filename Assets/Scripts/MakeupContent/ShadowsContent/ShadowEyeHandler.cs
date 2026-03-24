using Cysharp.Threading.Tasks;
using HandContent;
using Tools;
using UI.Buttons;
using UnityEngine;
using UnityEngine.UI;

namespace MakeupContent.ShadowsContent
{
    public class ShadowEyeHandler : MakeupHandler
    {
        [SerializeField] private GameObject[] _shadows;
        [SerializeField] private ShadowEyeButton[] _shadowButtons;
        [SerializeField] private ShadowTool _shadowTool;
        [SerializeField] private Transform _brushDefaultPosition;
        [SerializeField] private Image _brushTipRenderer;
        [SerializeField] private Transform _brushTransform;
        [SerializeField] private Vector3 _offset;
        [SerializeField] private Transform _shadowFacePos;
        [SerializeField] private Transform _posWiting;
        [SerializeField] private DraggableHandler _draggableHandler;

        private bool _isWorking = false;
        private Color _currentColor;
        private Color _defaultColor = Color.white;

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
            _shadowTool.SetIndex(index);
            HandSequence(colorButtonPosition).Forget();
        }

        public async UniTask ApplyShadow(int index)
        {
            _draggableHandler.SetValue(false);
            Debug.Log("ApplyShadow" + index);

            await HandController.PlayApplyAnimation(_shadowFacePos.position - new Vector3(0, 100f, 0));
            
            Cleaning();
            _shadows[index].SetActive(true);
            
             await ReturnDefault();
        }

        private void Init()
        {
            for (int i = 0; i < _shadowButtons.Length; i++)
                _shadowButtons[i].SetIndex(i);
        }

        private async UniTask HandSequence(Vector3 colorButtonPosition)
        {
            await MoveHandPickApply(
                _brushDefaultPosition,
                _brushTransform,
                _shadowTool,
                _offset,
                _posWiting,
                _draggableHandler,
                colorButtonPosition,
                _brushTipRenderer,
                _currentColor
            );
        }

        private async UniTask ReturnDefault()
        {
            await ReturnTool(
                _brushDefaultPosition,
                _brushDefaultPosition
                ,()=> _brushTipRenderer.color = _defaultColor
            );
        }

        protected override void Cleaning()
        {
            foreach (var shadow in _shadows)
                shadow.SetActive(false);
        }
    }
}