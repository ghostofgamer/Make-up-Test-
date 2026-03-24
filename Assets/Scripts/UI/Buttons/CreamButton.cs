using MakeupContent.CreamContent;
using UnityEngine;

namespace UI.Buttons
{
    public class CreamButton : AbstractButton
    {
        [SerializeField] private CreamHandler _creamHandler;
    
        protected override void OnClick()
        {
            _creamHandler.ChooseCream();
        }
    }
}