using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Tools;
using UnityEngine;

namespace HandContent
{
    public class HandController : MonoBehaviour
    {
        [SerializeField] private Transform _itemPosition;
        [SerializeField] private Transform _handTransform;
        [SerializeField] private Transform _defaultPos;

        [Header("Settings")] [SerializeField] private float _moveDuration = 1f;
        [SerializeField] private float _moveSpeed = 800f;
        [SerializeField] private float _offset = 30f;
        [SerializeField] private float _smearDuration = 0.21f;
        [SerializeField] private float _moveTargetDuration = 0.4f;

        private Transform _currentHeldObject;
        private Vector3 _startPos;
        private Sequence _seq;

        public MakeUpTool CurrentMakeupTool { get; private set; }
        public bool IsWorking { get; private set; } = false;

        public async UniTask MoveHandTo(Vector3 targetPos, Action onComplete = null)
        {
            IsWorking = true;

            Tween tween = _handTransform
                .DOMove(targetPos, _moveDuration)
                .SetEase(Ease.InOutSine);

            await tween.AsyncWaitForCompletion();

            onComplete?.Invoke();
        }

        public void PickUpObject(Transform obj, MakeUpTool applyTool, Vector3 offset)
        {
            CurrentMakeupTool = applyTool;
            _currentHeldObject = obj;
            _currentHeldObject.SetParent(_itemPosition);
            _currentHeldObject.localPosition = offset;
        }

        public void DropItem(Transform parent)
        {
            if (_currentHeldObject == null)
                return;

            _currentHeldObject.SetParent(parent);
            _currentHeldObject.localPosition = Vector3.zero;
            _currentHeldObject = null;

            _ = ReturnHand();
        }

        public async UniTask ReturnHand(Action onComplete = null)
        {
            await MoveHandTo(_defaultPos.position);

            onComplete?.Invoke();
            IsWorking = false;
        }

        public void Apply()
        {
            if (CurrentMakeupTool != null)
                CurrentMakeupTool.Apply();
        }

        public async UniTask PlayApplyAnimation(Vector3 targetPos)
        {
            _startPos = _handTransform.localPosition;
            _seq = DOTween.Sequence();

            _seq.Append(_handTransform.DOMove(targetPos, _moveTargetDuration).SetEase(Ease.OutSine));

            for (int i = 0; i < 3; i++)
            {
                _seq.Append(_handTransform.DOLocalMoveX(_startPos.x + _offset, _smearDuration).SetEase(Ease.InOutSine));
                _seq.Append(_handTransform.DOLocalMoveX(_startPos.x - _offset, _smearDuration).SetEase(Ease.InOutSine));
            }

            _seq.Append(_handTransform.DOLocalMoveX(_startPos.x, _smearDuration).SetEase(Ease.InOutSine));
            _seq.AppendCallback(() => _handTransform.localPosition = _startPos);
                
            await _seq.AsyncWaitForCompletion();
        }
    }
}