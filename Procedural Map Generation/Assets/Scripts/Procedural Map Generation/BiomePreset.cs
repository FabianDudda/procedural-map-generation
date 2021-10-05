using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Biome Preset", menuName = "New Biome Preset")]
public class BiomePreset : ScriptableObject
{
    public Sprite[] tiles;
    public float minHeight;

    public enum TileType { Water, Sand, Grass, Dirt, Mountain };

    // returns a random sprite
    public Sprite GetTileSprite()
    {
        if (this.name == "Water") {
            TileType type = TileType.Water;
            Debug.Log(type);
        }
        else if (this.name == "Sand") {
            TileType type = TileType.Sand;
            Debug.Log(type);
        }
        else if (this.name == "Grass") {
            TileType type = TileType.Grass;
            Debug.Log(type);
        }
        else if (this.name == "Dirt") {
            TileType type = TileType.Dirt;
            Debug.Log(type);
        }
        else if (this.name == "Mountain") {
            TileType type = TileType.Mountain;
            Debug.Log(type);
        }


        return tiles[Random.Range(0, tiles.Length)];
    }

    public bool MatchCondition(float height)
    {
        return height >= minHeight;
    }
}
