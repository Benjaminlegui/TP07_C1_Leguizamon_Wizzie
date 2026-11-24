# Corrección, Trabajo Práctico N°07 (Side Scroller 2D)

- **Alumno/a:** Leguizamon
- **Entrega:** `TP07_C1_Leguizamon_Wizzie`
- **Materia:** Programación con Motores de Videojuegos I
- **Nota TP07:** 3

## 1. Checklist de requisitos

| #   | Requisito | Estado | Evidencia |
|-----|-----------|--------|-----------|
| R1  | Repositorio nuevo con nombre `TP07_C1_Apellido_Titulo` | ✓ | Carpeta `TP07_C1_Leguizamon_Wizzie`, `.git/config` apunta a `.../TP07_C1_Leguizamon_Wizzie` |
| R2  | Proyecto de Unity con el mismo nombre | Parcial | En `ProjectSettings/ProjectSettings.asset` el `productName` es `TP07_C1_Leguizamon_Platformer`, no `..._Wizzie` |
| R3  | El personaje va a la derecha y a la izquierda | ✓ | `Assets/Scripts/Player/Player.cs:98` (`ReadMoveInput`), `Assets/Scripts/Player/PlayerContext.cs:27` (`MoveHorizontal`) |
| R4  | El personaje puede disparar | ✓ | `Assets/Scripts/Player/States/Ground/PlayerThrowState.cs:42` (`Fire` con `ObjectPool<Fireball>`) |
| R5  | El personaje puede saltar | ✓ | `Assets/Scripts/Player/States/Ground/PlayerGroundState.cs:24`, `Assets/Scripts/Player/States/Air/PlayerJumpState.cs:24` |
| R6  | Enemigos u obstáculos que le quitan vida al jugador | ✓ | `Assets/Scripts/Enemy/Enemy.cs:122` (daño por contacto), `Assets/Prefab/Projectiles/EnemyProjectile.prefab` (`damage: 25`) |
| R7  | Se eliminan con más de 2 disparos, tienen vida y barra visible arriba (Canvas World) | ✓ | `EnemyData.asset` con `maxHealth: 100` y `Fireball.prefab` con `damage: 25` (4 disparos). `HealthBar.prefab` en World Space anidado en `Enemy.prefab`, `Assets/Scripts/UI/UIHealthBar.cs:36` |
| R8  | ParticleSystem al destruir al enemigo | ✓ | `Assets/Scripts/Enemy/Enemy.cs:85`, `Assets/Prefab/Effects/EnemyDeathEffect.prefab` (se destruye al terminar) |
| R9  | Hay Pickables | ✓ | `Assets/Scripts/Pickups/Pickup.cs`, `Assets/Scripts/Pickups/HealthPickup.cs`, una instancia en `SampleScene.unity` y el drop en `Enemy.cs:93` |
| R10 | Sonido en todas las acciones | Parcial | Mirá el detalle abajo |
| R11 | Volumen configurable desde Settings conectado a un AudioMixer | ✓ | `Assets/Scripts/Audio/AudioManager.cs:91-96`, `Assets/Scripts/Audio/AudioSettingsUI.cs:15-24`, `GameAudio.mixer` con 3 parámetros expuestos y AudioSources ruteados a los grupos Music y Effects en las dos escenas |
| R12 | Usa ScriptableObject para datos o configuraciones | ✓ | `Assets/Scripts/Data/PlayerSO.cs`, `EnemySO.cs`, `PlayerAnimationsSO.cs`, con assets en `Assets/Data/` que leés desde `Player.cs` y `Enemy.cs` |
| R13 | Power ups o sutil modificación de jugabilidad en un caso determinado | ✗ | No hay power ups. La aceleración de `Assets/Scripts/Spawners/EnemySpawner.cs:25-39` nunca corre porque `EnemySpawner` no está en `SampleScene.unity` (la lista `spawners` del `GameManager` está vacía en la escena) |
| R14 | El personaje está animado | ✓ | `Assets/Art/Animations/Player/Sprite.controller` con 6 clips y parámetros de velocidad, y estados que llaman a `Animator.Play` |
| R15 | Escena de menú principal y otra de Gameplay | ✓ | `ProjectSettings/EditorBuildSettings.asset`: `MainMenu.unity` y `SampleScene.unity`, `Assets/Scripts/Globals/SceneLoader.cs` |

Detalle de R10 (sonido):

