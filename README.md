The Dark King - Console RPG

The Dark King is a C# console RPG built as a final project. It is a turn-based role-playing game with class selection, world exploration, enemy encounters, combat, inventory management, equipment, a shop, status effects, XP, leveling, save slots, and a final boss progression path.

This project was built to practice object-oriented programming in C#, with a focus on inheritance, polymorphism, encapsulation, collections, file I/O, and game system organization.

## Project Overview

The player creates a character, chooses a class, explores different world areas, fights enemies, finds loot, buys and sells items, equips gear, levels up, and eventually unlocks the Realm of Darkness after clearing the King's Castle.

The game runs entirely in the console and uses menus, typed commands, and numbered choices for interaction.

## Features

- Console-based RPG gameplay loop
- New game, load game, delete save, and exit options
- Character creation with name, age, gender, hair colour, and class selection
- Playable classes:
  - Warrior
  - Rogue
  - Mage
- Turn-based combat system
- Enemy subclasses with unique stats and abilities
- Status effects including poison, confusion, and stun
- XP rewards and level-up progression
- Gold rewards and enemy item drops
- Inventory system with weapons, armour, and consumables
- Equipment system for weapons and armour
- Shop system for buying and selling items
- Multiple world areas:
  - Forest
  - Town
  - Mountain
  - Boss' Castle
  - Realm of Darkness
- Three save slots using local text files

## Tech Stack

- C#
- .NET 8
- Console application
- File I/O for saving and loading
- Visual Studio
- Newtonsoft.Json package reference

## Main Systems

### Game Manager

`GameManager` controls the main flow of the game. It handles:

- Start menu
- New game setup
- Load and delete save menus
- World selection
- Area exploration
- Battle flow
- Game over handling
- Boss progression
- Save slot management

### Player and Classes

The `Player` class inherits from `Character` and stores RPG-specific data such as:

- Level
- XP
- Skill points
- Gold
- Inventory
- Equipped weapon
- Equipped armour
- Current location
- Realm of Darkness unlock state

Playable classes such as `Warrior`, `Rogue`, and `Mage` extend the player with different base stats and special abilities.

### Combat

Combat is turn-based. On the player's turn, they can:

- Attack
- Use a class skill
- Use an item
- Flee

Enemies can attack or use special abilities. Combat also supports status effects, damage calculation, defense reduction, XP rewards, gold rewards, and item drops.

### Inventory and Equipment

The inventory supports several item types:

- Weapons
- Armour
- Consumables

Weapons increase attack. Armour can increase defense and maximum HP. Consumables can restore HP or temporarily improve stats.

The equipment system recalculates combat values from base stats plus equipped item bonuses.

### Shop

The shop allows the player to:

- Buy weapons
- Buy armour
- Buy consumables
- Sell inventory items for gold
- Display inventory while shopping

This system gave me practice with lists, item lookup, user input validation, and menu-driven program flow.

### Save and Load

The game supports three local save slots:

```text
save_slot_1.txt
save_slot_2.txt
save_slot_3.txt
```

Each save stores player stats, class, XP, gold, inventory, equipped gear, and progression flags. The load system recreates the correct player subclass and restores inventory/equipment by item name.

## Project Structure

```text
FinalProject/
├── FinalProject.sln
└── RPG/
    ├── Program.cs
    ├── GameManager.cs
    ├── Character.cs
    ├── Player.cs
    ├── Warrior.cs
    ├── Rogue.cs
    ├── Mage.cs
    ├── Enemy.cs
    ├── DireWolf.cs
    ├── KillerHornet.cs
    ├── Golem.cs
    ├── Balrog.cs
    ├── TheKing.cs
    ├── Inventory.cs
    ├── Weapon.cs
    ├── Armour.cs
    ├── Consumeable.cs
    ├── InventoryDatabase.cs
    ├── Shop.cs
    ├── NPC.cs
    ├── Enums.cs
    └── RPG.csproj
```

## How to Run

### Visual Studio

1. Open `FinalProject.sln`.
2. Set `RPG` as the startup project.
3. Build the solution.
4. Run the project.

### Command Line

From the `RPG` folder:

```bash
dotnet run
```

## Example Gameplay Flow

1. Start a new game.
2. Choose a class.
3. Create your character.
4. Explore the Forest or Mountain.
5. Fight enemies and collect rewards.
6. Visit Town to heal and shop.
7. Equip stronger gear.
8. Clear the King's Castle.
9. Unlock the Realm of Darkness.
10. Fight the final boss.

## What I Learned

- Creating a larger C# console application across multiple classes
- Using inheritance for players, enemies, and items
- Using polymorphism for special abilities and display behavior
- Managing game state through a central game manager
- Creating menu-driven user interaction
- Building turn-based combat logic
- Saving and loading player data from files
- Reconstructing objects from saved text data
- Designing an inventory and equipment system

## Known Limitations

- The game is text-based and does not include graphics.
- Some menus require exact item names.
- Shop purchases should be improved with stronger gold validation.
- Some spelling and UI text could be cleaned up.
- Save files are plain text and are not protected from manual editing.

## Future Improvements

- Add better input validation across all menus
- Add clearer combat messages and battle summaries
- Add more enemies, items, areas, and class abilities
- Improve shop purchase validation
- Replace plain text saves with structured JSON
- Add automated tests for combat, saving/loading, and item logic
- Add a cleaner command system for menu navigation

