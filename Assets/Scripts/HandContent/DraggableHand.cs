using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;

namespace HandContent
{
    public class DraggableHand : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        [SerializeField] private RectTransform handTip;
        [SerializeField] private RectTransform faceZone;

        public Tool CurrentTool { get; private set; }
        private Canvas _canvas;
        private RectTransform _rectTransform;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _canvas = GetComponentInParent<Canvas>(); // Canvas, для корректного расчета drag
        }
        
        public void PickUpTool(Tool tool)
        {
            CurrentTool = tool;
            CurrentTool.OnPickUp();
        }

        public void OnDrag(PointerEventData eventData)
        {
            Debug.Log("OnDrag!");
            _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("OnBeginDrag!");
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log("OnEndDrags");

            Vector2 handTipScreenPos = RectTransformUtility.WorldToScreenPoint(_canvas.worldCamera, handTip.position);

            if (RectTransformUtility.RectangleContainsScreenPoint(faceZone, handTipScreenPos, _canvas.worldCamera))
            {
                Debug.Log("Кисть руки в зоне лица!");
                CurrentTool.ApplyToFace();
                // Вызвать событие попадания
            }
            else
            {
                Debug.Log("Кисть руки не в зоне лица");
            }
            
            CurrentTool.OnDrop();
            CurrentTool = null;
        }
    }
}