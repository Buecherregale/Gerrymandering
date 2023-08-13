using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;

public class WorldMapInteraction : MonoBehaviour
{
    [Serializable]
    public struct Country {
        public Countries name;
        public GameObject gameObject;
    }
    
    [SerializeField] private List<Country> countries;

    private void Update() {
        // Check if the mouse is over a UI element
        if (EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonDown(0)) {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePosition2D = new Vector2(mousePosition.x, mousePosition.y);
            
            RaycastHit2D hit = Physics2D.Raycast(mousePosition2D, Vector2.zero);
            
            if (hit.collider != null && hit.collider.CompareTag("MapMarker"))
            {
                Debug.Log("Mouse is over a Map Marker!");
                Countries found = countries.Where(country => country.gameObject == hit.collider.gameObject)
                    .Select(country => country.name)
                    .First();
                Debug.Log("Found country: " + found);
                
            }
        }
    }
    
    [System.Serializable]
    public enum Countries {
        Deutschland,
        USA,
    }
}
