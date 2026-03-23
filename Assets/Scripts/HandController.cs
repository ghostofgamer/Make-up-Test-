using System;
using System.Collections;
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
    
    public IEnumerator MoveHandTo(Vector3 targetPos, Action onComplete = null)
    {
        while (Vector3.Distance(_handTransform.position, targetPos) > 0.01f)
        {
            _handTransform.position = Vector3.MoveTowards(_handTransform.position, targetPos, _moveSpeed * Time.deltaTime);
            yield return null;
        }
        
        onComplete?.Invoke();
    }
    
    public void PickUpObject(Transform obj,Tool tool)
    {
        CurrentTool = tool;
        _currentHeldObject = obj;
        _currentHeldObject.SetParent(_itemPosition);
        _currentHeldObject.localPosition = Vector3.zero;
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
        });
    }

    public void Apply()
    {
        if(CurrentTool != null)
            CurrentTool.ApplyToFace();
    }
}