| Acción | Estado | Evidencia |
|--------|--------|-----------|
| Música de gameplay | ✗ | `gameplayMusic` está vacío en `SampleScene.unity`, y `AudioManager.PlayMusic` sale sin reproducir si el clip es null (`AudioManager.cs:60`) |
| Saltar | ✓ (placeholder) | `AudioGameplay.cs` se suscribe a `Player.Jumped`, clip `Placeholder.wav` |
| Disparar | ✓ (placeholder) | `Player.Threw`, mismo `Placeholder.wav` |
| Agarrar coleccionable | ✗ | `Pickup.PickedUp` no tiene suscriptores (`Pickup.cs:17`) |
| Eliminar enemigo | ✗ | `Enemy.Died` solo lo escucha `UIGameHUD`, y `EnemyDeathEffect.prefab` no tiene AudioSource |
| Al perder | ✓ (placeholder) | `AudioGameplay.OnGameChanged`, mismo `Placeholder.wav` |

Saltar, disparar, aterrizar y perder suenan con el mismo clip de 0,15 s (`Assets/Audio/Placeholder.wav`), así que no se distinguen entre sí.

## 2. Cumplimiento de Pautas de Cursada

| ID | Pauta | Estado | Evidencia |
|----|-------|--------|-----------|
| P1 | Unity 6000.3.21f1 | ✓ | `ProjectSettings/ProjectVersion.txt` dice `6000.3.21f1` |
| P2 | Repositorio público | ✓ | El repositorio es público (al 2026-11-16) |
| P3 | Nombre del repo | ✓ | `TP07_C1_Leguizamon_Wizzie` respeta `TPxx_Cn_Apellido_Titulo` |
| P4 | Código en inglés | ✓ | Clases, métodos, campos, escenas y capas en inglés en los 53 scripts. La capa 10 se llama `Enemy Pojectile` (typo, pero en inglés) |
| P5 | Un repo por TP, sin branches | ✓ | Solo existe `main` |

## 3. Estructura de carpetas

| Carpeta esperada | Encontrada | Observación |
|------------------|------------|-------------|
| `Art/Animations/` | ✓ | Player, Enemies/Musketeer, Pickable |
| `Art/Sprites/` | ✓ | Player, Enemies, Enviroment, Pickable, Projectiles, UI |
| `Art/Audio/` | ✗ | El audio está en `Assets/Audio/` |
| `Data/` | ✓ | `Data/Player/`, `Data/Enemy/` con los `.asset` de SO |
| `Prefabs/` | Con otro nombre | Está como `Assets/Prefab/` (singular) |
| `Scenes/` | ✓ | `MainMenu.unity`, `SampleScene.unity` |
| `Scripts/` | ✓ | Subcarpetas por dominio. `Scripts/Data/` está, y `Game/` y `Ui/` las reemplazaste por `UI/` y carpetas temáticas |

Archivos mal ubicados:

- `Assets/Audio/GameAudio.mixer` va en `Assets/Art/Audio/`.
- `Assets/Audio/Placeholder.wav` va en `Assets/Art/Audio/`.

Otras observaciones (no las tengo en cuenta): la carpeta `Assets/Art/Sprites/Enviroment/` tiene un typo (`Environment`). `Assets/Settings/`, `DefaultVolumeProfile.asset`, `UniversalRenderPipelineGlobalSettings.asset`, `InputSystem_Actions.inputactions` y `TextMesh Pro/` son de la plantilla URP o de paquetes, no los evalúo. Tenés `.gitignore` de Unity; no tenés `.gitattributes`.

## 4. Qué le falta

- Asignar la música de gameplay en `AudioGameplay` (en `SampleScene.unity`) y cambiar `Placeholder.wav` por un clip distinto para cada acción.
- Sonido al agarrar el pickable y al eliminar al enemigo (suscribite a `Pickup.PickedUp` y `Enemy.Died`).
- Un power up o una modificación de jugabilidad activa en la escena: agregá `EnemySpawner` a `SampleScene` y referencialo en `GameManager.spawners`, o sumá un pickup que modifique velocidad, salto o daño.
- Que `productName` sea igual al nombre del repo (`TP07_C1_Leguizamon_Wizzie`).
- Bloquear el input del jugador mientras el panel de Settings pausa el juego.
- Que los proyectiles se destruyan al tocar el suelo o las paredes.
- El link a la build publicada en itch.io en el `README.md` (hoy no tiene ningún link).

## 5. Hallazgos de código

### Error (4)

