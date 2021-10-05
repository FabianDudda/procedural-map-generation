using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    public Point GridPosition { get; private set; } // GridPosition of this Tile.
    public GameObject GameObjectOnTile;             // Reference for the GameObject ON THIS Tile.
    public bool IsEmpty;                            
    public bool IsWalkable;
    public bool VegetationPossible;


    public void Setup(Point gridPos, Transform parentTile, BiomePreset biome)
    {
        this.transform.SetParent(parentTile);                                                        // Set the Tile's parent.
        this.GetComponent<SpriteRenderer>().sprite = biome.GetTileSprite();                          // Set the Tile's sprite. 
        this.GridPosition                          = gridPos;                                        // Set the Tile's GridPosition.
        this.name                                  = gridPos.X + "-" + gridPos.Y + "-" + biome.name; // Set the Tile's name.

        // Set IsEmpty
        if (biome.name == "Mountain") 
            { IsEmpty = false; }
        else 
            { IsEmpty = true; }

        // Set IsWalkable
        if (biome.name == "Water" || biome.name == "Mountain") 
            { IsWalkable = false; } 
        else 
            { IsWalkable = true; }

        // Set VegetationPossible
        if (biome.name == "Grass") 
            { VegetationPossible = true; }
        else 
            { VegetationPossible = false; }

        MapManager.Instance.TilesDictionary.Add(gridPos, this); // Adds the Tile to the TilesDictionary in MapManager.
    }

    //public void PlaceTree(GameObject treeGO)
    //{
    //    if (IsEmpty == true)
    //    {
    //        treeGO.transform.SetParent(transform);  // Set the tile under it as a parent.
    //        GameObjectOnTile = treeGO;              // Reference for the TreeGo, which sits on this tile.

    //        IsEmpty    = false;               
    //        IsWalkable = false;
    //    }
    //}
}
