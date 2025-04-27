#if UNITY_EDITOR

namespace VodVas.UIOptimizer
{
    public class ImageOptimizer : BaseComponentOptimizer<UnityEngine.UI.Image>
    {
        protected override bool ShouldProcessType(UIOptimizerProfile profile)
        {
            return profile.ImageSettings.Process;
        }

        protected override void ApplyOptimizations(UnityEngine.UI.Image component, UIOptimizerProfile profile)
        {
            if (profile.ImageSettings.DisableMaskable)
                component.maskable = false;

            if (profile.ImageSettings.DisableRaycast)
                component.raycastTarget = false;
        }

        public override void UpdateStatistics(ref OptimizationResults results)
        {
            results.ScannedImageCount++;
            results.ProcessedImageCount++;
        }
    }
}
#endif