# 🚀 UI Optimizer Pro for Unity ![GitHub](https://img.shields.io/badge/Unity-2021.3%2B-blue) ![GitHub](https://img.shields.io/badge/License-MIT-green)

**Automate UI optimization like a pro** - Save 90% time on performance optimization with smart component processing and intelligent exclusion rules!

**💼 All-in-One Optimization**  
   Automatically processes:
   - 🖼️ Images/Raw Images
   - 📝 Legacy Text & TMP Text
   - 🖌️ Maskable components
   - 🎯 Raycast targets

**🎯 Surgical Precision Controls**  
```csharp
   [Serializable]
   public class ComponentSettings
{
       [SerializeField] private bool _process = true;
       [SerializeField] private bool _disableMaskable = true;
       [SerializeField] private bool _disableRaycast = true;
}
```
Per-component settings with one-click presets!

**🛡️ Smart Exclusion System**

Auto-exclude interactive elements (Buttons, Sliders)

Manual object exclusions

Scene-wide filters

Type-based blacklisting

**📊 Real-Time Analytics**
 ```
plaintext
UI Optimizer Results:
------------------------------
Text Components: 42 processed
TMP Text: 18 optimized 
Images: 67 disabled raycasts
------------------------------
Total Saved: 127 operations
Time: 156ms
 ```
**⚡ Editor-First Design**

Full Undo/Redo support

Drag-n-drop exclusions

Profile system (Create/Save/Load)

Batch processing for entire scenes

**🚀 Quick Start**
Installation

bash
git clone https://github.com/YourRepo/UI-Optimizer-Pro.git
Import UIOptimizer folder into Unity Editor

First Optimization

csharp
Tools → VodVas → UI Optimizer Pro
Editor Window Demo

Create Profile

csharp
[CreateAssetMenu(fileName = "UIOptimizerProfile", 
                menuName = "UIOptimizer/Profile")]
Save settings per project/scene!

**🎮 Usage Scenarios**
Case 1: 
Mobile Menu Optimization

 ```
1. Exclude all Buttons ← Keep interactivity
2. Disable raycasts on Background images
3. Process 150+ UI elements in 200ms
 ```
Case 2:
HUD Performance Boost
 ```
1. Target only Text components
2. Preserve TMP counters
3. Achieve 62% draw call reduction
 ```
**🛠️ Configuration Deep Dive**
Core Settings Structure:
 ```csharp
public class UIOptimizerProfile : ScriptableObject
{
    [SerializeField] ComponentSettings _textSettings;  // Legacy & TMP
    [SerializeField] bool _excludeButtons = true;      // Auto-skip UI
    [SerializeField] List<GameObject> _manualExclusions; // Scene refs
}
 ```
Pro Tip: Use multiple profiles for:

**👷♂️ Development (safer settings)**

**🚀 Production (aggressive optimization)**

**📱 Mobile (extra raycast disabling)**

**📜 License**
MIT License - Free for commercial use. Clone, modify, distribute!

Copyright 2023 VodVas

Permission includes rights to use, copy, modify, merge with ANY Unity project, 
including commercial titles. Attribution appreciated but not required.

Transform your UI workflow today - What took hours now takes seconds! 🕒
