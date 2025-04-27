#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace VodVas.UIOptimizer
{
    public class UIOptimizerPresenter
    {
        private readonly ProfileService _profileService;
        private readonly OptimizerEngine _optimizerEngine;

        private UIOptimizerProfile _profile;
        private OptimizationResults _lastResults;
        private bool _isDirty;

        public bool IsDirty => _isDirty;

        public UIOptimizerPresenter(ProfileService profileService, OptimizerEngine optimizerEngine)
        {
            _profileService = profileService;
            _optimizerEngine = optimizerEngine;
        }

        public void Initialize(UIOptimizerProfile profile)
        {
            _profile = profile;
        }

        public void Update()
        {
            if (_optimizerEngine.IsProcessing && !_isDirty)
            {
                _isDirty = true;
            }
        }

        public void ResetDirty()
        {
            _isDirty = false;
        }

        public UIOptimizerProfile Profile => _profile;

        public void ProcessScene()
        {
            if (!_profileService.ValidateSettings(_profile))
            {
                EditorUtility.DisplayDialog("Error", "No components selected for processing!", "OK");
                return;
            }

            _lastResults = _optimizerEngine.ProcessScene(_profile);
            _isDirty = true;
        }

        public void ValidateExclusions()
        {
            _profileService.ValidateExclusions(_profile);
            _isDirty = true;
        }

        public void SaveProfile()
        {
            _profileService.SaveProfile(_profile);
        }

        public void CreateNewProfile()
        {
            _profile = _profileService.CreateNewProfile();
            _isDirty = true;
        }

        public void AddExclusion(GameObject gameObject)
        {
            if (gameObject != null && !_profile.ManualExclusions.Contains(gameObject))
            {
                Undo.RecordObject(_profile, "Add Excluded Object");
                _profile.ManualExclusions.Add(gameObject);
                _isDirty = true;
            }
        }

        public void RemoveExclusion(int index)
        {
            if (index >= 0 && index < _profile.ManualExclusions.Count)
            {
                Undo.RecordObject(_profile, "Remove Excluded Object");
                _profile.ManualExclusions.RemoveAt(index);
                _isDirty = true;
            }
        }

        public void ClearExclusions()
        {
            Undo.RecordObject(_profile, "Clear Exclusions");
            _profile.ManualExclusions.Clear();
            _isDirty = true;
        }

        public OptimizationResults GetLastResults() => _lastResults;

        public void Cleanup()
        {
        }
    }
}
#endif