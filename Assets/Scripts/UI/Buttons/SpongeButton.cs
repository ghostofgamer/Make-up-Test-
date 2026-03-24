using MakeupContent.SpongeContent;
using UnityEngine;

namespace UI.Buttons
{
    public class Sponge : AbstractButton
    {
        [SerializeField] private SpongeHandler _spongeHandler;

        protected override void OnClick() => _spongeHandler.CleanMakeUp();
    }
}