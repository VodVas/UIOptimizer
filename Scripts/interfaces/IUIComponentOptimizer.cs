#if UNITY_EDITOR
using UnityEngine;

namespace VodVas.UIOptimizer
{
    public interface IUIComponentOptimizer
    {
        bool CanProcess(GameObject gameObject, UIOptimizerProfile profile);
        void Process(GameObject gameObject, UIOptimizerProfile profile);
        void UpdateStatistics(ref OptimizationResults results);
    }
}
#endif