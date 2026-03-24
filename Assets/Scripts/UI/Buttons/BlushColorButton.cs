using MakeupContent.BlushContent;
using UnityEngine;

namespace UI.Buttons
{
    public class BlushColorButton : AbstractButton
    {
        [SerializeField] private Color _color;
        [SerializeField] private BlushHandler _blushHandler;
        
        private int _index;

        public void SetIndex(int index)
        {
            _index = index;
        }
        
        protected override void OnClick()
        {
            _blushHandler.OnColorSelected(_color, transform.position, _index);
        }
    }
}