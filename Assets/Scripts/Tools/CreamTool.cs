using MakeupContent;
using UnityEngine;

namespace Tools
{
    public class CreamTool : Tool
    {
        [SerializeField] private CreamHandler _creamHandler;

        public override void ApplyToFace()
        {
            _creamHandler.ApplyCream();
        }
    }
}