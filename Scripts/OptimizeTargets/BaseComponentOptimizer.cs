#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace VodVas.UIOptimizer
{
    public abstract class BaseComponentOptimizer<T> : IUIComponentOptimizer where T : Component
    {
        public bool CanProcess(GameObject gameObject, UIOptimizerProfile profile)
        {
            return gameObject.GetComponent<T>() != null && ShouldProcessType(profile);
        }

        public void Process(GameObject gameObject, UIOptimizerProfile profile)
        {
            T component = gameObject.GetComponent<T>();
            if (component == null) return;

            Undo.RecordObject(component, $"Optimize {typeof(T).Name}");
            ApplyOptimizations(component, profile);
        }

        public abstract void UpdateStatistics(ref OptimizationResults results);
        protected abstract bool ShouldProcessType(UIOptimizerProfile profile);
        protected abstract void ApplyOptimizations(T component, UIOptimizerProfile profile);
    }
}
#endif