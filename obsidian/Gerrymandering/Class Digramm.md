
```plantuml

class GerrymanderingTile {

}

class District {
	faction : Faction 
	marked : boolean
	Neighbours: Neighbour[] 
}

class VisualTile {

}

class County {
	District : District[]
	id : int
	faction : Faction 
	- calculateDominant() : Faction
	AddDistrict() : boolean
	RemoveDistrict()
	- calulateNeighbours()
}

enum Faction {
 Republicans,
 Democrats
}

enum Neighbours {
	top,
	bot,
	left,
	right
}

District <|-- GerrymanderingTile
VisualTile <|-- GerrymanderingTile
County o-- District : contains


```