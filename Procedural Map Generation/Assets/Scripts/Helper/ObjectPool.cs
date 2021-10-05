using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The ObjectPool will check, if there is an equal deactivated GameObject in the Pool
// If there is one, it will reuse this GameObject
// If there is NO one, it will create a new GameObject

// Based on the inScope Studio Tutorial
// https://www.youtube.com/watch?v=69k5eRfrEsI

public class ObjectPool : Singleton<ObjectPool>
{

    // An array of prefabs used to create an object in the world.
    [SerializeField] private GameObject[] objectPrefabs;

    // A list for all GameObjects in the ObjectPool.
    private List<GameObject> pooledObjects = new List<GameObject>();

    // Property for the ObjectPool, so that other Scripts can access it.
    public ObjectPool Pool { get; set; }

    private void Awake()
    {

        // Gets the ObjectPool Script
        Pool = GetComponent<ObjectPool>();
    }

    // Gets an object from the pool
    public GameObject GetObject(string type)
    {

        // Go through all GameObjects in the pooledObjects list.
        foreach (GameObject go in pooledObjects)
        {

            // Check if we have an equal GameObject in the ObjectPool and
            // Check if the GameObject is Inactive.
            if (go.name == type && go.activeInHierarchy == false)
            {

                // Activates the Gameobject
                go.SetActive(true);

                // Return the GameObject
                return go;
            }
        }

        // If the pool didn't contain the object that we needed,
        // Then we need to instantiate a new GameObject,
        for (int i = 0; i < objectPrefabs.Length; i++)
        {

            // If we have a prefab for creating the object.
            if (objectPrefabs[i].name == type)
            {

                // We instantiate the prefab of the correct type.
                GameObject newObject = Instantiate(objectPrefabs[i]);

                // Adds the newObject to the polledObjects list.
                pooledObjects.Add(newObject);

                // Sets the name.
                newObject.name = type;

                // Return the GameObject.
                return newObject;
            }
        }
        return null;
    }

    // Deactivates the GameObject
    public void ReleaseObject(GameObject gameObject)
    {

        // Set the GameObject inactive.
        gameObject.SetActive(false);
    }
}
