# Menu Template

A ready-to-use user interface (UI) menu system for Unity. It provides a solid foundation for games requiring a main menu with settings management, language switching, and key remapping.

## Technical Requirements

This package is designed for **Unity 6000.0** and relies on the following technologies:
* **Input System** (`com.unity.inputsystem`) for input management and key remapping.
* **Newtonsoft JSON** (`com.unity.nuget.newtonsoft-json`) for JSON data handling (settings serialization and custom localization).

*Note: These dependencies are listed in the `package.json` and will be automatically resolved by Unity during installation.*

## Installation from Git URL

Use the Unity Package Manager (UPM) to install this package directly from GitHub.

1. In Unity, open `Window > Package Manager`.
2. Click the `+` icon in the top left corner.
3. Select `Add package from git URL...`.
4. Enter the repository URL: `https://github.com/lucasdprx/Menu-Template.git`

*(Optional) To target a specific version, append `#v1.0.0` to the end of the URL.*

## Quick Start

This package includes custom Editor tools for a seamless and safe setup.

### 1. Scene Integration
To instantly add the menu to your active scene:
1. From the top menu bar, select `Menu Template > Create Menu Template`.
2. Ensure your scene contains a functional `EventSystem` component.

### 2. Input Configuration
To enable the key remapping features:
1. Select the instantiated Menu Prefab in your Hierarchy.
2. Access the key binding settings via the Inspector.
3. Assign your `InputActionAsset` along with the specific input actions associated with the rebinds.