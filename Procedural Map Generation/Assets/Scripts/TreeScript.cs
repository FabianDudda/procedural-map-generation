using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
    public Point GridPosition { get; set; }

    public void Setup(int sortingOrder, Point gridPosition)
    {
        this.GetComponent<SpriteRenderer>().sortingOrder    = sortingOrder;                                     // Set the sortingOrder.
        this.GridPosition                                   = gridPosition;                                     // Set the GridPosition.
        this.transform.position                             = new Vector3(gridPosition.X, gridPosition.Y, 0);   // Set the position equals to the Tile it sits on.

        TileScript tileScript = MapManager.Instance.TilesDictionary[gridPosition];  // Get TileScript from the Tile where the Tree should be placed.
        tileScript.PlaceTree(this.gameObject);                                      // Execute the PlaceTree() in the TileScript
    }
}
