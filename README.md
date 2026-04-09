# Menu Template

Un système de menu d'interface utilisateur (UI) prêt à l'emploi pour Unity. Il fournit une fondation solide pour les jeux nécessitant un menu principal avec la gestion des paramètres, le changement de langue et la reconfiguration des touches.

## Prérequis Techniques

Ce package est conçu pour **Unity 6000.0** et s'appuie sur les technologies suivantes :
* **Input System** (`com.unity.inputsystem`) pour la gestion et la reconfiguration des touches.
* **Newtonsoft JSON** (`com.unity.nuget.newtonsoft-json`) pour la localization.

*Note : Ces dépendances sont inscrites dans le `package.json` et seront résolues automatiquement par Unity lors de l'installation.*

## Installation via Git URL

Utilisez le Unity Package Manager (UPM) pour installer ce package directement depuis GitHub.

1. Dans Unity, ouvrez `Window > Package Manager`.
2. Cliquez sur l'icône `+` dans le coin supérieur gauche.
3. Sélectionnez `Add package from git URL...`.
4. Renseignez l'URL de votre dépôt :
   `https://github.com/[votre-compte-github]/[nom-du-depot].git`

*(Optionnel) Pour cibler une version spécifique, ajoutez `#v1.0.0` à la fin de l'URL.*

## Utilisation Rapide

Pour intégrer le menu dans votre scène :
1. Allez dans `Packages/Menu Template/Runtime/Prefabs` (ou le chemin exact de votre prefab).
2. Glissez-déposez le Prefab `[NomDuPrefabDeMenu]` dans votre hiérarchie.
3. Assurez-vous d'avoir un composant `EventSystem` fonctionnel dans votre scène (Unity le génère automatiquement lors de la création d'un Canvas).
