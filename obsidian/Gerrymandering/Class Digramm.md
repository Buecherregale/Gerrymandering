
```plantuml

class CustomTile{

}

class FactionTile {
	faction : Faction 
	isMarked : boolean
	Neighbours: Neighbour[] 
}

class VisualTile {

}

class Group {
	factionTile : Tile[]
	number : int
	faction : Faction 
	- calculateDominant() : Faction
	addTile() : boolean
	removeTile()
	- calulateNeighbours()
}

enum Faction {
 Republicans,
 Democrats
}

enum Neighbours{
	top,
	bot,
	left,
	right
}

FactionTile <|-- CustomTile
VisualTile <|-- CustomTile
Group o-- FactionTile : contains


```