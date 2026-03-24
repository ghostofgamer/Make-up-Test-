using Cysharp.Threading.Tasks;
using UnityEngine;

namespace MakeupContent.CreamContent
{
    public class CreamHandler : MakeupHandler
    {
        [SerializeField] private Transform _defaultCreamPos;
        [SerializeField] private Transform _defaultParent;
        [SerializeField] private Transform _faceAcnePosition;

        public void ChooseCream()
        {
            if (!TryStartAction())
                return;

            UseToolAsync(_defaultCreamPos, ApplyTool.transform, ApplyTool).Forget();
        }

        public override async UniTask ApplyMakeUpAsync()
        {
            await ApplyToolEffect(_faceAcnePosition, Targets[0], _defaultCreamPos, false);
        }
    }
}