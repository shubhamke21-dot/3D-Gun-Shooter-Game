# 3D Gun Shooter Game

A lightweight PC 3D shooter prototype built for Unity. It includes:
- first-person player movement and mouse look
- weapon system with ammo and reload logic
- inventory for medkits and weapons
- procedural arena map generation
- AI enemies that chase and attack the player
- HUD for health, ammo, and inventory

## Features
- First-person shooter controls for PC
- Inventory with medkits and weapons
- Enemy AI using chase/attack behavior
- Procedural arena with walls and obstacles
- Simple wave-based survival gameplay loop
- HUD and minimap-style layout ready for extension

## Requirements
- Unity 2022.3 LTS or newer
- Windows, Mac, or Linux desktop

## Project structure
- `Assets/Scripts/Player/PlayerController.cs` — player movement, health, and look controls
- `Assets/Scripts/Weapons/WeaponManager.cs` — weapon switching and shooting logic
- `Assets/Scripts/Inventory/InventorySystem.cs` — item inventory and pickup handling
- `Assets/Scripts/Enemies/EnemyAI.cs` — enemy chase and attack behavior
- `Assets/Scripts/Map/ProceduralArena.cs` — map generation and spawn logic
- `Assets/Scripts/UI/HUDController.cs` — in-game UI logic
- `Assets/Scripts/Gameplay/GameManager.cs` — survival loop and enemy management

## Quick setup in Unity
1. Create a new Unity 3D project.
2. Copy these files into the project under the corresponding folders.
3. Create a scene named `Main`.
4. Add a `GameObject` named `Player`.
5. Add a `CharacterController` component to it.
6. Attach `PlayerController`.
7. Add a `Camera` child object to `Player`, set it as the main camera.
8. Add a `WeaponManager` component to the player.
9. Add `InventorySystem`, `GameManager`, and `HUDController` to a root `GameManager` object.
10. Add an `EnemyAI` prefab and spawn via `ProceduralArena`.
11. Press Play.

## Controls
- WASD — move
- Mouse — look around
- Left click — shoot
- R — reload
- Shift — sprint
- Space — jump
- E — use medkit (if available)

## Notes
This project is intentionally built as a compact starter prototype rather than a full commercial game. You can expand it with:
- more weapons and weapon balance
- better enemy animations and state machines
- richer map generation and level design
- pause menus and main menu scenes
- sound effects and VFX
- save/load progression

## Suggested next upgrades
- add NavMesh navigation
- add sound and particle effects
- add weapon rarity and loot drops
- add boss fights and multiple arenas
- add minimap and mission objectives

---
Made for PC desktop play and easy extension in Unity.
