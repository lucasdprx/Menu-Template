# Menu Template

A ready-to-use user interface (UI) menu system for Unity. It provides a solid foundation for games requiring a main menu with settings management, language switching, and key remapping.

## Technical Requirements

This package is designed for **Unity 6000.0** and relies on the following technologies:
* **Input System** (`com.unity.inputsystem`) for input management and key remapping.
* **Newtonsoft JSON** (`com.unity.nuget.newtonsoft-json`) for JSON data handling (settings serialization and custom localization).

*Note: These dependencies are listed in the `package.json` and will be automatically resolved by Unity during installation.*

## Installation via Git URL

Use the Unity Package Manager (UPM) to install this package directly from GitHub.

1. In Unity, open `Window > Package Manager`.
2. Click the `+` icon in the top left corner.
3. Select `Add package from git URL...`.
4. Enter the repository URL: `https://github.com/lucasdprx/Menu-Template.git`

*(Optional) To target a specific version, append `#v1.0.0` to the end of the URL.*

## Quick Start

To integrate the menu into your scene:
1. Navigate to `Packages/Menu-Template/Runtime/Menu/Prefabs`.
2. Drag and drop the `CanvasMenu` Prefab into your Hierarchy.
3. Ensure you have a functional `EventSystem` component in your scene.
4. Access the key binding settings on the prefab via the Inspector, and assign your `InputActionAsset` along with the specific input actions associated with the rebinds.