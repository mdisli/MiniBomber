# MiniBomber

A classic Bomberman-style 2D game built with Unity.

## Requirements

- Unity 2022.3.62f1 (LTS)
- DOTween
- UniTask

## Project Structure

```
Assets/_Workspace/Scripts/
├── Animation/          # Sprite animation system
├── Bomb/               # Bomb mechanics and explosions
├── Enemy/              # Enemy AI and movement
├── Grid System/        # Tilemap-based grid management
├── Managers/           # Game and camera managers
├── Player/             # Player controller and input
├── Scriptable Objects/ # Game events and variables
├── UI Scripts/         # UI screens and input buttons
└── Walls/              # Destructible and regenerative walls
```

## Key Features

- Destructible and regenerative walls
- Enemy AI with basic pathfinding
- Event-driven architecture using ScriptableObjects
- Mobile-ready input system

## Controls

- **WASD** - Movement
- **Space** - Drop bomb
- Mobile touch buttons supported