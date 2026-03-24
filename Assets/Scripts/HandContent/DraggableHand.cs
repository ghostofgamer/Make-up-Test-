using UnityEngine;
using UnityEngine.EventSystems;

namespace HandContent
{
    public class DraggableHand : MonoBehaviour, IDragHandler, IEndDragHandler
    {
        [SerializeField] private RectTransform handTip;
        [SerializeField] private RectTransform faceZone;
        [SerializeField] private HandController _handController;
        
        private Canvas _canvas;
        private RectTransform _rectTransform;
        private Vector2 _handTipScreenPos;
        
        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _canvas = GetComponentInParent<Canvas>();
        }

        public void OnDrag(PointerEventData eventData)
        {
            _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _handTipScreenPos = RectTransformUtility.WorldToScreenPoint(_canvas.worldCamera, handTip.position);

            if (RectTransformUtility.RectangleContainsScreenPoint(faceZone, _handTipScreenPos, _canvas.worldCamera))
                _handController.Apply();
            else
                Debug.Log("Кисть руки не в зоне лица");
        }
    }
}