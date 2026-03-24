using Cysharp.Threading.Tasks;
using MakeupContent.BlushContent;
using UnityEngine;

namespace Tools
{
    public class BlushTool : Tool
    {
        [SerializeField]private BlushHandler _handler;
        
        public override void ApplyToFace()
        {
            _handler.ApplyBlush().Forget();
        }
    }
}