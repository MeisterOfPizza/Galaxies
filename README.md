# Galaxies
Galaxies is a game made in C# MonoGame for a school project.

---

## Game Specifications
### Genres
* 2D
* Space
* Roguelike
* Exploration
* Turned based combat  

### Development
**Language**: C#  
**Engine**: MonoGame  
**IDE**: Visual Studio 2017/2019  
**Drop**: 5 weeks (from 2019-01-28)

---

## Vision
Galaxies (the game) should be short, simple and sweet.

The player will explore a randomly generated universe with occasional enemies showing up to attack the player. The player will be able to trade for new ships. Ship upgrades can be bought or stolen from destroyed enemies. If the player dies, the game is over. However, they are able to save at the Citadel, where he/she may also trade with other NPCs.

---

## Gameplay
### Goal
The main goal of the game is to travel to the center of the galaxy and to defeat the evil space monster, threatening galactic peace. Defeating this monster will result in the player completing the game (a victory).

### Combat
Combat will be very simple in order to save development time. It will be turn based with each participent only having one sort of attack. Attacking will consume energy, which will be proportionally refilled each turn the attacker chooses to defend ("Shield Up") instead of attacking.

### Ships and Ship Upgrades
There will be a very small quantity of ships available for the player to use, approximately three different designs (and classes). Enemies will have a bit more of a varied ship pool, approximately ten different ship designs. *Ship Upgrades* are nothing more than stat boosts. Each ship upgrade will boost one or more stats, eg. ship health, ship shields, damage and/or energy etc.

### Exploring
Whenever entering a new solar system, the player will be able to freely explore any number of planets. When the player has selected a planet, a random event will trigger. This event might give you some Galactic Gold, ship upgrade(s) or it might even trigger an enemy encounter.

Leaving the current solar system can be done at any time, however, once left, the same system cannot be entered again.

Docking at the Citadel will grant you the ability to repair your ship, purchase new ships or upgrades for your ship or to sell owned ship upgrades.

### Ending
Star charts can be purchased for a large sum of Galactic Gold from one of the NPCs inside the Citadel. These star charts shows us the path to the center of the galaxy. Following this path will result in the player encountering the final boss. Once the player has entered the final battle, they cannot back out, and will either win or lose the game.

---

## Gamedata

Ships, ship upgrades, enemies, planetary systems (including planets) and trades (buy and sell price etc.) will all be stored in different .xml files. Data structures for each .xml file will not be presented here.

---

## Keywords
**Galactic Gold** - Main currency of the game.  
**(The) Citadel** - Galactic hub for all intelligent life forms in the Milky Way.
