#if UNITY_EDITOR
using System;
using UnityEngine;

namespace VodVas.UIOptimizer
{
    public struct OptimizationResults
    {
        public int ScannedTextCount;
        public int ProcessedTextCount;

        public int ScannedTMPTextCount;
        public int ProcessedTMPTextCount;

        public int ScannedImageCount;
        public int ProcessedImageCount;

        public int ScannedRawImageCount;
        public int ProcessedRawImageCount;

        public int ScannedPanelCount;
        public int ProcessedPanelCount;

        public int TotalScanned => ScannedTextCount + ScannedImageCount + ScannedRawImageCount + ScannedPanelCount + ScannedTMPTextCount;
        public int TotalProcessed => ProcessedTextCount + ProcessedImageCount + ProcessedRawImageCount + ProcessedPanelCount + ProcessedTMPTextCount;

        public TimeSpan ProcessingTime;

        public override string ToString()
        {
            return $"Processed:\n" +
                   $"Text: {ProcessedTextCount}/{ScannedTextCount}\n" +
                   $"TMP Text: {ProcessedTMPTextCount}/{ScannedTMPTextCount}\n" +
                   $"Images: {ProcessedImageCount}/{ScannedImageCount}\n" +
                   $"Raw Images: {ProcessedRawImageCount}/{ScannedRawImageCount}\n" +
                   $"Panels: {ProcessedPanelCount}/{ScannedPanelCount}\n" +
                   $"Total: {TotalProcessed}/{TotalScanned}\n" +
                   $"Time: {ProcessingTime.TotalMilliseconds:F2}ms";
        }

        public void LogToConsole()
        {
            Debug.Log(
                $"<b>UI Optimizer Results:</b>\n" +
                $"------------------------------\n" +
                $"Text Components: {ProcessedTextCount} processed out of {ScannedTextCount} scanned\n" +
                $"TMP Text Components: {ProcessedTMPTextCount} processed out of {ScannedTMPTextCount} scanned\n" +
                $"Image Components: {ProcessedImageCount} processed out of {ScannedImageCount} scanned\n" +
                $"Raw Image Components: {ProcessedRawImageCount} processed out of {ScannedRawImageCount} scanned\n" +
                $"Panel Components: {ProcessedPanelCount} processed out of {ScannedPanelCount} scanned\n" +
                $"------------------------------\n" +
                $"Total Components Processed: {TotalProcessed}\n" +
                $"Total Components Scanned: {TotalScanned}\n" +
                $"Processing Time: {ProcessingTime.TotalMilliseconds:F2}ms\n" +
                $"------------------------------");
        }
    }
}
#endif