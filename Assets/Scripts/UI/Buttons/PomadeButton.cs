using MakeupContent.PomadeContent;
using Tools;
using UnityEngine;

namespace UI.Buttons
{
    public class PomadeButton : MakeUpButton
    {
        [SerializeField] private PomadeHandler _pomadeHandler;
        [SerializeField] private PomadeTool _pomadeTool;
        
        protected override void OnClick()
        {
            _pomadeHandler.OnColorSelected(Index, transform, _pomadeTool);
        }
    }
}