using UnityEngine;

namespace HandContent
{
    public class DraggableHandler : MonoBehaviour
    {
        [SerializeField] private DraggableHand _draggableHand;

        public void SetValue(bool value)
        {
            _draggableHand.enabled = value;
        }
    }
}