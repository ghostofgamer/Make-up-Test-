using System.Collections;
using Tools;
using UnityEngine;

namespace MakeupContent
{
    public class CreamHandler : MakeupHandler
    {
        [SerializeField] private GameObject _acne;
        [SerializeField] private Transform _defaultCreamPos;
        [SerializeField] private CreamTool _creamTool;
        [SerializeField] private HandController _handController;
        [SerializeField] private Transform _posWiting;
        [SerializeField] private Transform _defaultParent;
        [SerializeField] private Vector3 _offset;
        
        private bool _isWorking = false;

        public void ChooseCream()
        {
            if (_isWorking || _handController.IsWorking)
                return;

            _isWorking = true;
            StartCoroutine(HandSequence(_defaultCreamPos.position));
        }

        public void ApplyCream()
        {
            _acne.SetActive(false);
            StartCoroutine(ReturnDefault());
        }

        protected override void Cleaning()
        {
            _acne.SetActive(true);
        }

        private IEnumerator HandSequence(Vector3 creamPos)
        {
            yield return _handController.MoveHandTo(creamPos,
                () => { _handController.PickUpObject(_creamTool.transform, _creamTool,_offset); });
            
            yield return _handController.MoveHandTo(_posWiting.position);
        }
        
        private IEnumerator ReturnDefault()
        {
            yield return _handController.MoveHandTo(_defaultCreamPos.position, () =>
            {
                _handController.DropItem(_defaultParent);
            });

            yield return _handController.ReturnHand(() => { _isWorking = false; });
        }
    }
}