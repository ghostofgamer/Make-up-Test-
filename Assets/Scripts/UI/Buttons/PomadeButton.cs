using MakeupContent.PomadeContent;
using Tools;
using UnityEngine;

namespace UI.Buttons
{
    public class PomadeButton : AbstractButton
    {
        [SerializeField] private PomadeHandler _pomadeHandler;
        [SerializeField] private PomadeTool _pomadeTool;

        private int _index;

        public void SetIndex(int index)
        {
            _index = index;
        }

        protected override void OnClick()
        {
            _pomadeHandler.OnColorSelected(_index, transform.position, transform, _pomadeTool);
        }
    }
}