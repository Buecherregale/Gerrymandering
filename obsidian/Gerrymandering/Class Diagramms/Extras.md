```plantuml
class InputEvent {
	+ inputPosition: Vector3
}

class InputEventSystem {
	+ OnInpBegin: InputEvent
	+ OnInpDrag: InputEvent
	+ OnInpEnd: InputEvent
}

class GerrymanderingUtil {
	+ {static} VecToDir(origin: Vector3Int, next: Vector3Int): Direction
	+ {static} DirToVec(direction: Direction): Vector3Int
	+ {static} MaxIndex(list: int[]): int
	+ {static} GetColor(faction: Faction): Color
}
```