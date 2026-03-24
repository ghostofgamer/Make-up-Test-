using Cysharp.Threading.Tasks;
using Tools;
using UnityEngine;

namespace MakeupContent.PomadeContent
{
    public class PomadeHandler : MakeupHandler
    {
        [SerializeField] private Transform _facePomadePosition;

        private Transform _defaultPomadePosition;
        private MakeUpTool _currentPomadeTool;
        private int _currentIndex;
        
        public void OnColorSelected(int index, Transform pomadeDefaultPosition,
            MakeUpTool makeUpTool)
        {
            if (!TryStartAction())
                return;
            
            _currentPomadeTool = makeUpTool;
            _currentIndex = index;
            _defaultPomadePosition = pomadeDefaultPosition;
            
            UseToolAsync(_defaultPomadePosition, _currentPomadeTool.transform, _currentPomadeTool).Forget();
        }

        public override  async UniTask ApplyMakeUpAsync()
        {
            await ApplyToolEffect(_facePomadePosition, Targets[_currentIndex], _defaultPomadePosition);
        }
    }
}