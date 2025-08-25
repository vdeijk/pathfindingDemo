# Pathfinding Demo – .NET Code Showcase

## Executive Summary
This project demonstrates professional .NET practices in a game-like context. It showcases clean architecture, dependency injection, and service-oriented design, all structured for maintainability and scalability. Perfect for reviewing advanced C# skills applied in a real-world scenario.

## Key Features

**A Grid-Based Pathfinding**
- Player automatically navigates around obstacles.
- Vegetation and high-cost tiles influence path selection.

**Enemy Patrols**
- Move intelligently between points with configurable wait times.

**Clean, Layered Architecture**
- **Data** – Pure objects (agents, grid, camera).
- **Services** – Core logic (movement, pathfinding, audio, level management).
- **Controllers** – Orchestrate actions, trigger behaviors, connect services.

**Dependency Injection**
- Demonstrates DI patterns in Unity-like systems (Zenject example included).

**Modular DLL Structure**
- Mirrors multi-project setups in standard .NET applications.

## Technical Highlights
- Follows SOLID principles and service-oriented design.
- Implements singleton & service patterns cleanly.
- Separates data, logic, and orchestration for maintainability.
- Uses events and signals for communication where appropriate.
- Code is framework-agnostic, readable, and maintainable.

## Why Review This
- Clear demonstration of professional .NET practices in a Unity-style context.
- Shows ability to structure, modularize, and maintain a medium-scale codebase.
- Highlights advanced C# skills in dependency management, architecture, and pathfinding logic.