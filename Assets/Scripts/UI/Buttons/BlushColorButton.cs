using MakeupContent.BlushContent;
using UnityEngine;

namespace UI.Buttons
{
    public class BlushColorButton : MakeUpButton
    {
        [SerializeField] private Color _color;
        [SerializeField] private BlushHandler _blushHandler;
        
        
        protected override void OnClick()
        {
            _blushHandler.OnColorSelected(_color, transform.position, Index);
        }
    }
}