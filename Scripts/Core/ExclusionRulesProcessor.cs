#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace VodVas.UIOptimizer
{
    public class ExclusionRulesProcessor
    {
        private readonly UIOptimizerProfile _profile;
        private HashSet<GameObject> _exclusionCache;

        public ExclusionRulesProcessor(UIOptimizerProfile profile)
        {
            _profile = profile;
            RebuildExclusionCache();
        }

        public void RebuildExclusionCache()
        {
            _exclusionCache = new HashSet<GameObject>();

            for (int i = 0; i < _profile.ManualExclusions.Count; i++)
            {
                GameObject go = _profile.ManualExclusions[i];
                if (go != null) _exclusionCache.Add(go);
            }
        }

        public bool ShouldExclude(GameObject obj)
        {
            if (_exclusionCache.Contains(obj)) return true;

            if (_profile.ExcludeButtons && obj.GetComponent<Button>() != null) return true;
            if (_profile.ExcludeSliders && obj.GetComponent<Slider>() != null) return true;
            if (_profile.ExcludeToggles && obj.GetComponent<Toggle>() != null) return true;
            if (_profile.ExcludeScrollbars && obj.GetComponent<Scrollbar>() != null) return true;
            if (_profile.ExcludeDropdowns && obj.GetComponent<Dropdown>() != null) return true;
            if (_profile.ExcludeInputFields && obj.GetComponent<InputField>() != null) return true;
            if (_profile.ExcludeEventTriggers && obj.GetComponent<EventTrigger>() != null) return true;

            return false;
        }
    }
}
#endif