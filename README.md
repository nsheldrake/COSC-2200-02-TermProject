# Crazy Eights

## Overview

Crazy Eights is a desktop implementation of the classic card game built with **C# and Windows Presentation Foundation (WPF)**. The player competes against a computer-controlled opponent through an interactive graphical interface.

The project uses object-oriented design to separate game logic, cards, players, rules, settings, and UI functionality.

## Features

- Play against a computer-controlled opponent
- Interactive card selection and gameplay
- Automatic validation of legal moves
- Choose a new suit when playing an Eight
- Computer selects random valid moves
- Automatic deck reshuffling
- Player, opponent, and deck card counters
- Turn and game status indicators
- Winner detection and play-again functionality
- Customizable background and menu colours
- JSON-based settings persistence
- Keyboard shortcuts
- Custom card graphics

## Technologies Used

- **C#** — Core application and game logic
- **.NET 8** — Application framework
- **WPF** — Windows desktop application framework
- **XAML** — User interface design
- **OOP** — Classes, inheritance, encapsulation, and enums
- **Data Binding** — Dynamic UI updates with `INotifyPropertyChanged`
- **Async/Await** — Computer turns and game delays
- **JSON** — Persistent application settings
- **Git/GitHub** — Version control

## Project Structure

```text
CrazyEightsCosc2200/
│
├── MainWindow.xaml/.cs    # Main UI and event handling
├── GameLogic.cs           # Core game management
├── GameRules.cs           # Crazy Eights rules
├── GameState.cs           # Game state management
├── Card.cs                # Card model
├── Deck.cs                # Deck creation and management
├── Hand.cs                # Player hand management
├── Player.cs              # Player functionality
├── ComputerPlayer.cs      # Computer opponent logic
├── Pile.cs                # Discard pile management
├── Rank.cs / Suit.cs      # Card enums
├── Settings.cs            # Application settings
├── settings.json          # Saved settings
└── images/                # Card and UI graphics
```

## Keyboard Shortcuts

| Action | Shortcut |
|---|---|
| Rules / Guide | `F1` |
| Draw Card | `Ctrl + D` |
| Play Card | `Ctrl + P` |
| Reset Game | `Ctrl + R` |
| Settings | `Ctrl + S` |
| Start Game | `Enter` |

## How to Play

1. Enter your name and start the game.
2. Five cards are dealt to you and the computer.
3. Play a card matching the **rank** or **suit** of the current card.
4. If you cannot play a card, draw from the deck.
5. Playing an **Eight** allows you to choose the next suit.
6. The computer automatically takes its turn.
7. The first player to empty their hand wins.

## Project Setup

### Requirements

- Windows
- Visual Studio 2022 or newer
- .NET 8 SDK
- .NET Desktop Development workload

### Installation

Clone the repository:

```bash
git clone https://github.com/nsheldrake/COSC-2200-02-TermProject.git
```

Open:

```text
CrazyEightsCosc2200/CrazyEightsCosc2200.sln
```

in Visual Studio, then build and run the application with `F5`.

## Authors

**Nathan Sheldrake**  
**Li Yiwei**
