using Cysharp.Threading.Tasks;
using Tools;
using UnityEngine;

namespace MakeupContent.PomadeContent
{
    public class PomadeHandler : MakeupHandler
    {
        [SerializeField] private Transform _facePomadePosition;

        private Transform _defaultPomadePosition;
        private PomadeTool _currentPomadeTool;
        private int _currentIndex;
        
        public void OnColorSelected(int index, Transform pomadeDefaultPosition,
            PomadeTool pomadeTool)
        {
            if (!TryStartAction())
                return;

            _currentPomadeTool = pomadeTool;
            _currentIndex = index;
            _defaultPomadePosition = pomadeDefaultPosition;
            
            UseToolAsync(_defaultPomadePosition, _currentPomadeTool.transform, _currentPomadeTool).Forget();
        }

        public async UniTask ApplyPomade()
        {
            await ApplyToolEffect(_facePomadePosition, Targets[_currentIndex], _defaultPomadePosition);
        }
    }
}