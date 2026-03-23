using System;
using UnityEngine;

namespace SpongeContent
{
    public class SpongeHandler : MonoBehaviour
    {
        public event Action Cleaning;
    
        public void CleanMakeUp()
        {
            Debug.Log("CleanmakeUp");
            Cleaning?.Invoke();
        }
    }
}