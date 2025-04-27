#if UNITY_EDITOR

namespace VodVas.UIOptimizer
{
    public class RawImageOptimizer : BaseComponentOptimizer<UnityEngine.UI.RawImage>
    {
        protected override bool ShouldProcessType(UIOptimizerProfile profile)
        {
            return profile.RawImageSettings.Process;
        }

        protected override void ApplyOptimizations(UnityEngine.UI.RawImage component, UIOptimizerProfile profile)
        {
            if (profile.RawImageSettings.DisableMaskable)
                component.maskable = false;

            if (profile.RawImageSettings.DisableRaycast)
                component.raycastTarget = false;
        }

        public override void UpdateStatistics(ref OptimizationResults results)
        {
            results.ScannedRawImageCount++;
            results.ProcessedRawImageCount++;
        }
    }
}
#endif
