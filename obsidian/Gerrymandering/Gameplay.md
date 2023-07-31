---
tags: todo
priority: 1
---

Controls:
	Touch/Mouse:
	Dragging für Markieren von Tiles zu neuem District
	Einzelner Click auf District zum löschen


Tilemaps:

```c#
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapMarker : MonoBehaviour
{
    public Tilemap tilemap;

    private Camera mainCamera;
    private Vector3Int previousTilePosition;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Vector3 touchPosition = mainCamera.ScreenToWorldPoint(touch.position);
                Vector3Int tilePosition = tilemap.WorldToCell(touchPosition);

                MarkTile(tilePosition);
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                Vector3 touchPosition = mainCamera.ScreenToWorldPoint(touch.position);
                Vector3Int tilePosition = tilemap.WorldToCell(touchPosition);

                if (tilePosition != previousTilePosition)
                {
                    MarkTile(tilePosition);
                }
            }
        }
    }

    private void MarkTile(Vector3Int tilePosition)
    {
        TileBase tile = tilemap.GetTile(tilePosition);

        if (tile != null)
        {
            // Hier kannst du die Logik zur Markierung des Tiles implementieren
            // Zum Beispiel die Farbe oder das Aussehen des markierten Tiles ändern
            Debug.Log("Tile marked at position: " + tilePosition);
        }

        previousTilePosition = tilePosition;
    }
}

```