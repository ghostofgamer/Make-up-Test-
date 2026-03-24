using System;
using UnityEngine;

namespace MakeupContent.SpongeContent
{
    public class SpongeHandler : MonoBehaviour
    {
        public event Action Cleaning;

        public void CleanMakeUp() => Cleaning?.Invoke();
    }
}