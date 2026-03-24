using MakeupContent.PomadeContent;
using Tools;
using UnityEngine;

namespace UI.Buttons
{
    public class PomadeButton : MakeUpButton
    {
        [SerializeField] private PomadeHandler _pomadeHandler;
        [SerializeField] private MakeUpTool _makeUpTool;
        
        protected override void OnClick()
        {
            _pomadeHandler.OnColorSelected(Index, transform, _makeUpTool);
        }
    }
}