# Wizzie

> **Coming soon:** the game is still in development. Next up:
>
> - Parallax background
> - Camera that follows the player
> - A complete, playable level
> - More enemies and pickups

A 2D side-scroller made in Unity for *Trabajo Práctico N°07*. You play a wizard who runs, jumps and throws fireballs through a tile-based level full of patrolling musketeers.

## Gameplay

- Move left and right, jump and throw fireballs.
- Musketeers patrol their platform, pause at the ends, chase you when you get close and shoot when you are in range. Touching them also hurts.
- Every enemy has a health bar above its head and needs four fireballs to go down. They burst into particles when they die.
- Defeated enemies can drop a red crystal that restores health.
- The HUD shows the elapsed time and the number of enemies defeated.
- When your health reaches zero the game freezes and the Game Over panel lets you retry or go back to the main menu.

## Controls

| Action | Key |
| --- | --- |
| Move | `A` / `D` |
| Jump | `Space` |
| Throw fireball | `Q` |
| Pause / settings | **Settings** button (top right) |

The movement and jump keys are configurable in the `PlayerData` ScriptableObject.

## Scenes

| Scene | Purpose |
| --- | --- |
| `Assets/Scenes/MainMenu.unity` | Main menu with Play, Settings, Credits and Quit |
| `Assets/Scenes/SampleScene.unity` | Gameplay level |

Both scenes are in the Build Settings, with `MainMenu` first.

## Getting started

1. Install **Unity 6000.3.21f1** (Unity 6) through Unity Hub.
2. Clone the repository and add the project folder in Unity Hub.
3. Open `Assets/Scenes/MainMenu.unity` and press Play.

## Architecture

### Hierarchical state machines

`State<TContext>` and `StateMachine<TContext>` are generic and shared by the player and the enemies. A state can own sub-states, so shared transitions live in the parent.

- **Player:** `Grounded` (Idle, Run, Land, Throw) and `Airborne` (Jump, Fall). Animation speeds follow gameplay values: the jump clip matches the rise time, the throw clip matches the release time and the run clip matches the movement speed.
- **Enemy:** Patrol, Idle, Chase and Shoot. A ground check ahead of the feet keeps enemies from walking off their platforms.

Each character builds its states in a setup class (`PlayerStateSetup`, `EnemyStateSetup`) and shares data through a context object (`PlayerContext`, `EnemyContext`).

### ScriptableObjects

| Asset | Contents |
| --- | --- |
| `PlayerSO` | Movement, jump, health, throw timing, ground check and input keys |
| `EnemySO` | Health, patrol, detection, shooting, contact damage and death drops |
| `PlayerAnimationsSO` | Animation clips per state, used by the player and the musketeer |

### Other systems

- **Health:** a reusable `Health` component with `Changed` and `Died` events, used by the player and the enemies.
- **Object pooling:** a generic `ObjectPool<T>` for projectiles.
- **Pickups:** an abstract `Pickup` base class; `HealthPickup` heals the player and is ignored at full health.
- **Audio:** an `AudioManager` routes music and effects through `GameAudio.mixer`. The Settings panel controls the Master, Music and Effects volumes, which are saved in `PlayerPrefs`.
- **UI:** world-space health bars, the HUD, Game Over and reusable menu buttons (`UIMenuButton` and subclasses).

## Project structure

```
Assets/
├── Art/          Sprites, animations, tileset and sprite atlas
├── Audio/        Audio mixer and sound clips
├── Data/         ScriptableObject assets (player, enemy, animations)
├── Prefab/       Characters, projectiles, pickups, effects and UI
├── Scenes/       MainMenu and gameplay scenes
└── Scripts/      Audio, Combat, Data, Enemy, Globals, Pickups,
                  Player, Pool, Projectiles, Spawners, UI
```

## Credits

- **Tileset:** *Pixel Art Platformer - Village Props* by Cainos
- **Health bar UI:** *Pixel UI pack 3*
- **Crystal pickup:** *Crystal Animation (5 colors)*
- **Characters:** Wizard and Musketeer sprite sheets

Made by Benjamin Leguizamon.
