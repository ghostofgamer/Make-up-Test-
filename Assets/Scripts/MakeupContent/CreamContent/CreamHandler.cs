using Cysharp.Threading.Tasks;
using HandContent;
using Tools;
using UnityEngine;

namespace MakeupContent.CreamContent
{
    public class CreamHandler : MakeupHandler
    {
        [SerializeField] private GameObject _acne;
        [SerializeField] private Transform _defaultCreamPos;
        [SerializeField] private CreamTool _creamTool;
        [SerializeField] private Transform _posWiting;
        [SerializeField] private Transform _defaultParent;
        [SerializeField] private Vector3 _offset;
        [SerializeField] private Transform _faceAcnePosition;
        [SerializeField] private DraggableHandler _draggableHandler;

        private bool _isWorking = false;

        public void ChooseCream()
        {
            if (!TryStartAction())
                return;

            _isWorking = true;
            HandSequence(_defaultCreamPos.position).Forget();
        }


        public async UniTask ApplyCream()
        {
            _draggableHandler.SetValue(false);

            await HandController.PlayApplyAnimation(_faceAcnePosition.position - new Vector3(0, 100f, 0));
            
            _acne.SetActive(false);
            
             await ReturnDefault();
        }

        protected override void Cleaning()
        {
            _acne.SetActive(true);
        }

        private async UniTask HandSequence(Vector3 creamPos)
        {
            await MoveHandPickApply(
                _defaultCreamPos,      // позиция инструмента
                _creamTool.transform,  // инструмент
                _creamTool,
                Vector3.zero,          // offset
                _posWiting,            // позиция ожидания
                _draggableHandler
                // applyTargetPos = null → просто берём и идём
            );
        }

        private async UniTask ReturnDefault()
        {
            await ReturnTool(
                _defaultCreamPos,
                _defaultCreamPos
            );
        }
    }
}