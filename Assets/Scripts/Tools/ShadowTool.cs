using UnityEngine;

namespace Tools
{
    public class ShadowTool : Tool
    {
        [SerializeField] private ShadowEyeHandler _shadowHandler;

        private int _currentIndex;
        
        public void SetIndex(int index)
        {
            _currentIndex = index;
        }
        
        public override void ApplyToFace()
        {
            _shadowHandler.ApplyShadow(_currentIndex);
        }
    }
}