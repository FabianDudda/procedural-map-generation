using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
    public Sprite[] treeSprites;
    public Point GridPosition { get; set; }

    public void Setup(int sortingOrder, Point gridPosition)
    {
        SpriteRenderer sr  = this.GetComponent<SpriteRenderer>();
        sr.sortingOrder    = sortingOrder;                                     
        sr.sprite          = treeSprites[Random.Range(0, treeSprites.Length)];

        this.GridPosition       = gridPosition;                                     // Set the GridPosition.
        this.transform.position = new Vector3(gridPosition.X, gridPosition.Y, 0);   // Set the position equals to the Tile it sits on.

        TileScript tileScript = MapManager.Instance.TilesDictionary[gridPosition];  // Get TileScript from the Tile where the Tree should be placed.
                                                                                    //tileScript.PlaceTree(this.gameObject);                                      // Execute the PlaceTree() in the TileScript  

        
        if (tileScript.IsEmpty == true && tileScript.VegetationPossible == true) {

            this.transform.SetParent(tileScript.transform);  // Set the tile under it as a parent.

            tileScript.GameObjectOnTile = this.gameObject;              // Reference for the TreeGo, which sits on this tile.
            tileScript.IsEmpty    = false;
            tileScript.IsWalkable = false;
        }
        
    }
}
