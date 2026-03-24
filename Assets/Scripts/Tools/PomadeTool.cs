using Cysharp.Threading.Tasks;
using MakeupContent.PomadeContent;
using UnityEngine;

namespace Tools
{
    public class PomadeTool : Tool
    {
        [SerializeField] private PomadeHandler _pomadeHandler;
        
        public override void ApplyToFace()
        {
            _pomadeHandler.ApplyPomade().Forget();
        }
    }
}
