#if UNITY_EDITOR
using TMPro;

namespace VodVas.UIOptimizer
{
    public class TMP_TextOptimizer : BaseComponentOptimizer<TMP_Text>
    {
        protected override bool ShouldProcessType(UIOptimizerProfile profile)
        {
            return profile.TextSettings.Process;
        }

        protected override void ApplyOptimizations(TMP_Text component, UIOptimizerProfile profile)
        {
            if (profile.TextSettings.DisableMaskable)
                component.maskable = false;

            if (profile.TextSettings.DisableRaycast)
                component.raycastTarget = false;
        }

        public override void UpdateStatistics(ref OptimizationResults results)
        {
            results.ScannedTMPTextCount++;
            results.ProcessedTMPTextCount++;
        }
    }
}
#endif