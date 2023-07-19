
```plantuml

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

class GerrymanderingTile {

}

class VisualTile {

}

class District {
	county : County
	faction : Faction 
	marked : boolean
	Neighbours() : District[] 
}

class County {
	Districts : District[]
	id : int
	Winning : Faction 
	AddDistrict(district : District) : boolean
	RemoveDistrict(district : District) : boolean
	- CalculateDominant() : Faction
}

class State {
	Counties : County[]
	Winning : Faction
	AddCounty(county : County): boolean
	RemoveCounty(county : County): boolean
	- CalculateDominant() : Faction
}

District <|-- GerrymanderingTile
VisualTile <|-- GerrymanderingTile
County o-- District : contains
State o-- County : contains


```