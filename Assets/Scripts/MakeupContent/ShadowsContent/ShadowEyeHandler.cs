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
        [SerializeField] private ShadowTool _shadowTool;
        [SerializeField] private Transform _brushDefaultPosition;
        [SerializeField] private Image _brushTipRenderer;
        [SerializeField] private Transform _brushTransform;
        [SerializeField] private Transform _shadowFacePos;

        private Color _currentColor;
        private Color _defaultColor = Color.white;
        private int _currentIndex;
        
        public void OnColorSelected(Color color, Vector3 colorButtonPosition, int index)
        {
            if (!TryStartAction())
                return;

            _currentColor = color;
            _currentIndex = index;

            UseToolAsync(_brushDefaultPosition, _brushTransform, _shadowTool, colorButtonPosition,
                _brushTipRenderer, _currentColor).Forget();
        }

        public async UniTask ApplyShadow()
        {
            await ApplyToolEffect(_shadowFacePos, Targets[_currentIndex], _brushDefaultPosition, true,
                () => _brushTipRenderer.color = _defaultColor);
        }
    }
}