#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;

namespace VodVas.UIOptimizer
{
    [CreateAssetMenu(fileName = "UIOptimizerProfile", menuName = "UIOptimizer/Profile")]
    public class UIOptimizerProfile : ScriptableObject
    {
        [SerializeField] private ComponentSettings _textSettings = new ComponentSettings();
        [SerializeField] private ComponentSettings _imageSettings = new ComponentSettings();
        [SerializeField] private ComponentSettings _rawImageSettings = new ComponentSettings();
        [SerializeField] private ComponentSettings _panelSettings = new ComponentSettings();
        [SerializeField] private bool _includeInactive = true;
        [SerializeField] private bool _excludeButtons = true;
        [SerializeField] private bool _excludeText = true;
        [SerializeField] private bool _excludeTMPText = true;
        [SerializeField] private bool _excludeSliders = true;
        [SerializeField] private bool _excludeToggles = true;
        [SerializeField] private bool _excludeScrollbars = true;
        [SerializeField] private bool _excludeDropdowns = true;
        [SerializeField] private bool _excludeInputFields = true;
        [SerializeField] private bool _excludeEventTriggers = true;
        [SerializeField] private List<GameObject> _manualExclusions = new List<GameObject>();

        public ComponentSettings TextSettings => _textSettings;
        public ComponentSettings ImageSettings => _imageSettings;
        public ComponentSettings RawImageSettings => _rawImageSettings;
        public ComponentSettings PanelSettings => _panelSettings;
        public bool IncludeInactive => _includeInactive;
        public bool ExcludeButtons => _excludeButtons;
        public bool ExcludeText => _excludeText;
        public bool ExcludeTMPText => _excludeTMPText;
        public bool ExcludeSliders => _excludeSliders;
        public bool ExcludeToggles => _excludeToggles;
        public bool ExcludeScrollbars => _excludeScrollbars;
        public bool ExcludeDropdowns => _excludeDropdowns;
        public bool ExcludeInputFields => _excludeInputFields;
        public bool ExcludeEventTriggers => _excludeEventTriggers;
        public List<GameObject> ManualExclusions => _manualExclusions;
    }
}
#endif
