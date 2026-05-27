# Hollowspire

A small atmospheric 2D platformer prototype inspired by metroidvania games like Hollow Knight.

This project was built as part of my game development portfolio to explore:

- responsive combat
- directional attacks
- hitbox systems
- animation timing
- player movement feel
- gameplay programming architecture in Unity

---

# Features

- 2D platformer movement
- Directional melee attacks
- Dynamic hitbox positioning
- Coroutine-based attack system
- Animator-driven combat
- Metroidvania-inspired atmosphere
- Debug hitbox visualization using Gizmos

---

# Technologies

- Unity
- C#
- Unity Animator
- Coroutines
- 2D Physics

---

# Combat System

The combat system uses:

- directional attack data
- reusable attack definitions
- coroutine timing
- dynamic hitbox offsets

Example structure:

```csharp
Dictionary<Direction, AttackData>
```

Each attack direction contains:

- animation name
- hitbox offset
- timing control

This makes the system scalable for:

- combo attacks
- new weapons
- enemy combat
- attack canceling
- hit feedback improvements

---

# Current Goals

- Improve combat responsiveness
- Add enemy AI
- Expand movement mechanics
- Create interconnected levels
- Improve visual feedback
- Add sound design and polish

---

# Screenshots

_Add gameplay screenshots here later._

---

# Future Plans

- Combo system
- Enemy behaviors
- Boss fights
- Dash mechanic
- Procedural feedback effects
- Better state machine architecture

---

# About

This project is mainly focused on gameplay programming and combat feel.

Current areas of study:

- gameplay architecture
- game feel
- combat systems
- Unity engineering patterns
- metroidvania design principles
