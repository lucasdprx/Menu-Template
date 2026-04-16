# Menu Template

A ready-to-use user interface (UI) menu system for Unity. It provides a solid foundation for games requiring a main menu with settings management, language switching, and key remapping.

## Technical Requirements

This package is designed for **Unity 6000.0** and relies on the following technologies:
* **Input System** (`com.unity.inputsystem`) for input management and key remapping.
* **Newtonsoft JSON** (`com.unity.nuget.newtonsoft-json`) for JSON data handling (settings serialization and localization).

*Note: These dependencies are listed in the `package.json` and will be automatically resolved by Unity during installation.*

## Installation from Git URL

Use the Unity Package Manager (UPM) to install this package directly from GitHub.

1. In Unity, open `Window > Package Manager`.
2. Click the `+` icon in the top left corner.
3. Select `Install package from git URL...`.
4. Enter the repository URL: `https://github.com/lucasdprx/Menu-Template.git`

## Quick Start

This package includes custom Editor tools for a seamless and safe setup.

### 1. Scene Integration
To instantly add the menu to your active scene:
* From the top menu bar, select `Menu Template > Create Menu Template`.

### 2. Input Configuration
To enable the key remapping features:
1. Select the instantiated Menu Prefab in your Hierarchy.
2. Access the key binding settings via the Inspector.
3. Assign your `InputActionReference`.

## Optional Integration: UI Animations (DOTween)

This template natively supports [DOTween (HOTween v2)](https://assetstore.unity.com/packages/tools/animation/dotween-hotween-v2-27676) for enhanced button and menu transition animations. 

To enable this feature:
1. Install and setup DOTween from the Unity Asset Store.
2. Go to `Edit > Project Settings > Player`.
3. Scroll down to the **Other Settings** section.
4. Under **Scripting Define Symbols**, add the word `DOTWEEN`.
5. Click Apply. The template will automatically compile and use the animation scripts.

## Credits & Attribution

This package is distributed under the **MIT License**. 

While the license only requires preserving the copyright notice in the source files, if you use this template in a published game or an academic project, a mention in your game's credits is highly appreciated!

**Suggested credit format:**
> Menu System by Lucas Darpeix