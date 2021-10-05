using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : Singleton<MapManager>
{
    [Header("Tiles")]
    public BiomePreset[] Biomes;
    public GameObject TilePrefab;
    public Transform TileHolder;
    public Dictionary<Point, TileScript> TilesDictionary { get; set; }

    [Header("Objects")]
    [SerializeField] private GameObject[] plantPrefabs; // Reference for an array of the plantPrefabs.  0 = tree,  1 = bush

    [Header("Dimensions")]
    public int MapXSize;
    public int MapYSize;
    public float scale = 1.0f;
    public Vector2 offset;

    [Header("Height Map")]
    public Wave heightWaves;
    public float[,] heightMap;

    void Start()
    { 
    }
     
    public void SetSeedAndStartGame(float seedinput)
    {
        heightWaves.seed = seedinput;
        GenerateMap();
        GenerateObjects();
    }
    
    void GenerateMap()
    {
        // Create a Dictionary 
        TilesDictionary = new Dictionary<Point, TileScript>();

        // Generate the height map
        heightMap = NoiseGenerator.Generate(MapXSize, MapYSize, scale, heightWaves, offset);

        for (int x = 0; x < MapXSize; ++x)
        {
            for (int y = 0; y < MapYSize; ++y)
            {
                // Create a new 'TileScript' and call it 'newTile'.
                // Instantiate the GameObject from the TilePrefab (which is an empty GameObject and acts like a placeholder).
                // Then it adds the Component <TileScript> to the GameObject (This adds the TileScript.cs to the GameObject).
                TileScript newTile = Instantiate(TilePrefab.GetComponent<TileScript>(), new Vector3(x, y, 0), Quaternion.identity);

                // Call the Setup() for the newTile.
                newTile.Setup(new Point (x,y), TileHolder, GetBiome(heightMap[x, y]));               
            }
        }
    }

    #region PROCEDURAL MAP GENERATION
    public class BiomeTempData
    {
        public BiomePreset biome;

        public BiomeTempData(BiomePreset preset)
        {
            biome = preset;
        }

        public float GetDiffValue(float height)
        {
            return (height - biome.minHeight);
        }
    }
    BiomePreset GetBiome(float height)
    {
        BiomePreset biomeToReturn = null;
        List<BiomeTempData> biomeTemp = new List<BiomeTempData>();

        foreach (BiomePreset biome in Biomes)
        {
            if (biome.MatchCondition(height))
            {
                biomeTemp.Add(new BiomeTempData(biome));
            }
        }

        float curVal = 0.0f;

        foreach (BiomeTempData biome in biomeTemp)
        {
            if (biomeToReturn == null)
            {
                biomeToReturn = biome.biome;
                curVal = biome.GetDiffValue(height);
            }
            else
            {
                if (biome.GetDiffValue(height) < curVal)
                {
                    biomeToReturn = biome.biome;
                    curVal = biome.GetDiffValue(height);
                }
            }
        }

        if (biomeToReturn == null)
            biomeToReturn = Biomes[0];

        return biomeToReturn;
    }
    #endregion

    void GenerateObjects()
    {
        int treeCounter = 0;
        while (treeCounter < 15)
        {
            Point randomGridPosition = new Point(UnityEngine.Random.Range(0, MapXSize), UnityEngine.Random.Range(0, MapYSize)); // Creates a randomGridPosition (x,y) [x= between 0 and MapXSize; y= between 0 and MapYSize].
            TileScript tileScript    = TilesDictionary[randomGridPosition];                                                     // Get TileScript under randomGridPosition.

            if (tileScript.IsEmpty == true && tileScript.VegetationPossible == true)
            {
                int sortingLayer    = 100 + tileScript.GridPosition.X - tileScript.GridPosition.Y;  // Set sortingLayer [Lowest Grid row will be rendered above higher Grid row].

                GameObject TreeGO   = ObjectPool.Instance.Pool.GetObject(plantPrefabs[0].name);     // Instantiate a TreeGO out of the Object Pool.
                TreeScript Tree     = TreeGO.GetComponent<TreeScript>();                            // Get the TreeScript of the TreeGO.
                Tree.Setup(sortingLayer, randomGridPosition);                                       // Execute Setup() in the  TreeScript.
            }

            treeCounter++;
        }

        int bushCounter = 0;
        while (bushCounter < 15) {
            Point randomGridPosition = new Point(UnityEngine.Random.Range(0, MapXSize), UnityEngine.Random.Range(0, MapYSize)); // Creates a randomGridPosition (x,y) [x= between 0 and MapXSize; y= between 0 and MapYSize].
            TileScript tileScript = TilesDictionary[randomGridPosition];                                                     // Get TileScript under randomGridPosition.

            if (tileScript.IsEmpty == true && tileScript.VegetationPossible == true) {
                int sortingLayer = 100 + tileScript.GridPosition.X - tileScript.GridPosition.Y;  // Set sortingLayer [Lowest Grid row will be rendered above higher Grid row].

                GameObject BushGO = ObjectPool.Instance.Pool.GetObject(plantPrefabs[1].name);     // Instantiate a TreeGO out of the Object Pool.
                TreeScript Bush = BushGO.GetComponent<TreeScript>();                            // Get the TreeScript of the TreeGO.
                Bush.Setup(sortingLayer, randomGridPosition);                                       // Execute Setup() in the  TreeScript.
            }

            bushCounter++;
        }


    }
}


