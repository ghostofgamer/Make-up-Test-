using UnityEngine;

namespace Tools
{
    public abstract class Tool : MonoBehaviour
    {
        public abstract void ApplyToFace();

        public virtual void OnPickUp()
        {
        }

        public virtual void OnDrop()
        {
        }
    }
}