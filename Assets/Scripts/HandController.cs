using System;
using System.Collections;
using DG.Tweening;
using Tools;
using UnityEngine;

public class HandController : MonoBehaviour
{
    [SerializeField] private Transform _itemPosition;
    [SerializeField] private Transform _handTransform;
    [SerializeField] private Transform _defaultPos;
    [SerializeField] private float _moveSpeed = 800f;

    private Transform _currentHeldObject;

    public Tool CurrentTool { get; private set; }
    public bool IsWorking { get; private set; } = false;

    public IEnumerator MoveHandTo(Vector3 targetPos, Action onComplete = null)
    {
        IsWorking = true;
        
        while (Vector3.Distance(_handTransform.position, targetPos) > 0.01f)
        {
            _handTransform.position =
                Vector3.MoveTowards(_handTransform.position, targetPos, _moveSpeed * Time.deltaTime);
            yield return null;
        }

        onComplete?.Invoke();
    }

    public void PickUpObject(Transform obj, Tool tool ,Vector3 offset)
    {
        CurrentTool = tool;
        _currentHeldObject = obj;
        _currentHeldObject.SetParent(_itemPosition);
        _currentHeldObject.localPosition = offset;
    }

    public void DropItem(Transform parent)
    {
        _currentHeldObject.SetParent(parent);
        _currentHeldObject.localPosition = Vector3.zero;

        StartCoroutine(ReturnHand());
    }

    public IEnumerator ReturnHand(Action onComplete = null)
    {
        if (_currentHeldObject != null)
        {
            _currentHeldObject = null;
        }

        yield return MoveHandTo(_defaultPos.position, () =>
        {
            onComplete?.Invoke();
            IsWorking = false;
        });
    }

    public void Apply()
    {
        if (CurrentTool != null)
            CurrentTool.ApplyToFace();
    }
    
    public void PlayApplyAnimation(Vector3 targetPos, Action onComplete = null)
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
        
        
        /*_handTransform.DOMove(targetPos, 1f).OnComplete(() =>
        {
            /#1#/ 2. Делаем "мазки" влево-вправо
            _handTransform
                .DOPunchPosition(new Vector3(30f, 0f, 0f), 1.5f, 6, 0.5f)
                .OnComplete(() =>
                {
                    onComplete?.Invoke();
                });#1#
        });*/
    }
}