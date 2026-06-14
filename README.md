# AR-Measurement

[![Unity Version](https://img.shields.io/badge/Unity-2020.3.32f1-blue.svg)](https://unity.com/)
[![Platform](https://img.shields.io/badge/Platform-iOS%20%7C%20Android-lightgrey.svg)]()
[![AR Support](https://img.shields.io/badge/AR-ARCore%20%2C%20ARKit%20%2C%20AR%20Foundation-green.svg)]()

`AR-Measurement` is an advanced mobile Augmented Reality (AR) application built in Unity, designed to measure botanical features such as leaf surface area, plant height, and tree/stem girth. By combining on-device AR sensors, real-time camera processing, and local sensor integration, it provides an efficient digital alternative for field botanists, agriculturalists, and forestry researchers.

---

## 🌟 Key Features

*   **Leaf Area Calculation**: Place multiple points (up to 8) on a leaf or other planar surfaces to form an AR polygon. The application dynamically computes the enclosed 2D surface area using the surveyor's shoelace formula.
*   **Trunk / Girth Measurement**: Place two horizontal anchors in AR to determine the girth (diameter/circumference) of plant stems or tree trunks.
*   **Height Estimation**: Drop a base anchor on the ground, then adjust a target vertical marker using intuitive "Up" and "Down" UI buttons to determine the vertical height of plants or trees.
*   **Real-time Color Sampler**: Samples pixel colors from a live camera feed `RenderTexture` to extract the hex code and RGB values of botanical specimens.
*   **Ambient Light Detection**: Connects to the device's physical ambient light sensor (via Unity's New Input System) to log lux levels, helping researchers track light exposure in the field.
*   **Flexible Measurement Units**: Dynamically switch between meters (`m`), centimeters (`cm`), inches (`in`), and feet (`ft`). Units are saved locally using `PlayerPrefs`.
*   **On-Device Debug Console**: Built-in runtime logging using the `IngameDebugConsole` plugin to simplify on-device testing and troubleshooting.

---

## 📂 Repository Structure

The following directory tree highlights the core structure of the project:

```text
AR-Measurement/
├── Assets/                       # Unity Project Assets
│   ├── Art/                      # UI sprites, custom shaders, and texturing assets
│   │   ├── Shader/               # Custom shaders (e.g., FeatheredPlaneShader for fading plane edges)
│   │   └── UI/                   # Icon assets for AR controls and panels
│   ├── Material/                 # Material files for rendering mesh indicators and lines
│   ├── Plugins/                  # Third-party plugins
│   │   └── IngameDebugConsole/   # Runtime overlay debug console for on-device logging
│   ├── Prefabs/                  # Reusable GameObjects (AR default plane, marker points, text overlays)
│   ├── Scenes/                   # Main project scenes
│   │   ├── MainMenu.unity        # Application launch scene with UI settings panel
│   │   ├── MeasureLeafArea.unity # UI/AR environment to measure surface area of leaves
│   │   ├── MeasurePlantGirth.unity # UI/AR environment to measure tree trunk/stem girths
│   │   └── MeasurePlantHeight.unity # UI/AR environment to measure tree/plant height
│   └── Scripts/                  # Main C# source code
│       ├── ARFeatheredPlaneMeshVisualizer.cs  # Smooths and feathers boundary edges of detected AR planes
│       ├── Features/             # Main calculation modules
│       │   ├── GetColor.cs       # Live pixel camera sampler for color recognition
│       │   ├── GetLightIntensity.cs # Integrates mobile ambient light sensor reads
│       │   ├── MeasureArea.cs    # Leaf area polygon plotting and calculations (Shoelace formula)
│       │   ├── MeasureGirth.cs   # Distance-based girth calculation
│       │   └── MeasureHeight.cs  # Vertical height adjustment and measurement logic
│       ├── Utils/                # Constants and helper files
│       │   └── AppConstants.cs   # Scene strings and constant key definitions
│       └── ViewManagers/         # Navigation and state handlers
│           ├── HomeScreenViewManager.cs # Handles main menu actions and scene routing
│           └── SettingsViewManager.cs   # Handles measurement unit updates and persistence
├── Packages/                     # Unity Package Manager files
│   ├── manifest.json             # Lists dependent packages (AR Foundation, XR Interaction Toolkit, TMP, etc.)
│   └── packages-lock.json        # Exact resolved versions of packages
├── ProjectSettings/              # Unity configuration settings (graphics, inputs, XR setups, etc.)
└── .gitignore                    # Excludes build outputs, cache folders, and auto-generated solution files
```

---

## 📋 Prerequisites

Before opening or building the project, ensure you have the following installed:

*   **Unity Editor**: Version **`2020.3.32f1`** (LTS). Installing via **Unity Hub** is recommended.
*   **Platform Support Modules**:
    *   **Android Build Support** (with Android SDK & NDK Tools, OpenJDK) to compile `.apk` / `.aab`.
    *   **iOS Build Support** (requires macOS and **Xcode** installed) to build for Apple devices.
*   **AR-Compatible Device**:
    *   **Android**: ARCore-supported device running Android 7.0 (API level 24) or higher.
    *   **iOS**: ARKit-supported iPhone or iPad running iOS 11.0 or higher.

---

## 🔧 Installation & Setup

1.  **Clone the Repository**:
    ```bash
    git clone https://github.com/your-username/AR-Measurement.git
    cd AR-Measurement
    ```

2.  **Open in Unity**:
    *   Open **Unity Hub**.
    *   Click the **Add** button.
    *   Select the root directory `AR-Measurement`.
    *   Select the editor version **`2020.3.32f1`**.
    *   Unity will perform an initial import and download packages listed in `Packages/manifest.json`.

---

## 🚀 How to Run

### Testing in Editor
1.  Open the Project in Unity.
2.  Navigate to `Assets/Scenes` and open `MainMenu.unity`.
3.  Press the **Play** button at the top of the editor.
4.  *Note: While UI flow can be verified in-editor, full AR features (surface tracking/plane placement) require a physical device or AR Simulation.*

### Deploying to Android
1.  Open **File** > **Build Settings**.
2.  Select **Android** from the Platform list and click **Switch Platform**.
3.  Ensure the scenes are configured in order:
    *   `Assets/Scenes/MainMenu.unity` (Index 0)
    *   `Assets/Scenes/MeasureLeafArea.unity`
    *   `Assets/Scenes/MeasurePlantGirth.unity`
    *   `Assets/Scenes/MeasurePlantHeight.unity`
4.  Navigate to **Project Settings** > **XR Plug-in Management** and check **ARCore** under the Android tab.
5.  Connect your Android device to your computer via USB (with **USB Debugging** enabled in Developer Options).
6.  Click **Build and Run** in Build Settings, specify a path to save the `.apk`, and wait for deployment.

### Deploying to iOS
1.  Open **File** > **Build Settings**.
2.  Select **iOS** from the Platform list and click **Switch Platform**.
3.  Ensure the scenes are added in the correct order.
4.  Navigate to **Project Settings** > **Player** > **Other Settings** and fill out the **Camera Usage Description** (e.g., *"This application requires the camera for augmented reality measurements"*).
5.  Ensure **XR Plug-in Management** > **ARKIt** is enabled under the iOS tab.
6.  Click **Build** to generate the iOS Xcode Project.
7.  Open the exported Xcode project (`Unity-iPhone.xcodeproj`).
8.  Connect your iOS device, select your signing team in Xcode settings, and click **Run** (Play icon) in Xcode to deploy.

---

## 🧪 Running Tests

This repository contains the Unity Test Framework (`com.unity.test-framework`) inside `Packages/manifest.json`. Currently, there are no custom automated Unit or Integration tests.

To run or write test cases:
1.  In the Unity Editor, open **Window** > **General** > **Test Runner**.
2.  Here, you can write and execute **EditMode** or **PlayMode** tests for the scripts.

---

## 🛠️ Diagnostics & Code Overview

Below are code references demonstrating the key botanical arithmetic implemented in the codebase:

### Leaf Area Calculation (Shoelace Formula)
From [MeasureArea.cs](file:///Users/soni/Personal/Github%20Projects%20to%20make%20md%20and%20make%20description%20for%20resume/AR-Measurement/Assets/Scripts/Features/MeasureArea.cs#L92-L116):
```csharp
int totalPoints = lineRenderer.positionCount;
float temp = 0, area = 0;
if (totalPoints > 2)
{
    for (int i = 0; i < totalPoints; i++)
    {
        if (i != (totalPoints - 1))
        {
            float mulA = lineRenderer.GetPosition(i).x * lineRenderer.GetPosition(i + 1).z;
            float mulB = lineRenderer.GetPosition(i + 1).x * lineRenderer.GetPosition(i).z;
            temp = temp + (mulA - mulB);
        }
        else
        {
            float mulA = lineRenderer.GetPosition(i).x * lineRenderer.GetPosition(0).z;
            float mulB = lineRenderer.GetPosition(0).x * lineRenderer.GetPosition(i).z;
            temp = temp + (mulA - mulB);
        }
    }
    area = Mathf.Abs(temp / 2) * unitConverter * unitConverter;
}
```
This takes the 2D plane coordinates (x and z axes) plotted by the user and integrates them to calculate planar area.
