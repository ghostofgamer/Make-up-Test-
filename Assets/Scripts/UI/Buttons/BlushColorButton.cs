using BlushContent;
using SOContent;
using UnityEngine;

namespace UI.Buttons
{
    public class BlushColorButton : AbstractButton
    {
        [SerializeField] private BlushConfig _config;
        [SerializeField] private BlushHandler _blushHandler;


        protected override void OnClick()
        {
            throw new System.NotImplementedException();
        }
    }
}