using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// abstract class is a super class, that never exist on its own
// Erklärung: https://www.youtube.com/watch?v=ibOBHDgg2kg&t=300
public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
            }

            return instance;
        }
    }
}
