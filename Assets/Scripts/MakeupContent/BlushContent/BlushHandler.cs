using Cysharp.Threading.Tasks;
using Tools;
using UnityEngine;
using UnityEngine.UI;

namespace MakeupContent.BlushContent
{
    public class BlushHandler : MakeupHandler
    {
        [SerializeField] private Transform _brushTransform;
        [SerializeField] private Image _brushTipRenderer;
        [SerializeField] private Transform brushDefaultPosition;
        [SerializeField] private BlushTool _tool;
        [SerializeField] private Transform _faceBushPos;

        private Color _currentColor;
        private Color _defaultColor = Color.white;
        private int _currentIndex;
        
        public void OnColorSelected(Color color, Vector3 colorButtonPosition, int index)
        {
            if (!TryStartAction())
                return;

            _currentColor = color;
            _currentIndex = index;
            
            UseToolAsync(brushDefaultPosition, _brushTransform, _tool, colorButtonPosition,
                _brushTipRenderer, _currentColor).Forget();
        }

        public async UniTask ApplyBlush()
        {
            await ApplyToolEffect(_faceBushPos, Targets[_currentIndex], brushDefaultPosition, true,
                () => _brushTipRenderer.color = _defaultColor);
        }
    }
}