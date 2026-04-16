# Contributing to Menu Template

First off, thank you for considering contributing to this project! It's people like you who make this a great tool for the student community.

## Code of Conduct
By participating in this project, you agree to maintain a respectful and professional environment.

## How Can I Contribute?

### Reporting Bugs
* Check the [Issues tab](https://github.com/lucasdprx/Menu-Template/issues) to see if the bug has already been reported.
* If not, create a new issue. Include your Unity version, a clear description of the problem, and steps to reproduce it.

### Suggesting Enhancements
* Open an issue first to discuss the idea. 
* We aim to keep this package focused on **UI and Menu logic only**. Features related to gameplay mechanics will likely be declined to maintain the "Single Responsibility Principle".

### Pull Requests
1. **Fork** the repository and create your branch from `main`.
2. Ensure your code follows the project's standards:
    * Use the namespace `PtrkGames.MenuTemplate.Runtime` or `PtrkGames.MenuTemplate.Editor`.
    * Do not use global variables; use proper encapsulation.
    * Use `#if DOTWEEN` for any animation-related code.
3. Update the documentation (README) if your changes affect the setup process.
4. Submit a Pull Request with a clear description of the changes.

## Development Environment
* Developed with **Unity 6000.x**. 
* Ensure all dependencies (Input System, Newtonsoft JSON) are present in your test project.

## License
By contributing, you agree that your contributions will be licensed under the project's **MIT License**.
