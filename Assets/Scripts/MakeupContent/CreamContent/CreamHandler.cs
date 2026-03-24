using Cysharp.Threading.Tasks;
using Tools;
using UnityEngine;

namespace MakeupContent.CreamContent
{
    public class CreamHandler : MakeupHandler
    {
        [SerializeField] private Transform _defaultCreamPos;
        [SerializeField] private CreamTool _creamTool;
        [SerializeField] private Transform _defaultParent;
        [SerializeField] private Transform _faceAcnePosition;

        public void ChooseCream()
        {
            if (!TryStartAction())
                return;

            UseToolAsync(_defaultCreamPos, _creamTool.transform, _creamTool).Forget();
        }

        public async UniTask ApplyCream()
        {
            await ApplyToolEffect(_faceAcnePosition, Targets[0], _defaultCreamPos, false);
        }
    }
}