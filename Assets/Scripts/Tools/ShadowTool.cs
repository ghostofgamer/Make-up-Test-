using Cysharp.Threading.Tasks;
using MakeupContent.ShadowsContent;
using UnityEngine;

namespace Tools
{
    public class ShadowTool : Tool
    {
        [SerializeField] private ShadowEyeHandler _shadowHandler;
        
        public override void ApplyToFace()
        {
            _shadowHandler.ApplyShadow().Forget();
        }
    }
}