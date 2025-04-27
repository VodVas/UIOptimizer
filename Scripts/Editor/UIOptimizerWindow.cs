#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace VodVas.UIOptimizer
{
    public class UIOptimizerWindow : EditorWindow
    {
        [SerializeField] private UIOptimizerProfile _profile;

        private ProfileService _profileService;
        private UIOptimizerPresenter _presenter;
        private UIOptimizerView _view;
        private SerializedObject _serializedObject;

        [MenuItem("Tools/VodVas/UI Optimizer Pro")]
        public static void ShowWindow()
        {
            var window = GetWindow<UIOptimizerWindow>("UI Optimizer Pro");
            window.minSize = new Vector2(500, 600);
        }

        private void OnEnable()
        {
            _serializedObject = new SerializedObject(this);
            _profileService = new ProfileService();
            _profile = _profileService.GetOrCreateProfile();

            var optimizerEngine = new OptimizerEngine();

            _presenter = new UIOptimizerPresenter(_profileService, optimizerEngine);
            _view = new UIOptimizerView(_presenter);

            _presenter.Initialize(_profile);
            EditorApplication.update += OnUpdate;
        }

        private void OnDisable()
        {
            EditorApplication.update -= OnUpdate;
            _presenter.Cleanup();
        }

        private void OnGUI()
        {
            _serializedObject.Update();
            _view.Draw();
            _serializedObject.ApplyModifiedProperties();
        }

        private void OnUpdate()
        {
            _presenter.Update();
            if (_presenter.IsDirty)
            {
                Repaint();
                _presenter.ResetDirty();
            }
        }
    }
}
#endif