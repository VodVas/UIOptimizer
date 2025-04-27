#if UNITY_EDITOR
using UnityEngine.UI;

namespace VodVas.UIOptimizer
{
    public class TextOptimizer : BaseComponentOptimizer<Text>
    {
        protected override bool ShouldProcessType(UIOptimizerProfile profile)
        {
            return profile.TextSettings.Process;
        }

        protected override void ApplyOptimizations(Text component, UIOptimizerProfile profile)
        {
            if (profile.TextSettings.DisableMaskable)
                component.maskable = false;

            if (profile.TextSettings.DisableRaycast)
                component.raycastTarget = false;
        }

        public override void UpdateStatistics(ref OptimizationResults results)
        {
            results.ScannedTextCount++;
            results.ProcessedTextCount++;
        }
    }
}
#endif