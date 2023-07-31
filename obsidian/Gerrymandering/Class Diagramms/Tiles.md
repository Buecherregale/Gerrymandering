```plantuml
class GerrymanderingTile {}

class VisualTile {}

class DistrictTile {
	+ Faction: Faction
}

VisualTile <|-- GerrymanderingTile
DistrictTile <|-- GerrymanderingTile
GerrymanderingTile <|-- Unity.Tile
```