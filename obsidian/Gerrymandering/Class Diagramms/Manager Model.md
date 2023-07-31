```plantuml
namespace Model {
enum Faction {
	Neutral,
	Democrats,
	Republicans
}

class District {
	+ Tile: DistrictTile
	+ County: County
	+ Faction: Faction
	+ Position: Vector3Int
	+ Id: int
}

class County {
	+ Districts: District[]
	+ Winning: Faction
	+ Size: int
	+ Id: Int
}

class State {
	+ Counties: County[]
	+ Winning: Faction
	+ Size: int
	+ Id: int
}
}
class TileManager {
	# districtMap: Tilemap
	# markMap: Tilemap
	# borderMaps: Tilemap[]
	# markTile: Tile

	+ BorderTilesByParty: Tile[][]
	- borderTilesNeutral: Tile[]
	- borderTilesRepuublicans: Tile[]
	- borderTilesDemocrats: Tile[]
}

class DistrictManager {
	- _districts: Map<Vector3Int, District> 

	+ CalculateNeighbours(pos: Vector3Int): Vector3Int[]
	+ GetAllNeighbours(pos: Vector3Int): Vector3Int[]
	+ GetDistrict(pos: Vector3Int): District
	+ TryGetDistrict(pos: Vector3Int, out district: District): bool
	+ DrawCountyBorder(district: District)
	+ ClearCountyBorders(district: District)
}

class CountyManager {
	+ AddDistrict(county: County, district: District): bool
	+ RemoveDistrict(county: County, district: District): bool
	+ Clear(county: County)
	- {static} CalculateWinning(county: County): Faction
	- CanAddToCounty(county: County, district: District): bool
}

class StateManager {
	+ maxCountySize: int
	- _currentState: State
	- _currentCounty: County
	+ AddCounty(state: State, county: County): bool
	+ RemoveCounty(state: State, county: County): bool
	- {static} CalculateDominant(state: State): Faction
	- OnInpBegin(pos: Vector3)
	- OnInpDrag(pos: Vector3)
	- OnInpEnd(pos: Vector3)
}

County "1" *-- "many" District : contains
State "1" o-- "many" County : contains

District "1" -- "1" Faction: votes for
County "1" -- "1" Faction: won by
State "1" -- "1" Faction: won by

DistrictManager "1" -- "all" District : manages
CountyManager "1" -- "all" County: manages
StateManager "1" -- "all" State: manages
```