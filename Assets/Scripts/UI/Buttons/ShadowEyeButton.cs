using MakeupContent.ShadowsContent;
using UnityEngine;

namespace UI.Buttons
{
    public class ShadowEyeButton : MakeUpButton
    {
        [SerializeField] private Color _color;
        [SerializeField] private  ShadowEyeHandler _shadowEyeHandler;
        
        protected override void OnClick()
        {
            _shadowEyeHandler.OnColorSelected(_color, transform.position, Index);
        }
    }
}
