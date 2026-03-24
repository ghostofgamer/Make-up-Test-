using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Tools;
using UnityEngine;


public class HandController : MonoBehaviour
{
    [SerializeField] private Transform _itemPosition;
    [SerializeField] private Transform _handTransform;
    [SerializeField] private Transform _defaultPos;
    [SerializeField] private float _moveSpeed = 800f;
    
    [Header("Settings")]
    [SerializeField] private float _moveDuration = 1f;

    private Transform _currentHeldObject;

    public Tool CurrentTool { get; private set; }
    public bool IsWorking { get; private set; } = false;
    
    /*public IEnumerator MoveHandTo(Vector3 targetPos, Action onComplete = null )
    {
        IsWorking = true;

        Tween tween = _handTransform
            .DOMove(targetPos, _moveDuration)
            .SetEase(Ease.InOutSine);

        yield return tween.WaitForCompletion();
        onComplete?.Invoke();
    }*/
    
    public async UniTask MoveHandTo(Vector3 targetPos, Action onComplete = null)
    {
        IsWorking = true;

        Tween tween = _handTransform
            .DOMove(targetPos, _moveDuration)
            .SetEase(Ease.InOutSine);

        await tween.AsyncWaitForCompletion();

        onComplete?.Invoke();
    }
    
    public void PickUpObject(Transform obj, Tool tool, Vector3 offset)
    {
        CurrentTool = tool;
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

        // StartCoroutine(ReturnHand());
        _ = ReturnHand();
    }
    
    /*public IEnumerator ReturnHand(Action onComplete = null)
    {
        yield return MoveHandTo(_defaultPos.position);

        onComplete?.Invoke();
        IsWorking = false;
    }*/
    
    public async UniTask ReturnHand(Action onComplete = null)
    {
        await MoveHandTo(_defaultPos.position);

        onComplete?.Invoke();
        IsWorking = false;
    }

    public void Apply()
    {
        if (CurrentTool != null)
            CurrentTool.ApplyToFace();
    }
    
    /*public void PlayApplyAnimation(Vector3 targetPos, Action onComplete = null)
    {
        float offset = 30f;
        float duration = 0.2f;

        Vector3 startPos = _handTransform.localPosition;

        Sequence seq = DOTween.Sequence();

        // 1. Подводим руку
        seq.Append(_handTransform.DOMove(targetPos, 0.4f).SetEase(Ease.OutSine));

        // 2. Плавные мазки
        for (int i = 0; i < 3; i++)
        {
            seq.Append(_handTransform.DOLocalMoveX(startPos.x + offset, duration).SetEase(Ease.InOutSine));
            seq.Append(_handTransform.DOLocalMoveX(startPos.x - offset, duration).SetEase(Ease.InOutSine));
        }

        // 3. Возврат в центр (опционально)
        seq.Append(_handTransform.DOLocalMoveX(startPos.x, duration).SetEase(Ease.InOutSine));

        seq.OnComplete(() =>
        {
            onComplete?.Invoke();
        });
    }*/
    
    public async UniTask PlayApplyAnimation(Vector3 targetPos)
    {
        float offset = 30f;
        float duration = 0.2f;

        Vector3 startPos = _handTransform.localPosition;

        Sequence seq = DOTween.Sequence();

        seq.Append(_handTransform.DOMove(targetPos, 0.4f).SetEase(Ease.OutSine));

        for (int i = 0; i < 3; i++)
        {
            seq.Append(_handTransform.DOLocalMoveX(startPos.x + offset, duration).SetEase(Ease.InOutSine));
            seq.Append(_handTransform.DOLocalMoveX(startPos.x - offset, duration).SetEase(Ease.InOutSine));
        }

        seq.Append(_handTransform.DOLocalMoveX(startPos.x, duration).SetEase(Ease.InOutSine));

        await seq.AsyncWaitForCompletion();
    }
    
    /*public async UniTask PlayApplyAnimation(Vector3 targetPos, Action onComplete = null)
    {
        float offset = 30f;
        float duration = 0.2f;

        Vector3 startPos = _handTransform.localPosition;

        Sequence seq = DOTween.Sequence();

        seq.Append(_handTransform.DOMove(targetPos, 0.4f).SetEase(Ease.OutSine));

        for (int i = 0; i < 3; i++)
        {
            seq.Append(_handTransform.DOLocalMoveX(startPos.x + offset, duration).SetEase(Ease.InOutSine));
            seq.Append(_handTransform.DOLocalMoveX(startPos.x - offset, duration).SetEase(Ease.InOutSine));
        }

        seq.Append(_handTransform.DOLocalMoveX(startPos.x, duration).SetEase(Ease.InOutSine));

        await seq.AsyncWaitForCompletion();

        onComplete?.Invoke();
    }*/
}