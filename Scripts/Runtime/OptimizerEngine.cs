#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Diagnostics;
using TMPro;

namespace VodVas.UIOptimizer
{
    public class OptimizerEngine
    {
        private readonly List<IUIComponentOptimizer> _optimizers = new List<IUIComponentOptimizer>();
        private bool _isProcessing;

        public bool IsProcessing => _isProcessing;

        public OptimizerEngine()
        {
            RegisterOptimizers();
        }

        private void RegisterOptimizers()
        {
            _optimizers.Add(new TextOptimizer());
            _optimizers.Add(new TMP_TextOptimizer());
            _optimizers.Add(new ImageOptimizer());
            _optimizers.Add(new RawImageOptimizer());
        }

        public OptimizationResults ProcessScene(UIOptimizerProfile profile)
        {
            _isProcessing = true;
            var results = new OptimizationResults();
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            try
            {
                Undo.SetCurrentGroupName("UI Optimization");
                int undoGroup = Undo.GetCurrentGroup();

                Canvas[] canvases = GameObject.FindObjectsOfType<Canvas>(profile.IncludeInactive);
                HashSet<GameObject> excludedObjects = new HashSet<GameObject>(profile.ManualExclusions);

                AddAutomaticExclusions(excludedObjects, profile, ref results);

                foreach (var canvas in canvases)
                {
                    ProcessHierarchy(canvas.gameObject, excludedObjects, profile, ref results);
                }

                Undo.CollapseUndoOperations(undoGroup);
            }
            finally
            {
                _isProcessing = false;
            }

            stopwatch.Stop();
            results.ProcessingTime = stopwatch.Elapsed;
            results.LogToConsole();

            return results;
        }

        private void ProcessHierarchy(GameObject root, HashSet<GameObject> excludedObjects,
                                     UIOptimizerProfile profile, ref OptimizationResults results)
        {
            Queue<GameObject> processQueue = new Queue<GameObject>();
            processQueue.Enqueue(root);

            while (processQueue.Count > 0)
            {
                GameObject current = processQueue.Dequeue();

                if (excludedObjects.Contains(current) ||
                    (!profile.IncludeInactive && !current.activeInHierarchy))
                {
                    continue;
                }

                ProcessGameObject(current, profile, ref results);

                for (int i = 0; i < current.transform.childCount; i++)
                {
                    processQueue.Enqueue(current.transform.GetChild(i).gameObject);
                }
            }
        }

        private void ProcessGameObject(GameObject gameObject, UIOptimizerProfile profile, ref OptimizationResults results)
        {
            foreach (var optimizer in _optimizers)
            {
                if (optimizer.CanProcess(gameObject, profile))
                {
                    optimizer.Process(gameObject, profile);
                    optimizer.UpdateStatistics(ref results);
                }
            }
        }

        private void AddAutomaticExclusions(HashSet<GameObject> excludedObjects, UIOptimizerProfile profile, ref OptimizationResults results)
        {
            if (profile.ExcludeButtons)
                AddComponentsToExclusions<Button>(excludedObjects, profile.IncludeInactive);

            if (profile.ExcludeTMPText)
                AddComponentsToExclusions<TMP_Text>(excludedObjects, profile.IncludeInactive);

            if (profile.ExcludeSliders)
                AddComponentsToExclusions<Slider>(excludedObjects, profile.IncludeInactive);

            if (profile.ExcludeToggles)
                AddComponentsToExclusions<Toggle>(excludedObjects, profile.IncludeInactive);

            if (profile.ExcludeScrollbars)
                AddComponentsToExclusions<Scrollbar>(excludedObjects, profile.IncludeInactive);

            if (profile.ExcludeDropdowns)
                AddComponentsToExclusions<Dropdown>(excludedObjects, profile.IncludeInactive);

            if (profile.ExcludeInputFields)
                AddComponentsToExclusions<InputField>(excludedObjects, profile.IncludeInactive);

            if (profile.ExcludeEventTriggers)
                AddComponentsToExclusions<EventTrigger>(excludedObjects, profile.IncludeInactive);
        }

        private void AddComponentsToExclusions<T>(HashSet<GameObject> excludedObjects, bool includeInactive) where T : Component
        {
            T[] components = GameObject.FindObjectsOfType<T>(includeInactive);
            foreach (var component in components)
            {
                if (component != null && component.gameObject != null)
                {
                    excludedObjects.Add(component.gameObject);
                }
            }
        }
    }
}
#endif
