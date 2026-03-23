using UnityEngine;

namespace UI.Buttons
{
    public class ShadowEyeButton : AbstractButton
    {
        [SerializeField] private Color _color;
        [SerializeField] private  ShadowEyeHandler _shadowEyeHandler;
        
        private int _index;

        public void SetIndex(int index)
        {
            _index = index;
        }
        
        protected override void OnClick()
        {
            _shadowEyeHandler.OnColorSelected(_color, transform.position, _index);
        }
    }
}
