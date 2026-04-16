## Description
Please include a summary of the changes and the related issue. 
Explain your technical choices if you introduced a new system or refactored existing code.

Fixes # (issue number)

## Type of change
Please delete options that are not relevant.
- [ ] Bug fix (non-breaking change which fixes an issue)
- [ ] New feature (non-breaking change which adds functionality)
- [ ] Breaking change (fix or feature that would cause existing functionality to not work as expected)
- [ ] Documentation update

## Checklist:
Before submitting this PR, please verify the following:
- [ ] My code follows the style guidelines of this project (no global variables, proper encapsulation).
- [ ] I have used the correct namespaces (`Menu.Runtime` or `Menu.Editor`).
- [ ] Any new animation code is wrapped in `#if DOTWEEN` directives to prevent compilation errors for users without DOTween.
- [ ] I have tested my changes in a clean Unity project.
- [ ] I have updated the documentation (`README.md`) if necessary.
