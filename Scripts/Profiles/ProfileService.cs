#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.IO;

namespace VodVas.UIOptimizer
{
    public class ProfileService
    {
        private const string DEFAULT_FOLDER_PATH = "Assets/Editor/UIOptimizer";
        private const string DEFAULT_PROFILE_NAME = "DefaultProfile.asset";

        public UIOptimizerProfile GetOrCreateProfile()
        {
            string[] guids = AssetDatabase.FindAssets("t:UIOptimizerProfile");
            if (guids.Length > 0)
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[0]);
                return AssetDatabase.LoadAssetAtPath<UIOptimizerProfile>(path);
            }

            return CreateNewProfile();
        }

        public UIOptimizerProfile CreateNewProfile()
        {
            EnsureDirectoryExists();
            string assetPath = $"{DEFAULT_FOLDER_PATH}/{DEFAULT_PROFILE_NAME}";

            UIOptimizerProfile profile = ScriptableObject.CreateInstance<UIOptimizerProfile>();
            AssetDatabase.CreateAsset(profile, assetPath);
            AssetDatabase.SaveAssets();
            return profile;
        }

        public void SaveProfile(UIOptimizerProfile profile)
        {
            if (profile == null) return;

            EditorUtility.SetDirty(profile);
            AssetDatabase.SaveAssets();
        }

        public void ValidateExclusions(UIOptimizerProfile profile)
        {
            if (profile == null) return;

            Undo.RecordObject(profile, "Validate Exclusions");
            profile.ManualExclusions.RemoveAll(go => go == null);
        }

        public bool ValidateSettings(UIOptimizerProfile profile)
        {
            return profile != null && (
                profile.TextSettings.Process ||
                profile.ImageSettings.Process ||
                profile.RawImageSettings.Process ||
                profile.PanelSettings.Process
            );
        }

        private void EnsureDirectoryExists()
        {
            if (!Directory.Exists(DEFAULT_FOLDER_PATH))
            {
                Directory.CreateDirectory(DEFAULT_FOLDER_PATH);
            }
        }
    }
}
#endif