using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    public Point GridPosition { get; private set; } // GridPosition of this Tile.
    public bool IsEmpty;                            // Check if a building, tree etc. is blocking this Tile
    public GameObject GameObjectOnTile;             // Reference for the GameObject ON THIS Tile.

    public void Setup(Point gridPos, Transform parentTile, BiomePreset biome)
    {
        this.transform.SetParent(parentTile);                                                        // Set the Tile's parent.
        this.GetComponent<SpriteRenderer>().sprite = biome.GetTileSprite();                          // Set the Tile's sprite. 
        this.GridPosition                          = gridPos;                                        // Set the Tile's GridPosition.
        this.name                                  = gridPos.X + "-" + gridPos.Y + "-" + biome.name; // Set the Tile's name.

        // Set IsEmpty
        if (biome.name == "Grass")
        {
            IsEmpty = true;
        }
        else
        {
            IsEmpty = false;
        }

        MapManager.Instance.TilesDictionary.Add(gridPos, this); // Adds the Tile to the TilesDictionary in MapManager.
    }

    public void PlaceTree(GameObject treeGO)
    {
        if (IsEmpty == true)
        {
            treeGO.transform.SetParent(transform);  // Set the tile under it as a parent.
            GameObjectOnTile = treeGO;              // Reference for the TreeGo, which sits on this tile.
            IsEmpty          = false;               // Set IsEmpty to false.
        }
    }
}
