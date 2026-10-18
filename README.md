# Freight

A 3D logistics and trading game built in **Unity**. You run a small fleet of cargo ships, hauling resources between islands while the map grows one block at a time.

> **In development.** Freight is an early prototype. Many systems are still placeholders, there are TODOs throughout the code, and the art in `Assets/Art-temp` is temporary.

## The Idea

- The world is a set of islands generated procedurally. Each turn a new block of land is added to an island, and a building is placed on it.
- **Producer** buildings (such as mines and farms) make resources. **Consumer** buildings (such as towns) buy them.
- You give your ships orders to **collect** resources from producers and **deliver** them to consumers, earning credits in a wallet.
- Turns are timed (5 seconds by default). Time pauses between turns, and on certain turns you pick a **reward**, such as a new ship or a speed upgrade for an existing one.
- The game ends when there are no more blocks left to place.

## Ships and Orders

Ships are defined as ScriptableObjects (`Columbus`, `Piccolo` and an enemy ship so far), each with its own speed and order limit. Select a ship and right-click a building or a spot on the map to see the orders available for that target.

Orders are built from small reusable actions that run in sequence: look at the target, navigate there, wait for the landing platform to be free, line up, open the platform, load or unload freight, pay or collect credits, then leave. Orders queue up, and a ship reports a notification if an order can't start, for example when it has no cargo space or the building doesn't produce that resource.

## Controls

| Action | Keyboard and mouse | Gamepad |
| --- | --- | --- |
| Move camera | `W` `A` `S` `D` | Left stick |
| Rotate camera | Mouse movement (see `CameraController` for any button needed) | Right stick |
| Zoom | Mouse wheel | Right stick |
| Select | Left click | South button |
| Give an order | Right click | East button |

## Getting Started

### Requirements

- Unity **2021.3.11f1**, as set in `ProjectSettings/ProjectVersion.txt`
- Packages are restored from `Packages/manifest.json`, including the Universal Render Pipeline, Input System, TextMesh Pro and the [Serialized Reference UI](https://github.com/TextusGames/UnitySerializedReferenceUI) package, which is pulled from GitHub (Git must be installed)

### Running

1. Clone the repository and open the folder in Unity Hub.
2. Open `Assets/Resources/Scenes/game.unity`.
3. Press **Play**.

The game starts straight into a new game. A main menu scene exists but is currently disabled in `GameManager`.

## Project Structure

```
Assets/
├── Scripts/
│   ├── SceneControllers/      GameManager, which sets up and starts a game
│   ├── GameSceneControllers/  Camera, input, terrain, buildings, turns, wallet, pathfinding...
│   ├── Interactables/         Buildings and ships
│   ├── Orders/                Collect, deliver and move orders and their reusable actions
│   ├── ActionList/            Sequential action runner used by orders
│   ├── Request/               Resource requests for producers and consumers
│   ├── Wallet/                Credits and transactions
│   ├── Terrain/               Procedural island and map generation
│   ├── Pathfinding/           A* grid and algorithm
│   ├── TurnRewards/           Rewards offered between turns
│   ├── ScriptableObjects/     Definitions for ships, buildings, resources and settings
│   └── UI/                    HUD, panels, screens and widgets
├── Resources/
│   ├── Defintions/            Ship, building, resource, reward and map setting assets
│   ├── Prefabs/               Buildings, ships, systems and previews
│   └── Scenes/                Game scene and others
├── Input Settings/            Input System controls
├── Blenders/                  Blender source models
├── QuickOutline/              Third-party outline effect
└── Art-temp/                  Temporary third-party art
```

## How It Works

- **Game setup**: `GameManager` initialises each controller, generates the map, places the mothership, ships and starting buildings, then starts the clock.
- **Terrain**: islands are laid out with Poisson disc sampling and split into cells using Delaunay triangulation and Voronoi diagrams. Each turn adds the unplaced cell closest to the centre.
- **Pathfinding**: ships use an A* grid. Paths are computed on a background thread, with requests handled one at a time, and a straight line is used when nothing is in the way.
- **Turns**: `TurnsController` listens to the game clock and fires turn started and ended events, offering rewards on configured turns.
- **Data driven**: ships, buildings, resources, rewards and map settings are all ScriptableObjects under `Assets/Resources/Defintions`.

## Third-Party Assets

- QuickOutline by Chris Nolet for selection outlines (see `Assets/QuickOutline/Readme.txt`)
- Placeholder art in `Assets/Art-temp`: the Megapoly.Art containers, Ultimate Fantasy RTS and Ultimate Spaceships packs. Some of these include their own license files, so check them before reusing or redistributing the assets.

## License

No license has been added to this repository yet.
