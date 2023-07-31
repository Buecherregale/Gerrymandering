
```plantuml

enum Faction {
 Neutral,
 Democrats,
 Republicans
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
	_instancecounter : int
	id : int
	county : County
	faction : Faction 
	Position : Vector3Int
	District(tile : DistrictTile, pos : Vector3Int) : District
}

class County {
	Districts : District[]
	_instancecounter : int
	Winning : Faction
	Size : int
	id : int 
	County() : County
}

class State {
	_instancecounter : int
	Counties : County[]
	Winning : Faction
	Size : int
	id : int
	State() : State
}

class DistrictManager {
	tilemanager : Tilemanager
	_districts : Dictionary<Vector3Int, District>
	CalculateNeighbours(pos : Vector3Int) : Vector3Int[]
	GetAllNeighbours(pos : Vector3Int) : Vector3Int[]
	GetDistrict(pos : Vector3Int) : District
	TryGetDistrict(pos : Vector3Int) : bool
	DrawCountyBorder(district : District) : void
	ClearCountyBorders(district : District) : void
	Start : void
}

District <|-- GerrymanderingTile
VisualTile <|-- GerrymanderingTile
County o-- District : contains
State o-- County : contains
District o-- DistrictManager : manages


```