using SpongeContent;
using UnityEngine;

namespace MakeupContent
{
    public abstract class MakeupHandler : MonoBehaviour
    {
        [SerializeField] private SpongeHandler _spongeHandler;

        private void OnEnable()
        {
            _spongeHandler.Cleaning += Cleaning;
        }

        private void OnDisable()
        {
            _spongeHandler.Cleaning -= Cleaning;
        }

        protected abstract void Cleaning();
    }
}