- `Assets/Scripts/Audio/AudioGameplay.cs:10` No hay música de gameplay asignada y todos los efectos usan `Placeholder.wav`. Tampoco hay suscripción para el pickup ni para la muerte del enemigo (R10).
- `Assets/Scripts/Pickups/Pickup.cs:15` `PickedUp` no tiene suscriptores: no suena nada al agarrar el pickable (R10).
- `Assets/Scripts/Enemy/Enemy.cs:74` `Enemy.Died` no dispara ningún sonido (R10).
- `Assets/Scripts/Globals/GameManager.cs:9` `spawners` está en None y `EnemySpawner` no está en la escena: no hay modificación de jugabilidad activa ni power ups (R13).

### Bug (2)

- `Assets/Scripts/Player/Player.cs:72` El jugador sigue leyendo input con `Time.timeScale = 0`. Space entra a Jump y reproduce el sonido durante la pausa, y Q deja Throw trabado.
- `Assets/Scripts/Projectiles/Projectile.cs:50` Los proyectiles atraviesan suelo y paredes, porque solo hacen `Despawn` al tocar algo con `Health`.

### Warning (6)

- `Assets/Scripts/Player/States/Air/PlayerAirState.cs:20` `velocityX <= 0f` y `velocityX != 0f` comparan con signo y sin tolerancia, así que el aterrizaje es distinto a izquierda y a derecha.
- `Assets/Scripts/Player/States/Air/PlayerJumpState.cs:22` Seteás `linearVelocity` desde `OnEnter`, que corre en `Update`.
- `Assets/Scripts/Player/PlayerContext.cs:6` Todos los campos del contexto son `public` y mutables.
- `Assets/Scripts/Enemy/Enemy.cs:60` `FindFirstObjectByType<Player>` en runtime, en el `Awake` de cada enemigo.
- `Assets/Scripts/Player/States/Ground/PlayerGroundState.cs:30` `KeyCode.Q` está hardcodeado, y el resto de las teclas sale de `PlayerSO`.
- `Assets/Scripts/Player/States/Ground/PlayerIdleState.cs:18` `Animator.Play(animation.name)` ata el nombre del estado del Animator al nombre del clip (se repite en todos los estados y en `EnemyContext.PlayAnimation`).

### Suggestion (9)

- `Assets/Scripts/Combat/Health.cs:40` Método de prueba `TakeTestDamage` con `[ContextMenu]`.
- `Assets/Scripts/Player/States/Air/PlayerJumpState.cs:32` `OnUpdate` y `OnExit` vacíos.
- `Assets/Scripts/Player/Player.cs:7` Typo `Trasnforms` en el Header.
- `Assets/Scripts/Player/States/PlayerStateSetup.cs:1` `using UnityEngine` sin usar.
- `Assets/Scripts/UI/UIPlayButton.cs:1` `using UnityEngine` sin usar.
- `Assets/Scripts/UI/UIMainMenuButton.cs:1` `using UnityEngine` sin usar.
- `Assets/Scripts/Globals/SceneLoader.cs:7` La escena de gameplay conserva el nombre de plantilla `SampleScene`.
- `Assets/Scripts/Enemy/States/EnemyStateSetup.cs:11` El enemigo usa `PlayerAnimationsSO`, y el nombre no corresponde.
- `Assets/Scripts/UI/UIMenuButton.cs:11` `GetComponent<Button>` en cada `OnEnable`.

## 6. Recomendaciones

1. Completá el audio: asigná `gameplayMusic`, poné un clip propio para salto, disparo y derrota, y agregá en `AudioGameplay` los sonidos de pickup (`Pickup.PickedUp`) y de muerte del enemigo (`Enemy.Died`).
2. Activá una modificación de jugabilidad: poné `EnemySpawner` en `SampleScene` y cargalo en `GameManager.spawners`, o creá un pickup de power up (velocidad, salto doble, daño).
3. Cambiá `productName` a `TP07_C1_Leguizamon_Wizzie` en Project Settings.
4. Cortá el input del jugador durante la pausa (`Player.cs:72`) y destruí los proyectiles al tocar la capa Ground (`Projectile.cs:50`).
5. Pasá la aplicación del salto a `FixedUpdate`, compará velocidades con `Mathf.Abs` y una tolerancia, y pasá la tecla de disparo a `PlayerSO`.
6. Mové `Assets/Audio/` a `Assets/Art/Audio/` y limpiá los `using` y los métodos vacíos que te marqué.
7. Publicá el juego en itch.io y agregá el link en el `README.md`.
