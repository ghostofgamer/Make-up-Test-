using Cysharp.Threading.Tasks;
using MakeupContent.BlushContent;
using UnityEngine;

namespace Tools
{
    public class BlushTool : Tool
    {
        [SerializeField]private BlushHandler _handler;

        private int _currentIndex;
        
        public void SetIndex(int index)
        {
            _currentIndex = index;
        }
        
        public override void ApplyToFace()
        {
            _handler.ApplyBlush(_currentIndex).Forget();
        }
    }
}