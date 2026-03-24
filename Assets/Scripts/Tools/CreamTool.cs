using Cysharp.Threading.Tasks;
using MakeupContent;
using MakeupContent.CreamContent;
using UnityEngine;

namespace Tools
{
    public class CreamTool : Tool
    {
        [SerializeField] private CreamHandler _creamHandler;

        public override void ApplyToFace()
        {
            _creamHandler.ApplyCream().Forget();
        }
    }
}