using Cysharp.Threading.Tasks;
using MakeupContent;
using UnityEngine;

namespace Tools
{
    public class MakeUpTool : MonoBehaviour, IApplyTool
    {
        [SerializeField] private MakeupHandler _makeupHandler;
        
        public void Apply()
        {
            _makeupHandler.ApplyMakeUpAsync().Forget();
        }
    }
}