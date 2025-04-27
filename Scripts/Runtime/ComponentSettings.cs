#if UNITY_EDITOR
using UnityEngine;
using System;

namespace VodVas.UIOptimizer
{
    [Serializable]
    public class ComponentSettings
    {
        [SerializeField] private bool _process = true;
        [SerializeField] private bool _disableMaskable = true;
        [SerializeField] private bool _disableRaycast = true;

        public bool Process => _process;
        public bool DisableMaskable => _disableMaskable;
        public bool DisableRaycast => _disableRaycast;
    }
}
#endif